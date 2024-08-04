using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET.MSL;

using static Native.MSLCompiler;

/// <summary>
/// <inheritdoc/>
/// <para>Outputs cross-compiled MSL when calling <see cref="Compile()"/>.</para>
/// </summary>
public unsafe partial class MSLCrossCompiler : Compiler
{
    /// <summary>
    /// MSL-specific options to compile with.
    /// </summary>
    public MSLCompilerOptions metalOptions = new();

    internal unsafe MSLCrossCompiler(Context context, Native.Compiler* compiler) : base(context, compiler) { }

    /// <inheritdoc/>
    public override string Compile()
    {
        metalOptions.Apply(context, optionsPtr);
        return base.Compile();
    }

    public bool IsRasterizationDisabled() 
        => spvc_compiler_msl_is_rasterization_disabled(compiler);
    
    public bool NeedsSwizzleBuffer() 
        => spvc_compiler_msl_needs_swizzle_buffer(compiler);

    public bool NeedsBufferSizeBuffer() 
        => spvc_compiler_msl_needs_buffer_size_buffer(compiler);
    
    public bool NeedsOutputBuffer() 
        => spvc_compiler_msl_needs_output_buffer(compiler);
    
    public bool NeedsPatchOutputBuffer() 
        => spvc_compiler_msl_needs_patch_output_buffer(compiler);
    
    public bool NeedsInputThreadgroupMem() 
        => spvc_compiler_msl_needs_input_threadgroup_mem(compiler);
    
    public void AddResourceBinding(in ResourceBinding binding) 
        => context.Throw(spvc_compiler_msl_add_resource_binding(compiler, binding));
    
    public void AddShaderInput(in ShaderInterfaceVar input)
        => context.Throw(spvc_compiler_msl_add_shader_input(compiler, input));
    
    public void AddShaderOutput(in ShaderInterfaceVar output) 
        => context.Throw(spvc_compiler_msl_add_shader_output(compiler, output));
    
    public void AddDiscreteDescriptorSet(uint desc_set) 
        => context.Throw(spvc_compiler_msl_add_discrete_descriptor_set(compiler, desc_set));
    
    public void SetArgumentBufferDeviceAddressSpace(uint desc_set, bool device_address)
        => context.Throw(spvc_compiler_msl_set_argument_buffer_device_address_space(compiler, desc_set, device_address));
    
    public bool IsShaderInputUsed(uint location) 
        => spvc_compiler_msl_is_shader_input_used(compiler, location);
    
    public bool IsShaderOutputUsed(uint location) 
        => spvc_compiler_msl_is_shader_output_used(compiler, location);
    
    public bool IsResourceUsed(ExecutionModel model, uint set, uint binding) 
        => spvc_compiler_msl_is_resource_used(compiler, model, set, binding);
    
    public void RemapConstexprSampler(VariableID id, in ConstexprSampler sampler) 
        => context.Throw(spvc_compiler_msl_remap_constexpr_sampler(compiler, id, sampler));
    
    public void RemapConstexprSamplerByBinding(uint desc_set, uint binding, in ConstexprSampler sampler)
        => context.Throw(spvc_compiler_msl_remap_constexpr_sampler_by_binding(compiler, desc_set, binding, sampler));
    
    public void RemapConstexprSamplerYCBCR(VariableID id, in ConstexprSampler sampler, in SamplerYCBCRConversion conv)
        => context.Throw(spvc_compiler_msl_remap_constexpr_sampler_ycbcr(compiler, id, sampler, conv));
    
    public void RemapConstexprSamplerByBindingYCBCR(uint desc_set, uint binding, in ConstexprSampler sampler, in SamplerYCBCRConversion conv)
        => context.Throw(spvc_compiler_msl_remap_constexpr_sampler_by_binding_ycbcr(compiler, desc_set, binding, sampler, conv));
    
    public void SetFragmentOutputComponents(uint location, uint components)
        => context.Throw(spvc_compiler_msl_set_fragment_output_components(compiler, location, components));
    
    public uint GetAutomaticResourceBinding(VariableID id)
        => spvc_compiler_msl_get_automatic_resource_binding(compiler, id);
    
    public uint GetAutomaticResourceBindingSecondary(VariableID id)
        => spvc_compiler_msl_get_automatic_resource_binding_secondary(compiler, id);
    
    public void AddDynamicBuffer(uint desc_set, uint binding, uint index)
        => context.Throw(spvc_compiler_msl_add_dynamic_buffer(compiler, desc_set, binding, index));
    
    public void AddInlineUniformBlock(uint desc_set, uint binding)
        => context.Throw(spvc_compiler_msl_add_inline_uniform_block(compiler, desc_set, binding));
    
    public void SetCombinedSamplerSuffix(string suffix)
        => context.Throw(spvc_compiler_msl_set_combined_sampler_suffix(compiler, suffix));
    
    public string GetCombinedSamplerSuffix()
        => Marshal.PtrToStringUTF8((IntPtr)spvc_compiler_msl_get_combined_sampler_suffix(compiler)) ?? "";
}
