# Licensed to the .NET Foundation under one or more agreements.
# The .NET Foundation licenses this file to you under the MIT license.

#Requires -Version 7.0

[CmdletBinding()]
Param(
    [Parameter(Position=0)]$TestProject = "HelloWasm.csproj",
    [ValidateSet("browser,wasi")][string]$OS = "browser",
    [switch]$Build,
    [switch]$Rebuild,
    [switch]$Analyze,
    [switch]$Summary,
    [ValidateSet("Debug","Release")][string]$Config = "Release",
    [ValidateSet("Debug","Checked","Release")][string]$IlcConfig = "Release",
    [Nullable[bool]]$DebugSymbols = $null,
    [uint]$NumberOfDiffsToShow = 20,
    [string]$PassThrough = ""
)

$ErrorActionPreference = "Stop"

$Arch="wasm"

$ShowHelp = !$Analyze -and !$Build -and !$Rebuild -and !$Summary

if ($ShowHelp)
{
    Write-Host ""
    Write-Host "wasmjit-diff.ps1 - Find differences in code generated by NativeAOT-LLVM"
    Write-Host ""
    Write-Host " Options:"
    Write-Host "  -TestProject         : Project file path. Default is HelloWasm.csproj"
    Write-Host "  -OS                  : Target OS (browser/wasi)"
    Write-Host "  -Build               : Build the test project for analysis"
    Write-Host "  -Rebuild             : Force-build the 'base' to diff against"
    Write-Host "  -Analyze             : Analyze the results from two project builds"
    Write-Host "  -Summary             : Analyze the results and only print the summary"
    Write-Host "  -Config              : Test configuration (Debug/Release). Default is Release"
    Write-Host "  -IlcConfig           : ILC configuration (Debug/Checked/Release). Default is Release"
    Write-Host "  -DebugSymbols        : Whether to build with debug symbols. Default is yes"
    Write-Host "  -NumberOfDiffsToShow : Number of diffs to show. Default is 20"
    Write-Host "  -PassThrough         : Additional command line to pass directly to 'dotnet'"
    Write-Host ""
    Write-Host " To start, use the -Build option to create the base build. Then make changes to the compiler"
    Write-Host " and invoke this script again with both -Build and -Analyze. -Analyze will create a summary"
    Write-Host " of the diffs as well as disassembly files under the ./wasmjit-diff/ directory, which can be"
    Write-Host " further inspected using tools like git-diff."
    Write-Host ""
    Write-Host " Note: -Analyze depends on https://github.com/WebAssembly/wabt tools being available in PATH."
    Write-Host ""
    return
}

if ($DebugSymbols -eq $null)
{
    $DebugSymbols = $true
}

$RuntimelabDirectory = [System.IO.Path]::GetFullPath("./../../../../../", $PSScriptRoot)
if (!$RuntimelabDirectory.Replace("\", "/").EndsWith("runtimelab/"))
{
    Write-Error "The script assumes residency in the runtimelab/src/tests/nativeaot/SmokeTests/HelloWasm directory"
    exit
}

$TestProjectPath = Convert-Path $TestProject
if (!(Test-Path $TestProjectPath))
{
    Write-Error "$TestProjectPath could not be found"
    exit
}

$TestProjectName = [IO.Path]::GetFileNameWithoutExtension($TestProjectPath)
$TestProjectFileName = [IO.Path]::GetFileName($TestProjectPath)
$TestProjectDirectory = [IO.Path]::GetDirectoryName($TestProjectPath)

$RuntimeTestsDirectory = "$RuntimelabDirectory/src/tests"
$TestProjectDirectoryRelativePath = [System.IO.Path]::GetRelativePath($RuntimeTestsDirectory, $TestProjectDirectory)

$TestProjectNativeOutputDirectory = "$RuntimelabDirectory/artifacts/tests/coreclr/$OS.$Arch.$Config/$TestProjectDirectoryRelativePath/$TestProjectName/native"
$TestProjectBaseNativeOutputDirectory = "$TestProjectNativeOutputDirectory.base"
$TestProjectWasmOutput = "$TestProjectNativeOutputDirectory/$TestProjectName.wasm"
$TestProjectBaseWasmOutput = "$TestProjectBaseNativeOutputDirectory/$TestProjectName.wasm"

$TestProjectBaseNativeOutputDirectoryExists = Test-Path $TestProjectBaseNativeOutputDirectory
if ($Rebuild -and $TestProjectBaseNativeOutputDirectoryExists)
{
    Remove-Item $TestProjectBaseNativeOutputDirectory -Recurse -Force
    $TestProjectBaseNativeOutputDirectoryExists = $false
}

$BuildingBase = !$TestProjectBaseNativeOutputDirectoryExists
if ($Build -or $Rebuild)
{
    # "Touch" the project file to avoid MSBuild incrementality.
    (Get-Item $TestProjectPath).LastWriteTime = Get-Date

    if ($BuildingBase)
    {
        Write-Host "Building '$TestProjectFileName' as base. Invoke this script again to build as diff."
        Write-Host ""
    }
    else
    {
        Write-Host "'$TestProjectBaseNativeOutputDirectory' already exists, assuming it as the base."
        Write-Host ""
    }

    $UserBuildArgs = "/p:TargetOS=$OS /p:TargetArchitecture=$Arch /p:IlcConfig=$IlcConfig /p:NativeDebugSymbols=$DebugSymbols -c $Config $PassThrough"
    $BuildExpression = "dotnet build $TestProjectPath /t:BuildNativeAot /p:TestBuildMode=nativeaot $UserBuildArgs"
    Write-Verbose "Invoking: '$BuildExpression'"
    Invoke-Expression $BuildExpression
    if ($LastExitCode -ne 0)
    {
        exit
    }

    if ($BuildingBase)
    {
        Rename-Item $TestProjectNativeOutputDirectory $TestProjectBaseNativeOutputDirectory
    }
}

if ($Analyze -or $Summary)
{
    if ($BuildingBase)
    {
        Write-Host "Only the base available; no analysis to perform."
        exit
    }

    $SummaryLineRegex = [Regex]::New(" - func\[(\d+)\] size=(\d+) <(.*)>", "Compiled")
    function ParseSummary($RawSummary, $SummaryName)
    {
        Write-Host -NoNewLine "Analysing ${SummaryName}"

        $TotalCodeSize = 0
        $SummaryList = [Collections.Generic.Dictionary[string, object]]::new()
        $LineIndex = 0
        $OutputProgressInterval = [int]($RawSummary.Length / 10)
        foreach ($Line in $RawSummary)
        {
            $Matches = $SummaryLineRegex.Matches($Line).Groups
            if ($Matches.Count -eq 0)
            {
                continue
            }

            $Index = [int]$Matches[1].Value
            $Size = [int]$Matches[2].Value
            $Name = [string]$Matches[3].Value

            $TotalCodeSize += $Size
            while ($SummaryList.ContainsKey($Name))
            {
                # Sometimes we get duplicates with demangled names that wasm-objdump produces. Tolerate them.
                $Name += "*"
            }
            $SummaryList.Add($Name, @{ Index = $Index; Size = $Size })

            if ($LineIndex % $OutputProgressInterval -eq 0)
            {
                Write-Host -NoNewLine "."
            }
            $LineIndex++
        }
        Write-Host ""

        return $SummaryList, $TotalCodeSize
    }

    $BaseSummaryRaw = wasm-objdump -x -j Code $TestProjectBaseWasmOutput
    $DiffSummaryRaw = wasm-objdump -x -j Code $TestProjectWasmOutput

    $BaseSummary, $TotalBaseCodeSize = ParseSummary $BaseSummaryRaw "base"
    $DiffSummary, $TotalDiffCodeSize = ParseSummary $DiffSummaryRaw "diff"

    $NullFunc = @{ Index = -1; Size = 0 }
    $Diffs = [Collections.Generic.List[object]]::new()
    foreach ($BaseFuncKvp in $BaseSummary.GetEnumerator())
    {
        $FuncName = $BaseFuncKvp.Key
        $BaseFunc = $BaseFuncKvp.Value
        if ($DiffSummary.ContainsKey($FuncName))
        {
            $DiffFunc = $DiffSummary[$FuncName]
        }
        else
        {
            # This method only exists in the base.
            $DiffFunc = $NullFunc
        }

        $CodeSizeDelta = $DiffFunc.Size - $BaseFunc.Size
        if ($CodeSizeDelta -ne 0)
        {
            $Diffs.Add(@{ Name = $FuncName; Base = $BaseFunc; Diff = $DiffFunc; CodeSizeDelta = [double]$CodeSizeDelta })
        }
    }
    # Now add methods that only exist in the diff.
    foreach ($DiffFuncKvp in $DiffSummary.GetEnumerator())
    {
        $FuncName = $DiffFuncKvp.Key
        if (!$BaseSummary.ContainsKey($FuncName))
        {
            $BaseFunc = $NullFunc
            $DiffFunc = $DiffFuncKvp.Value

            $Diffs.Add(@{ Name = $FuncName; Base = $BaseFunc; Diff = $DiffFunc; CodeSizeDelta = [double]$DiffFunc.Size })
        }
    }

    if ($Diffs.Count -ne 0)
    {
        $DiffFileIndex = 1000 # Start from 1000 to have better alignment of the output
        foreach ($Diff in $Diffs)
        {
            $Diff["DiffFileName"] = "$DiffFileIndex.dasm"
            $DiffFileIndex++
        }

        if ($Analyze)
        {
            $AnalysisDirectory = "$PSScriptRoot/wasmjit-diff"
            if (!(Test-Path $AnalysisDirectory))
            {
                mkdir $AnalysisDirectory | Write-Verbose
            }
            # Only one analysis at a time is currently supported (i. e. subsequent runs will overwrite this directory).
            $ProjectAnalysisDirectory = "$AnalysisDirectory/$TestProjectName"
            if (!(Test-Path $ProjectAnalysisDirectory))
            {
                mkdir $ProjectAnalysisDirectory | Write-Verbose
            }

            Write-Host "Collecting full disassembly for the base..."
            $BaseWat = wasm-objdump -d $TestProjectBaseWasmOutput
            Write-Host "Collecting full disassembly for the diff..."
            $DiffWat = wasm-objdump -d $TestProjectWasmOutput

            function CreateWatIndex($Wat)
            {
                $WatSummary = [Collections.Generic.List[int]]::new()
                $DefFuncIdx = 0
                for ($Idx = 0; $Idx -lt $Wat.Length; $Idx++)
                {
                    $Line = $Wat[$Idx]
                    if ($Line.Contains("func["))
                    {
                        if ($WatSummary.Count -eq 0)
                        {
                            $FuncIdxStart = $Line.IndexOf("[") + 1
                            $FuncIdxLength = $Line.IndexOf("]") - $FuncIdxStart
                            $DefFuncIdx = [int]$Line.Substring($FuncIdxStart, $FuncIdxLength)
                        }

                        $WatSummary.Add($Idx)
                    }
                }

                return $DefFuncIdx, $WatSummary
            }

            $BaseDefFuncIndex, $BaseWatSummary = CreateWatIndex($BaseWat)
            $DiffDefFuncIndex, $DiffWatSumary = CreateWatIndex($DiffWat)

            $BaseProjectAnalysisDirectory = "$ProjectAnalysisDirectory/base"
            if (Test-Path $BaseProjectAnalysisDirectory)
            {
                Remove-Item $BaseProjectAnalysisDirectory/* -Recurse
            }
            else
            {
                mkdir $BaseProjectAnalysisDirectory | Write-Verbose
            }
            $DiffProjectAnalysisDirectory = "$ProjectAnalysisDirectory/diff"
            if (Test-Path $DiffProjectAnalysisDirectory)
            {
                Remove-Item $DiffProjectAnalysisDirectory/* -Recurse            
            }
            else
            {
                mkdir $DiffProjectAnalysisDirectory | Write-Verbose
            }

            function CreateDiffFile($DiffFileName, $FuncIndex, $IsBase)
            {
                if ($IsBase)
                {
                    $DefFuncIndex = $BaseDefFuncIndex
                    $Wat = $BaseWat
                    $WatSummary = $BaseWatSummary
                    $DiffFileDirectory = $BaseProjectAnalysisDirectory
                }
                else
                {
                    $DefFuncIndex = $DiffDefFuncIndex
                    $Wat = $DiffWat
                    $WatSummary = $DiffWatSumary
                    $DiffFileDirectory = $DiffProjectAnalysisDirectory
                }

                Write-Verbose "$DefFuncIndex, $FuncIndex"
                $WatSummaryIndex = $FuncIndex - $DefFuncIndex
                $StartTextIndex = $WatSummary[$WatSummaryIndex]
                $EndTextIndex = ($WatSummaryIndex + 1) -eq $WatSummary.Count ? $Wat.Length : $WatSummary[$WatSummaryIndex + 1]
                Write-Verbose "$StartTextIndex, $EndTextIndex"

                $StreamWriter = [IO.StreamWriter]::New("$DiffFileDirectory/$DiffFileName")
                for ($Idx = $StartTextIndex; $Idx -lt $EndTextIndex; $Idx++)
                {
                    $Line = $Wat[$Idx]

                    # Trim the leading offsets to get clean diffs.
                    if ($Idx -eq $StartTextIndex)
                    {
                        $StartLineOffset = $Line.IndexOf("func")
                    }
                    else
                    {
                        $StartLineOffset = $Line.IndexOf(":") + 1
                    }

                    $StreamWriter.WriteLine($Line.Substring($StartLineOffset))
                }
                $StreamWriter.Close()
            }

            $OutputProgressIndex = 0
            $OutputProgressInterval = [Math]::Max([int]($Diffs.Count / 10), 1)

            Write-Host -NoNewLine "Writing diff files"
            foreach ($Diff in $Diffs)
            {
                # Create files on disk for this diff.
                if ($Diff.Base.Size -ne 0)
                {
                    CreateDiffFile $Diff.DiffFileName $Diff.Base.Index -IsBase $true
                }
                if ($Diff.Diff.Size -ne 0)
                {
                    CreateDiffFile $Diff.DiffFileName $Diff.Diff.Index -IsBase $false
                }
                if ($OutputProgressIndex % $OutputProgressInterval -eq 0)
                {
                    Write-Host -NoNewLine "."
                }
                $OutputProgressIndex++
            }
            Write-Host ""
        }

        $RegressionCount = 0
        $ImprovementCount = 0
        $HaveRegressionDiffs = $false
        $HaveImprovementDiffs = $false
        $HaveBaseOnlyMethods = $false
        $HaveDiffOnlyMethods = $false
        $AverageRelativeCodeSizeDelta = 0.0
        foreach ($Diff in $Diffs)
        {
            $IsRegression = $Diff.CodeSizeDelta -gt 0
            if ($IsRegression)
            {
                $RegressionCount++
            }
            else
            {
                $ImprovementCount++
            }

            # Null methods bias the data in obvious (diff-only ones will make it '+infinity') and less obvious ways.
            # Just skip them for now.
            if ($Diff.Base.Size -eq 0)
            {
                $HaveDiffOnlyMethods = $true
                continue
            }
            if ($Diff.Diff.Size -eq 0)
            {
                $HaveBaseOnlyMethods = $true
                continue
            }

            if ($IsRegression)
            {
                $HaveRegressionDiffs = $true
            }
            else
            {
                $HaveImprovementDiffs = $true
            }

            $AverageRelativeCodeSizeDelta += $Diff.CodeSizeDelta / $Diff.Base.Size
        }

        # Note: this is intentionally different from jit-analyze, which computes the total relative delta, i. e. sums
        # all the percentages. Computing an average seems more useful (and should be considered for upstreaming). For
        # even better results, we could compute and print a histogram...
        $AverageRelativeCodeSizeDelta /= $Diffs.Count

        Write-Host ""
        Write-Host "Summary of Code Size diffs:"
        Write-Host "(Lower is better)"
        Write-Host ""

        $TotalCodeSizeDelta = $TotalDiffCodeSize - $TotalBaseCodeSize
        Write-Host "Total bytes of base: $TotalBaseCodeSize"
        Write-Host "Total bytes of diff: $TotalDiffCodeSize"
        Write-Host ("Total bytes of delta: $TotalCodeSizeDelta ({0:P} % of base)" -f ($TotalCodeSizeDelta / $TotalBaseCodeSize))

        Write-Host ("Average relative delta: {0:P}" -f $AverageRelativeCodeSizeDelta)
        Write-Host "    diff is $($TotalCodeSizeDelta -lt 0 ? 'an improvement' : 'a regression')"
        Write-Host "    average relative diff is $($AverageRelativeCodeSizeDelta -lt 0 ? 'an improvement' : 'a regression')"
        Write-Host ""

        function ShowRelativeDiffs($DiffsToShow, $Message, $ShowBaseOnlyMethods = $false, $ShowDiffOnlyMethods = $false)
        {
            Write-Host $Message

            $DiffsShown = 0
            foreach ($Diff in $DiffsToShow)
            {
                if ($DiffsShown -ge $NumberOfDiffsToShow)
                {
                    break
                }

                $IsBaseOnlyMethod = $Diff.Diff.Size -eq 0
                $IsDiffOnlyMethod = $Diff.Base.Size -eq 0
                if (($ShowBaseOnlyMethods -eq $IsBaseOnlyMethod) -and ($ShowDiffOnlyMethods -eq $IsDiffOnlyMethod))
                {
                    Write-Host ("    {0,8} ({1,6:P} of base) : {2} - {3}" -f
                        $Diff.CodeSizeDelta, ($Diff.CodeSizeDelta / $Diff.Base.Size), $Diff.DiffFileName, $Diff.Name)
                    $DiffsShown++
                }
            }
            Write-Host ""
        }

        if ($RegressionCount -ne 0)
        {
            $RegressionDiffs = $Diffs | sort { $_.CodeSizeDelta / $_.Base.Size } -Descending | where CodeSizeDelta -gt 0
            if ($HaveRegressionDiffs)
            {
                ShowRelativeDiffs $RegressionDiffs "Top method regressions (percentages):"
            }
            if ($HaveDiffOnlyMethods)
            {
                ShowRelativeDiffs $RegressionDiffs "Top methods only present in diff:" -ShowDiffOnlyMethods $true
            }
        }

        if ($ImprovementCount -ne 0)
        {
            $ImprovementDiffs = $Diffs | sort { $_.CodeSizeDelta / $_.Base.Size } | where CodeSizeDelta -lt 0
            if ($HaveImprovementDiffs)
            {
                ShowRelativeDiffs $ImprovementDiffs "Top method improvements (percentages):"
            }
            if ($HaveBaseOnlyMethods)
            {
                ShowRelativeDiffs $ImprovementDiffs "Top methods only present in base:" -ShowBaseOnlyMethods $true
            }
        }

        Write-Host "$($Diffs.Count) total methods with Code Size differences ($ImprovementCount improved, $RegressionCount regressed)"
        Write-Host ""
    }
    else
    {
        Write-Host ""
        Write-Host "No diffs found!"
        Write-Host ""
    }
}
