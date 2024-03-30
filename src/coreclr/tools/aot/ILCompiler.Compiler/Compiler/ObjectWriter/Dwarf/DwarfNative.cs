// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ILCompiler.ObjectWriter
{
    internal static class DwarfNative
    {
        public const byte DW_EH_PE_absptr = 0x00;
        public const byte DW_EH_PE_omit = 0xFF;
        public const byte DW_EH_PE_ptr = 0x00;
        public const byte DW_EH_PE_uleb128 = 0x01;
        public const byte DW_EH_PE_udata2 = 0x02;
        public const byte DW_EH_PE_udata4 = 0x03;
        public const byte DW_EH_PE_udata8 = 0x04;
        public const byte DW_EH_PE_sleb128 = 0x09;
        public const byte DW_EH_PE_sdata2 = 0x0A;
        public const byte DW_EH_PE_sdata4 = 0x0B;
        public const byte DW_EH_PE_sdata8 = 0x0C;
        public const byte DW_EH_PE_signed = 0x08;
        public const byte DW_EH_PE_pcrel = 0x10;
        public const byte DW_EH_PE_textrel = 0x20;
        public const byte DW_EH_PE_datarel = 0x30;
        public const byte DW_EH_PE_funcrel = 0x40;
        public const byte DW_EH_PE_aligned = 0x50;
        public const byte DW_EH_PE_indirect = 0x80;

        public const byte DW_CFA_nop = 0x0;
        public const byte DW_CFA_set_loc = 0x1;
        public const byte DW_CFA_advance_loc1 = 0x2;
        public const byte DW_CFA_advance_loc2 = 0x3;
        public const byte DW_CFA_advance_loc4 = 0x4;
        public const byte DW_CFA_offset_extended = 0x5;
        public const byte DW_CFA_restore_extended = 0x6;
        public const byte DW_CFA_undefined = 0x7;
        public const byte DW_CFA_same_value = 0x8;
        public const byte DW_CFA_register = 0x9;
        public const byte DW_CFA_remember_state = 0xA;
        public const byte DW_CFA_restore_state = 0xB;
        public const byte DW_CFA_def_cfa = 0xC;
        public const byte DW_CFA_def_cfa_register = 0xD;
        public const byte DW_CFA_def_cfa_offset = 0xE;
        public const byte DW_CFA_def_cfa_expression = 0xF;
        public const byte DW_CFA_expression = 0x10;
        public const byte DW_CFA_offset_extended_sf = 0x11;
        public const byte DW_CFA_def_cfa_sf = 0x12;
        public const byte DW_CFA_def_cfa_offset_sf = 0x13;
        public const byte DW_CFA_val_offset = 0x14;
        public const byte DW_CFA_val_offset_sf = 0x15;
        public const byte DW_CFA_val_expression = 0x16;
        public const byte DW_CFA_advance_loc = 0x40;
        public const byte DW_CFA_offset = 0x80;
        public const byte DW_CFA_restore = 0xC0;
        public const byte DW_CFA_GNU_window_save = 0x2D;
        public const byte DW_CFA_GNU_args_size = 0x2E;
        public const byte DW_CFA_GNU_negative_offset_extended = 0x2F;
        public const byte DW_CFA_AARCH64_negate_ra_state = 0x2D;

        public const byte DW_ATE_address                  = 0x01;
        public const byte DW_ATE_boolean                  = 0x02;
        public const byte DW_ATE_complex_float            = 0x03;
        public const byte DW_ATE_float                    = 0x04;
        public const byte DW_ATE_signed                   = 0x05;
        public const byte DW_ATE_signed_char              = 0x06;
        public const byte DW_ATE_unsigned                 = 0x07;
        public const byte DW_ATE_unsigned_char            = 0x08;
        public const byte DW_ATE_imaginary_float          = 0x09;  /* DWARF3 */
        public const byte DW_ATE_packed_decimal           = 0x0A;  /* DWARF3f */
        public const byte DW_ATE_numeric_string           = 0x0B;  /* DWARF3f */
        public const byte DW_ATE_edited                   = 0x0C;  /* DWARF3f */
        public const byte DW_ATE_signed_fixed             = 0x0D;  /* DWARF3f */
        public const byte DW_ATE_unsigned_fixed           = 0x0E;  /* DWARF3f */
        public const byte DW_ATE_decimal_float            = 0x0F;  /* DWARF3f */
        public const byte DW_ATE_UTF                      = 0x10;  /* DWARF4 */
        public const byte DW_ATE_UCS                      = 0x11;  /* DWARF5 */
        public const byte DW_ATE_ASCII                    = 0x12;  /* DWARF5 */

        public const ushort DW_TAG_array_type = 1;
        public const ushort DW_TAG_class_type = 2;
        public const ushort DW_TAG_entry_point = 3;
        public const ushort DW_TAG_enumeration_type = 4;
        public const ushort DW_TAG_formal_parameter = 5;
        public const ushort DW_TAG_imported_declaration = 8;
        public const ushort DW_TAG_label = 10;
        public const ushort DW_TAG_lexical_block = 11;
        public const ushort DW_TAG_member = 13;
        public const ushort DW_TAG_pointer_type = 15;
        public const ushort DW_TAG_reference_type = 16;
        public const ushort DW_TAG_compile_unit = 17;
        public const ushort DW_TAG_string_type = 18;
        public const ushort DW_TAG_structure_type = 19;
        public const ushort DW_TAG_subroutine_type = 21;
        public const ushort DW_TAG_typedef = 22;
        public const ushort DW_TAG_union_type = 23;
        public const ushort DW_TAG_unspecified_parameters = 24;
        public const ushort DW_TAG_variant = 25;
        public const ushort DW_TAG_common_block = 26;
        public const ushort DW_TAG_common_inclusion = 27;
        public const ushort DW_TAG_inheritance = 28;
        public const ushort DW_TAG_inlined_subroutine = 29;
        public const ushort DW_TAG_module = 30;
        public const ushort DW_TAG_ptr_to_member_type = 31;
        public const ushort DW_TAG_set_type = 32;
        public const ushort DW_TAG_subrange_type = 33;
        public const ushort DW_TAG_with_stmt = 34;
        public const ushort DW_TAG_access_declaration = 35;
        public const ushort DW_TAG_base_type = 36;
        public const ushort DW_TAG_catch_block = 37;
        public const ushort DW_TAG_const_type = 38;
        public const ushort DW_TAG_constant = 39;
        public const ushort DW_TAG_enumerator = 40;
        public const ushort DW_TAG_file_type = 41;
        public const ushort DW_TAG_friend = 42;
        public const ushort DW_TAG_namelist = 43;
        public const ushort DW_TAG_namelist_item = 44;
        public const ushort DW_TAG_packed_type = 45;
        public const ushort DW_TAG_subprogram = 46;
        public const ushort DW_TAG_template_type_param = 47;
        public const ushort DW_TAG_template_value_param = 48;
        public const ushort DW_TAG_thrown_type = 49;
        public const ushort DW_TAG_try_block = 50;
        public const ushort DW_TAG_variant_part = 51;
        public const ushort DW_TAG_variable = 52;
        public const ushort DW_TAG_volatile_type = 53;
        public const ushort DW_TAG_dwarf_procedure = 54;
        public const ushort DW_TAG_restrict_type = 55;
        public const ushort DW_TAG_interface_type = 56;
        public const ushort DW_TAG_namespace = 57;
        public const ushort DW_TAG_imported_module = 58;
        public const ushort DW_TAG_unspecified_type = 59;
        public const ushort DW_TAG_partial_unit = 60;
        public const ushort DW_TAG_imported_unit = 61;
        public const ushort DW_TAG_mutable_type = 62;
        public const ushort DW_TAG_condition = 63;
        public const ushort DW_TAG_shared_type = 64;
        public const ushort DW_TAG_type_unit = 65;
        public const ushort DW_TAG_rvalue_reference_type = 66;
        public const ushort DW_TAG_template_alias = 67;
        public const ushort DW_TAG_coarray_type = 68;
        public const ushort DW_TAG_generic_subrange = 69;
        public const ushort DW_TAG_dynamic_type = 70;
        public const ushort DW_TAG_atomic_type = 71;
        public const ushort DW_TAG_call_site = 72;
        public const ushort DW_TAG_call_site_parameter = 73;
        public const ushort DW_TAG_skeleton_unit = 74;
        public const ushort DW_TAG_immutable_type = 75;

        public const ushort DW_FORM_addr = 1;
        public const ushort DW_FORM_block2 = 3;
        public const ushort DW_FORM_block4 = 4;
        public const ushort DW_FORM_data2 = 5;
        public const ushort DW_FORM_data4 = 6;
        public const ushort DW_FORM_data8 = 7;
        public const ushort DW_FORM_string = 8;
        public const ushort DW_FORM_block = 9;
        public const ushort DW_FORM_block1 = 10;
        public const ushort DW_FORM_data1 = 11;
        public const ushort DW_FORM_flag = 12;
        public const ushort DW_FORM_sdata = 13;
        public const ushort DW_FORM_strp = 14;
        public const ushort DW_FORM_udata = 15;
        public const ushort DW_FORM_ref_addr = 16;
        public const ushort DW_FORM_ref1 = 17;
        public const ushort DW_FORM_ref2 = 18;
        public const ushort DW_FORM_ref4 = 19;
        public const ushort DW_FORM_ref8 = 20;
        public const ushort DW_FORM_ref_udata = 21;
        public const ushort DW_FORM_indirect = 22;
        public const ushort DW_FORM_sec_offset = 23;
        public const ushort DW_FORM_exprloc = 24;
        public const ushort DW_FORM_flag_present = 25;
        public const ushort DW_FORM_strx = 26;
        public const ushort DW_FORM_addrx = 27;
        public const ushort DW_FORM_ref_sup4 = 28;
        public const ushort DW_FORM_strp_sup = 29;
        public const ushort DW_FORM_data16 = 30;
        public const ushort DW_FORM_line_strp = 31;
        public const ushort DW_FORM_ref_sig8 = 32;
        public const ushort DW_FORM_implicit_const = 33;
        public const ushort DW_FORM_loclistx = 34;
        public const ushort DW_FORM_rnglistx = 35;
        public const ushort DW_FORM_ref_sup8 = 36;
        public const ushort DW_FORM_strx1 = 37;
        public const ushort DW_FORM_strx2 = 38;
        public const ushort DW_FORM_strx3 = 39;
        public const ushort DW_FORM_strx4 = 40;
        public const ushort DW_FORM_addrx1 = 41;
        public const ushort DW_FORM_addrx2 = 42;
        public const ushort DW_FORM_addrx3 = 43;
        public const ushort DW_FORM_addrx4 = 44;

        public const ushort DW_AT_sibling = 1;
        public const ushort DW_AT_location = 2;
        public const ushort DW_AT_name = 3;
        public const ushort DW_AT_ordering = 9;
        public const ushort DW_AT_subscr_data = 10;
        public const ushort DW_AT_byte_size = 11;
        public const ushort DW_AT_bit_offset = 12;
        public const ushort DW_AT_bit_size = 13;
        public const ushort DW_AT_element_list = 15;
        public const ushort DW_AT_stmt_list = 16;
        public const ushort DW_AT_low_pc = 17;
        public const ushort DW_AT_high_pc = 18;
        public const ushort DW_AT_language = 19;
        public const ushort DW_AT_member = 20;
        public const ushort DW_AT_discr = 21;
        public const ushort DW_AT_discr_value = 22;
        public const ushort DW_AT_visibility = 23;
        public const ushort DW_AT_import = 24;
        public const ushort DW_AT_string_length = 25;
        public const ushort DW_AT_common_reference = 26;
        public const ushort DW_AT_comp_dir = 27;
        public const ushort DW_AT_const_value = 28;
        public const ushort DW_AT_containing_type = 29;
        public const ushort DW_AT_default_value = 30;
        public const ushort DW_AT_inline = 32;
        public const ushort DW_AT_is_optional = 33;
        public const ushort DW_AT_lower_bound = 34;
        public const ushort DW_AT_producer = 37;
        public const ushort DW_AT_prototyped = 39;
        public const ushort DW_AT_return_addr = 42;
        public const ushort DW_AT_start_scope = 44;
        public const ushort DW_AT_bit_stride = 46;
        public const ushort DW_AT_upper_bound = 47;
        public const ushort DW_AT_abstract_origin = 49;
        public const ushort DW_AT_accessibility = 50;
        public const ushort DW_AT_address_class = 51;
        public const ushort DW_AT_artificial = 52;
        public const ushort DW_AT_base_types = 53;
        public const ushort DW_AT_calling_convention = 54;
        public const ushort DW_AT_count = 55;
        public const ushort DW_AT_data_member_location = 56;
        public const ushort DW_AT_decl_column = 57;
        public const ushort DW_AT_decl_file = 58;
        public const ushort DW_AT_decl_line = 59;
        public const ushort DW_AT_declaration = 60;
        public const ushort DW_AT_discr_list = 61;
        public const ushort DW_AT_encoding = 62;
        public const ushort DW_AT_external = 63;
        public const ushort DW_AT_frame_base = 64;
        public const ushort DW_AT_friend = 65;
        public const ushort DW_AT_identifier_case = 66;
        public const ushort DW_AT_macro_info = 67;
        public const ushort DW_AT_namelist_item = 68;
        public const ushort DW_AT_priority = 69;
        public const ushort DW_AT_segment = 70;
        public const ushort DW_AT_specification = 71;
        public const ushort DW_AT_static_link = 72;
        public const ushort DW_AT_type = 73;
        public const ushort DW_AT_use_location = 74;
        public const ushort DW_AT_variable_parameter = 75;
        public const ushort DW_AT_virtuality = 76;
        public const ushort DW_AT_vtable_elem_location = 77;
        public const ushort DW_AT_allocated = 78;
        public const ushort DW_AT_associated = 79;
        public const ushort DW_AT_data_location = 80;
        public const ushort DW_AT_byte_stride = 81;
        public const ushort DW_AT_entry_pc = 82;
        public const ushort DW_AT_use_UTF8 = 83;
        public const ushort DW_AT_extension = 84;
        public const ushort DW_AT_ranges = 85;
        public const ushort DW_AT_trampoline = 86;
        public const ushort DW_AT_call_column = 87;
        public const ushort DW_AT_call_file = 88;
        public const ushort DW_AT_call_line = 89;
        public const ushort DW_AT_description = 90;
        public const ushort DW_AT_binary_scale = 91;
        public const ushort DW_AT_decimal_scale = 92;
        public const ushort DW_AT_small = 93;
        public const ushort DW_AT_decimal_sign = 94;
        public const ushort DW_AT_digit_count = 95;
        public const ushort DW_AT_picture_string = 96;
        public const ushort DW_AT_mutable = 97;
        public const ushort DW_AT_threads_scaled = 98;
        public const ushort DW_AT_explicit = 99;
        public const ushort DW_AT_object_pointer = 100;
        public const ushort DW_AT_endianity = 101;
        public const ushort DW_AT_elemental = 102;
        public const ushort DW_AT_pure = 103;
        public const ushort DW_AT_recursive = 104;
        public const ushort DW_AT_signature = 105;
        public const ushort DW_AT_main_subprogram = 106;
        public const ushort DW_AT_data_bit_offset = 107;
        public const ushort DW_AT_const_expr = 108;
        public const ushort DW_AT_enum_class = 109;
        public const ushort DW_AT_linkage_name = 110;
        public const ushort DW_AT_string_length_bit_size = 111;
        public const ushort DW_AT_string_length_byte_size = 112;
        public const ushort DW_AT_rank = 113;
        public const ushort DW_AT_str_offsets_base = 114;
        public const ushort DW_AT_addr_base = 115;
        public const ushort DW_AT_rnglists_base = 116;
        public const ushort DW_AT_dwo_id = 117;
        public const ushort DW_AT_dwo_name = 118;
        public const ushort DW_AT_reference = 119;
        public const ushort DW_AT_rvalue_reference = 120;
        public const ushort DW_AT_macros = 121;
        public const ushort DW_AT_call_all_calls = 122;
        public const ushort DW_AT_call_all_source_calls = 123;
        public const ushort DW_AT_call_all_tail_calls = 124;
        public const ushort DW_AT_call_return_pc = 125;
        public const ushort DW_AT_call_value = 126;
        public const ushort DW_AT_call_origin = 127;
        public const ushort DW_AT_call_parameter = 128;
        public const ushort DW_AT_call_pc = 129;
        public const ushort DW_AT_call_tail_call = 130;
        public const ushort DW_AT_call_target = 131;
        public const ushort DW_AT_call_target_clobbered = 132;
        public const ushort DW_AT_call_data_location = 133;
        public const ushort DW_AT_call_data_value = 134;
        public const ushort DW_AT_noreturn = 135;
        public const ushort DW_AT_alignment = 136;
        public const ushort DW_AT_export_symbols = 137;
        public const ushort DW_AT_deleted = 138;
        public const ushort DW_AT_defaulted = 139;
        public const ushort DW_AT_loclists_base = 140;

        public const byte DW_OP_addr = 3;
        public const byte DW_OP_deref = 6;
        public const byte DW_OP_const1u = 8;
        public const byte DW_OP_const1s = 9;
        public const byte DW_OP_const2u = 10;
        public const byte DW_OP_const2s = 11;
        public const byte DW_OP_const4u = 12;
        public const byte DW_OP_const4s = 13;
        public const byte DW_OP_const8u = 14;
        public const byte DW_OP_const8s = 15;
        public const byte DW_OP_constu = 16;
        public const byte DW_OP_consts = 17;
        public const byte DW_OP_dup = 18;
        public const byte DW_OP_drop = 19;
        public const byte DW_OP_over = 20;
        public const byte DW_OP_pick = 21;
        public const byte DW_OP_swap = 22;
        public const byte DW_OP_rot = 23;
        public const byte DW_OP_xderef = 24;
        public const byte DW_OP_abs = 25;
        public const byte DW_OP_and = 26;
        public const byte DW_OP_div = 27;
        public const byte DW_OP_minus = 28;
        public const byte DW_OP_mod = 29;
        public const byte DW_OP_mul = 30;
        public const byte DW_OP_neg = 31;
        public const byte DW_OP_not = 32;
        public const byte DW_OP_or = 33;
        public const byte DW_OP_plus = 34;
        public const byte DW_OP_plus_uconst = 35;
        public const byte DW_OP_shl = 36;
        public const byte DW_OP_shr = 37;
        public const byte DW_OP_shra = 38;
        public const byte DW_OP_xor = 39;
        public const byte DW_OP_bra = 40;
        public const byte DW_OP_eq = 41;
        public const byte DW_OP_ge = 42;
        public const byte DW_OP_gt = 43;
        public const byte DW_OP_le = 44;
        public const byte DW_OP_lt = 45;
        public const byte DW_OP_ne = 46;
        public const byte DW_OP_skip = 47;
        public const byte DW_OP_lit0 = 48;
        public const byte DW_OP_lit1 = 49;
        public const byte DW_OP_lit2 = 50;
        public const byte DW_OP_lit3 = 51;
        public const byte DW_OP_lit4 = 52;
        public const byte DW_OP_lit5 = 53;
        public const byte DW_OP_lit6 = 54;
        public const byte DW_OP_lit7 = 55;
        public const byte DW_OP_lit8 = 56;
        public const byte DW_OP_lit9 = 57;
        public const byte DW_OP_lit10 = 58;
        public const byte DW_OP_lit11 = 59;
        public const byte DW_OP_lit12 = 60;
        public const byte DW_OP_lit13 = 61;
        public const byte DW_OP_lit14 = 62;
        public const byte DW_OP_lit15 = 63;
        public const byte DW_OP_lit16 = 64;
        public const byte DW_OP_lit17 = 65;
        public const byte DW_OP_lit18 = 66;
        public const byte DW_OP_lit19 = 67;
        public const byte DW_OP_lit20 = 68;
        public const byte DW_OP_lit21 = 69;
        public const byte DW_OP_lit22 = 70;
        public const byte DW_OP_lit23 = 71;
        public const byte DW_OP_lit24 = 72;
        public const byte DW_OP_lit25 = 73;
        public const byte DW_OP_lit26 = 74;
        public const byte DW_OP_lit27 = 75;
        public const byte DW_OP_lit28 = 76;
        public const byte DW_OP_lit29 = 77;
        public const byte DW_OP_lit30 = 78;
        public const byte DW_OP_lit31 = 79;
        public const byte DW_OP_reg0 = 80;
        public const byte DW_OP_reg1 = 81;
        public const byte DW_OP_reg2 = 82;
        public const byte DW_OP_reg3 = 83;
        public const byte DW_OP_reg4 = 84;
        public const byte DW_OP_reg5 = 85;
        public const byte DW_OP_reg6 = 86;
        public const byte DW_OP_reg7 = 87;
        public const byte DW_OP_reg8 = 88;
        public const byte DW_OP_reg9 = 89;
        public const byte DW_OP_reg10 = 90;
        public const byte DW_OP_reg11 = 91;
        public const byte DW_OP_reg12 = 92;
        public const byte DW_OP_reg13 = 93;
        public const byte DW_OP_reg14 = 94;
        public const byte DW_OP_reg15 = 95;
        public const byte DW_OP_reg16 = 96;
        public const byte DW_OP_reg17 = 97;
        public const byte DW_OP_reg18 = 98;
        public const byte DW_OP_reg19 = 99;
        public const byte DW_OP_reg20 = 100;
        public const byte DW_OP_reg21 = 101;
        public const byte DW_OP_reg22 = 102;
        public const byte DW_OP_reg23 = 103;
        public const byte DW_OP_reg24 = 104;
        public const byte DW_OP_reg25 = 105;
        public const byte DW_OP_reg26 = 106;
        public const byte DW_OP_reg27 = 107;
        public const byte DW_OP_reg28 = 108;
        public const byte DW_OP_reg29 = 109;
        public const byte DW_OP_reg30 = 110;
        public const byte DW_OP_reg31 = 111;
        public const byte DW_OP_breg0 = 112;
        public const byte DW_OP_breg1 = 113;
        public const byte DW_OP_breg2 = 114;
        public const byte DW_OP_breg3 = 115;
        public const byte DW_OP_breg4 = 116;
        public const byte DW_OP_breg5 = 117;
        public const byte DW_OP_breg6 = 118;
        public const byte DW_OP_breg7 = 119;
        public const byte DW_OP_breg8 = 120;
        public const byte DW_OP_breg9 = 121;
        public const byte DW_OP_breg10 = 122;
        public const byte DW_OP_breg11 = 123;
        public const byte DW_OP_breg12 = 124;
        public const byte DW_OP_breg13 = 125;
        public const byte DW_OP_breg14 = 126;
        public const byte DW_OP_breg15 = 127;
        public const byte DW_OP_breg16 = 128;
        public const byte DW_OP_breg17 = 129;
        public const byte DW_OP_breg18 = 130;
        public const byte DW_OP_breg19 = 131;
        public const byte DW_OP_breg20 = 132;
        public const byte DW_OP_breg21 = 133;
        public const byte DW_OP_breg22 = 134;
        public const byte DW_OP_breg23 = 135;
        public const byte DW_OP_breg24 = 136;
        public const byte DW_OP_breg25 = 137;
        public const byte DW_OP_breg26 = 138;
        public const byte DW_OP_breg27 = 139;
        public const byte DW_OP_breg28 = 140;
        public const byte DW_OP_breg29 = 141;
        public const byte DW_OP_breg30 = 142;
        public const byte DW_OP_breg31 = 143;
        public const byte DW_OP_regx = 144;
        public const byte DW_OP_fbreg = 145;
        public const byte DW_OP_bregx = 146;
        public const byte DW_OP_piece = 147;
        public const byte DW_OP_deref_size = 148;
        public const byte DW_OP_xderef_size = 149;
        public const byte DW_OP_nop = 150;
        public const byte DW_OP_push_object_address = 151;
        public const byte DW_OP_call2 = 152;
        public const byte DW_OP_call4 = 153;
        public const byte DW_OP_call_ref = 154;
        public const byte DW_OP_form_tls_address = 155;
        public const byte DW_OP_call_frame_cfa = 156;
        public const byte DW_OP_bit_piece = 157;
        public const byte DW_OP_implicit_value = 158;
        public const byte DW_OP_stack_value = 159;
        public const byte DW_OP_implicit_pointer = 160;
        public const byte DW_OP_addrx = 161;
        public const byte DW_OP_constx = 162;
        public const byte DW_OP_entry_value = 163;
        public const byte DW_OP_const_type = 164;
        public const byte DW_OP_regval_type = 165;
        public const byte DW_OP_deref_type = 166;
        public const byte DW_OP_xderef_type = 167;
        public const byte DW_OP_convert = 168;
        public const byte DW_OP_reinterpret = 169;

        public const byte DW_CHILDREN_no = 0;
        public const byte DW_CHILDREN_yes = 1;

        public const byte DW_LNS_copy = 1;
        public const byte DW_LNS_advance_pc = 2;
        public const byte DW_LNS_advance_line = 3;
        public const byte DW_LNS_set_file = 4;
        public const byte DW_LNS_set_column = 5;
        public const byte DW_LNS_negate_stmt = 6;
        public const byte DW_LNS_set_basic_block = 7;
        public const byte DW_LNS_const_add_pc = 8;
        public const byte DW_LNS_fixed_advance_pc = 9;
        public const byte DW_LNS_set_prologue_end = 10;
        public const byte DW_LNS_set_epilogue_begin = 11;
        public const byte DW_LNS_set_isa = 12;

        public const byte DW_LNE_end_sequence = 1;
        public const byte DW_LNE_set_address = 2;
        public const byte DW_LNE_define_file = 3;
        public const byte DW_LNE_set_discriminator = 4;

        public const byte DW_UT_compile = 1;
        public const byte DW_UT_type = 2;
        public const byte DW_UT_partial = 3;
        public const byte DW_UT_skeleton = 4;
        public const byte DW_UT_split_compile = 5;
        public const byte DW_UT_split_type = 6;

        public const ushort DW_LANG_C89 = 1;
        public const ushort DW_LANG_C = 2;
        public const ushort DW_LANG_Ada83 = 3;
        public const ushort DW_LANG_C_plus_plus = 4;
        public const ushort DW_LANG_Cobol74 = 5;
        public const ushort DW_LANG_Cobol85 = 6;
        public const ushort DW_LANG_Fortran77 = 7;
        public const ushort DW_LANG_Fortran90 = 8;
        public const ushort DW_LANG_Pascal83 = 9;
        public const ushort DW_LANG_Modula2 = 10;
        public const ushort DW_LANG_Java = 11;
        public const ushort DW_LANG_C99 = 12;
        public const ushort DW_LANG_Ada95 = 13;
        public const ushort DW_LANG_Fortran95 = 14;
        public const ushort DW_LANG_PLI = 15;
        public const ushort DW_LANG_ObjC = 16;
        public const ushort DW_LANG_ObjC_plus_plus = 17;
        public const ushort DW_LANG_UPC = 18;
        public const ushort DW_LANG_D = 19;
        public const ushort DW_LANG_Python = 20;
        public const ushort DW_LANG_OpenCL = 21;
        public const ushort DW_LANG_Go = 22;
        public const ushort DW_LANG_Modula3 = 23;
        public const ushort DW_LANG_Haskel = 24;
        public const ushort DW_LANG_C_plus_plus_03 = 25;
        public const ushort DW_LANG_C_plus_plus_11 = 26;
        public const ushort DW_LANG_OCaml = 27;
        public const ushort DW_LANG_Rust = 28;
        public const ushort DW_LANG_C11 = 29;
        public const ushort DW_LANG_Swift = 30;
        public const ushort DW_LANG_Julia = 31;
        public const ushort DW_LANG_Dylan = 32;
        public const ushort DW_LANG_C_plus_plus_14 = 33;
        public const ushort DW_LANG_Fortran03 = 34;
        public const ushort DW_LANG_Fortran08 = 35;
        public const ushort DW_LANG_RenderScript = 36;
        public const ushort DW_LANG_BLISS = 37;
    }
}
