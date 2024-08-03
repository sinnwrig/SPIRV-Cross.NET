using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET.MSL;

using static Native.MSLCompiler;

public unsafe partial class MSLCrossCompiler : Compiler
{
    public MSLCompilerOptions metalOptions = new();

    internal unsafe MSLCrossCompiler(Context context, Native.Compiler* compiler) : base(context, compiler) { }

    /* Compile IR into a string. *source is owned by the context, and caller must not free it themselves. */
    public string Compile()
    {
        metalOptions.Apply(parent, optionsPtr);
        return base.Compile();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsRasterizationDisabled() 
        => spvc_compiler_msl_is_rasterization_disabled(compiler);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]    
    public bool NeedsSwizzleBuffer() 
        => spvc_compiler_msl_needs_swizzle_buffer(compiler);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NeedsBufferSizeBuffer() 
        => spvc_compiler_msl_needs_buffer_size_buffer(compiler);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NeedsOutputBuffer() 
        => spvc_compiler_msl_needs_output_buffer(compiler);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NeedsPatchOutputBuffer() 
        => spvc_compiler_msl_needs_patch_output_buffer(compiler);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool NeedsInputThreadgroupMem() 
        => spvc_compiler_msl_needs_input_threadgroup_mem(compiler);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddResourceBinding(in ResourceBinding binding) 
        => parent.Throw(spvc_compiler_msl_add_resource_binding(compiler, binding));
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddShaderInput(in ShaderInterfaceVar input)
        => parent.Throw(spvc_compiler_msl_add_shader_input(compiler, input));
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddShaderOutput(in ShaderInterfaceVar output) 
        => parent.Throw(spvc_compiler_msl_add_shader_output(compiler, output));
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddDiscreteDescriptorSet(uint desc_set) 
        => parent.Throw(spvc_compiler_msl_add_discrete_descriptor_set(compiler, desc_set));
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetArgumentBufferDeviceAddressSpace(uint desc_set, bool device_address)
        => parent.Throw(spvc_compiler_msl_set_argument_buffer_device_address_space(compiler, desc_set, device_address));
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsShaderInputUsed(uint location) 
        => spvc_compiler_msl_is_shader_input_used(compiler, location);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsShaderOutputUsed(uint location) 
        => spvc_compiler_msl_is_shader_output_used(compiler, location);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsResourceUsed(ExecutionModel model, uint set, uint binding) 
        => spvc_compiler_msl_is_resource_used(compiler, model, set, binding);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RemapConstexprSampler(VariableID id, in ConstexprSampler sampler) 
        => parent.Throw(spvc_compiler_msl_remap_constexpr_sampler(compiler, id, sampler));
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RemapConstexprSamplerByBinding(uint desc_set, uint binding, in ConstexprSampler sampler)
        => parent.Throw(spvc_compiler_msl_remap_constexpr_sampler_by_binding(compiler, desc_set, binding, sampler));
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RemapConstexprSamplerYCBCR(VariableID id, in ConstexprSampler sampler, in SamplerYCBCRConversion conv)
        => parent.Throw(spvc_compiler_msl_remap_constexpr_sampler_ycbcr(compiler, id, sampler, conv));
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RemapConstexprSamplerByBindingYCBCR(uint desc_set, uint binding, in ConstexprSampler sampler, in SamplerYCBCRConversion conv)
        => parent.Throw(spvc_compiler_msl_remap_constexpr_sampler_by_binding_ycbcr(compiler, desc_set, binding, sampler, conv));
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetFragmentOutputComponents(uint location, uint components)
        => parent.Throw(spvc_compiler_msl_set_fragment_output_components(compiler, location, components));
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public uint GetAutomaticResourceBinding(VariableID id)
        => spvc_compiler_msl_get_automatic_resource_binding(compiler, id);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public uint GetAutomaticResourceBindingSecondary(VariableID id)
        => spvc_compiler_msl_get_automatic_resource_binding_secondary(compiler, id);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddDynamicBuffer(uint desc_set, uint binding, uint index)
        => parent.Throw(spvc_compiler_msl_add_dynamic_buffer(compiler, desc_set, binding, index));
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddInlineUniformBlock(uint desc_set, uint binding)
        => parent.Throw(spvc_compiler_msl_add_inline_uniform_block(compiler, desc_set, binding));
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetCombinedSamplerSuffix(string suffix)
        => parent.Throw(spvc_compiler_msl_set_combined_sampler_suffix(compiler, suffix));
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string GetCombinedSamplerSuffix()
        => Marshal.PtrToStringUTF8((IntPtr)spvc_compiler_msl_get_combined_sampler_suffix(compiler)) ?? "";
}
