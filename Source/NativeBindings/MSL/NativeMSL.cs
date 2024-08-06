using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using SPIRVCross.NET.MSL;

namespace SPIRVCross.NET.NativeBindings;

#pragma warning disable 1591

public static partial class MSLCompiler
{
    const string LibName = "spirv-cross";

    /*
    [LibraryImport(LibName, EntryPoint = "spvc_msl_shader_interface_var_init_2")]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial void spvc_msl_shader_interface_var_init(MSLShaderInterfaceVar* var);

    [LibraryImport(LibName, EntryPoint = "spvc_msl_resource_binding_init_2")]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial void spvc_msl_resource_binding_init(MSLResourceBinding* binding);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial void spvc_msl_constexpr_sampler_init(MSLConstexprSampler* sampler);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial void spvc_msl_sampler_ycbcr_conversion_init(MSLSamplerYCBCRConversion* conv);
    */

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial CBool spvc_compiler_msl_is_rasterization_disabled(Compiler* compiler);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial CBool spvc_compiler_msl_needs_swizzle_buffer(Compiler* compiler);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial CBool spvc_compiler_msl_needs_buffer_size_buffer(Compiler* compiler);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial CBool spvc_compiler_msl_needs_output_buffer(Compiler* compiler);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial CBool spvc_compiler_msl_needs_patch_output_buffer(Compiler* compiler);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial CBool spvc_compiler_msl_needs_input_threadgroup_mem(Compiler* compiler);

    [LibraryImport(LibName, EntryPoint = "spvc_compiler_msl_add_resource_binding_2")]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_msl_add_resource_binding(Compiler* compiler, in ResourceBinding binding);

    [LibraryImport(LibName, EntryPoint = "spvc_compiler_msl_add_shader_input_2")]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_msl_add_shader_input(Compiler* compiler, in ShaderInterfaceVar input);

    [LibraryImport(LibName, EntryPoint = "spvc_compiler_msl_add_shader_output_2")]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_msl_add_shader_output(Compiler* compiler, in ShaderInterfaceVar output);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_msl_add_discrete_descriptor_set(Compiler* compiler, uint desc_set);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_msl_set_argument_buffer_device_address_space(Compiler* compiler, uint desc_set, CBool device_address);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial CBool spvc_compiler_msl_is_shader_input_used(Compiler* compiler, uint location);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial CBool spvc_compiler_msl_is_shader_output_used(Compiler* compiler, uint location);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial CBool spvc_compiler_msl_is_resource_used(Compiler* compiler, ExecutionModel model, uint set, uint binding);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_msl_remap_constexpr_sampler(Compiler* compiler, VariableID id, in ConstexprSampler sampler);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_msl_remap_constexpr_sampler_by_binding(Compiler* compiler, uint desc_set, uint binding, in ConstexprSampler sampler);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_msl_remap_constexpr_sampler_ycbcr(Compiler* compiler, VariableID id, in ConstexprSampler sampler, in SamplerYCBCRConversion conv);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_msl_remap_constexpr_sampler_by_binding_ycbcr(Compiler* compiler, uint desc_set, uint binding, in ConstexprSampler sampler, in SamplerYCBCRConversion conv);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_msl_set_fragment_output_components(Compiler* compiler, uint location, uint components);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial uint spvc_compiler_msl_get_automatic_resource_binding(Compiler* compiler, VariableID id);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial uint spvc_compiler_msl_get_automatic_resource_binding_secondary(Compiler* compiler, VariableID id);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_msl_add_dynamic_buffer(Compiler* compiler, uint desc_set, uint binding, uint index);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_msl_add_inline_uniform_block(Compiler* compiler, uint desc_set, uint binding);

    [LibraryImport(LibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_msl_set_combined_sampler_suffix(Compiler* compiler, string suffix);

    [LibraryImport(LibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial byte* spvc_compiler_msl_get_combined_sampler_suffix(Compiler* compiler);
}