using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET.Native;

#pragma warning disable 1591

public partial struct Compiler
{
    const string LibName = NativeLibrary.LibraryName;

    /* Maps directly to C++ API. */
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial uint spvc_compiler_get_current_id_bound(Compiler* compiler);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_create_compiler_options(Compiler* compiler, CompilerOptions** options);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_options_set_bool(CompilerOptions* options, CompilerOption option, CBool value);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_options_set_uint(CompilerOptions* options, CompilerOption option, uint value);

    /* Set compiler options. */
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_install_compiler_options(Compiler* compiler, CompilerOptions* options);

    /* Compile IR into a string. *source is owned by the context, and caller must not free it themselves. */
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_compile(Compiler* compiler, byte** source);

    /* Maps to C++ API. */
    [LibraryImport(LibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_add_header_line(Compiler* compiler, string line);

    [LibraryImport(LibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_require_extension(Compiler* compiler, string ext);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial nuint spvc_compiler_get_num_required_extensions(Compiler* compiler);

    [LibraryImport(LibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial byte* spvc_compiler_get_required_extension(Compiler* compiler, nuint index);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_flatten_buffer_block(Compiler* compiler, VariableID id);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial CBool spvc_compiler_variable_is_depth_or_compare(Compiler* compiler, VariableID id);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_mask_stage_output_by_location(Compiler* compiler, uint location, uint component);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_mask_stage_output_by_builtin(Compiler* compiler, BuiltIn builtin);

    /*
     * Decorations.
     * Maps to C++ API.
     */
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial void spvc_compiler_set_decoration(Compiler* compiler, ID id, Decoration decoration, uint argument);

    [LibraryImport(LibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial void spvc_compiler_set_decoration_string(Compiler* compiler, ID id, Decoration decoration, string argument);

    [LibraryImport(LibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial void spvc_compiler_set_name(Compiler* compiler, ID id, string argument);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial void spvc_compiler_set_member_decoration(Compiler* compiler, TypeID id, uint member_index, Decoration decoration, uint argument);

    [LibraryImport(LibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial void spvc_compiler_set_member_decoration_string(Compiler* compiler, TypeID id, uint member_index, Decoration decoration, string argument);
    
    [LibraryImport(LibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial void spvc_compiler_set_member_name(Compiler* compiler, TypeID id, uint member_index, string argument);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial void spvc_compiler_unset_decoration(Compiler* compiler, ID id, Decoration decoration);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial void spvc_compiler_unset_member_decoration(Compiler* compiler, TypeID id, uint member_index, Decoration decoration);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial CBool spvc_compiler_has_decoration(Compiler* compiler, ID id, Decoration decoration);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial CBool spvc_compiler_has_member_decoration(Compiler* compiler, TypeID id, uint member_index, Decoration decoration);
    
    [LibraryImport(LibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial byte* spvc_compiler_get_name(Compiler* compiler, ID id);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial uint spvc_compiler_get_decoration(Compiler* compiler, ID id, Decoration decoration);
    
    [LibraryImport(LibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial byte* spvc_compiler_get_decoration_string(Compiler* compiler, ID id, Decoration decoration);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial uint spvc_compiler_get_member_decoration(Compiler* compiler, TypeID id, uint member_index, Decoration decoration);
    
    [LibraryImport(LibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial byte* spvc_compiler_get_member_decoration_string(Compiler* compiler, TypeID id, uint member_index, Decoration decoration);

    [LibraryImport(LibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial byte* spvc_compiler_get_member_name(Compiler* compiler, TypeID id, uint member_index);

/*
 * Entry points.
 * Maps to C++ API.
 */
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_get_entry_points(Compiler* compiler, EntryPoint** entry_points, out nuint num_entry_points);
    
    [LibraryImport(LibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_set_entry_point(Compiler* compiler, string name, ExecutionModel model);
    
    [LibraryImport(LibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_rename_entry_point(Compiler* compiler, string old_name, string new_name, ExecutionModel model);
    
    [LibraryImport(LibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial byte* spvc_compiler_get_cleansed_entry_point_name(Compiler* compiler, string name, ExecutionModel model);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial void spvc_compiler_set_execution_mode(Compiler* compiler, ExecutionMode mode);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial void spvc_compiler_unset_execution_mode(Compiler* compiler, ExecutionMode mode);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial void spvc_compiler_set_execution_mode_with_arguments(Compiler* compiler, ExecutionMode mode, uint arg0, uint arg1, uint arg2);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_get_execution_modes(Compiler* compiler, ExecutionMode** modes, out nuint num_modes);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial uint spvc_compiler_get_execution_mode_argument(Compiler* compiler, ExecutionMode mode);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial uint spvc_compiler_get_execution_mode_argument_by_index(Compiler* compiler, ExecutionMode mode, uint index);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial ExecutionModel spvc_compiler_get_execution_model(Compiler* compiler);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial void spvc_compiler_update_active_builtins(Compiler* compiler);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial CBool spvc_compiler_has_active_builtin(Compiler* compiler, BuiltIn builtin, StorageClass storage);

/*
 * Type query interface.
 * Maps to C++ API, except it's read-only.
 */
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Type* spvc_compiler_get_type_handle(Compiler* compiler, TypeID id);

    /*
     * Buffer layout query.
     * Maps to C++ API.
     */
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_get_declared_struct_size(Compiler* compiler, Type* struct_type, out nuint size);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_get_declared_struct_size_runtime_array(Compiler* compiler, Type* struct_type, nuint array_size, out nuint size);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_get_declared_struct_member_size(Compiler* compiler, Type* type, uint index, out nuint size);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_type_struct_member_offset(Compiler* compiler, Type* type, uint index, out uint offset);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_type_struct_member_array_stride(Compiler* compiler, Type* type, uint index, out uint stride);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_type_struct_member_matrix_stride(Compiler* compiler, Type* type, uint index, out uint stride);

/*
 * Workaround helper functions.
 * Maps to C++ API.
 */
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_build_dummy_sampler_for_combined_images(Compiler* compiler, out VariableID id);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_build_combined_image_samplers(Compiler* compiler);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_get_combined_image_samplers(Compiler* compiler, CombinedImageSampler** samplers, out nuint num_samplers);

/*
 * Constants
 * Maps to C++ API.
 */
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_get_specialization_constants(Compiler* compiler, SpecializationConstant** constants, out nuint num_constants);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Constant* spvc_compiler_get_constant_handle(Compiler* compiler, ConstantID id);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial ConstantID spvc_compiler_get_work_group_size_specialization_constants(Compiler* compiler, out SpecializationConstant x, out SpecializationConstant y, out SpecializationConstant z);

/*
 * Buffer ranges
 * Maps to C++ API.
 */
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_get_active_buffer_ranges(Compiler* compiler, VariableID id, BufferRange** ranges, out nuint num_ranges);

/*
 * Misc reflection
 * Maps to C++ API.
 */
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial CBool spvc_compiler_get_binary_offset_for_decoration(Compiler* compiler, VariableID id, Decoration decoration, out uint word_offset);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_get_declared_capabilities(Compiler* compiler, Capability** capabilities, out nuint num_capabilities);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_get_declared_extensions(Compiler* compiler, byte*** extensions, out nuint num_extensions);

    [LibraryImport(LibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial byte* spvc_compiler_get_remapped_declared_block_name(Compiler* compiler, VariableID id);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_get_buffer_block_decorations(Compiler* compiler, VariableID id, Decoration** decorations, out nuint num_decorations);

    /*
     * Reflect resources.
     * Maps almost 1:1 to C++ API.
     */

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_get_active_interface_variables(Compiler* compiler, Set** set);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_set_enabled_interface_variables(Compiler* compiler, Set* set);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_create_shader_resources(Compiler* compiler, Resources** resources);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_create_shader_resources_for_active_variables(Compiler* compiler, Resources** resources, Set* active);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial CBool spvc_compiler_buffer_is_hlsl_counter_buffer(Compiler* compiler, VariableID id);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial CBool spvc_compiler_buffer_get_hlsl_counter_buffer(Compiler* compiler, VariableID id, out VariableID counter_id);
}