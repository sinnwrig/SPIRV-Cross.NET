using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET.MSL;

using static NativeBindings.MSLCompiler;

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

    internal unsafe MSLCrossCompiler(Context context, NativeBindings.Compiler* compiler) : base(context, compiler) { }

    /// <inheritdoc/>
    public override string Compile()
    {
        metalOptions.Apply(context, optionsPtr);
        return base.Compile();
    }
    
    /// <summary>
    /// Provide feedback to calling API to allow runtime to disable pipeline
	/// rasterization if vertex shader requires rasterization to be disabled.
    /// </summary>
    public bool IsRasterizationDisabled() 
        => spvc_compiler_msl_is_rasterization_disabled(compiler);
    
    /// <summary>
    /// Provide feedback to calling API to allow it to pass an auxiliary
	/// swizzle buffer if the shader needs it.
    /// </summary>
    public bool NeedsSwizzleBuffer() 
        => spvc_compiler_msl_needs_swizzle_buffer(compiler);

    /// <summary>
    /// Provide feedback to calling API to allow it to pass a buffer
	/// containing STORAGE_BUFFER buffer sizes to support OpArrayLength.
    /// </summary>
    public bool NeedsBufferSizeBuffer() 
        => spvc_compiler_msl_needs_buffer_size_buffer(compiler);

    /// <summary>
    /// Provide feedback to calling API to allow it to pass an output
	/// buffer if the shader needs it.
    /// </summary>
    public bool NeedsOutputBuffer() 
        => spvc_compiler_msl_needs_output_buffer(compiler);
    
    /// <summary>
    /// Provide feedback to calling API to allow it to pass a patch output
	/// buffer if the shader needs it.
    /// </summary>
    public bool NeedsPatchOutputBuffer() 
        => spvc_compiler_msl_needs_patch_output_buffer(compiler);
    
    /// <summary>
    /// Provide feedback to calling API to allow it to pass an input threadgroup
	/// buffer if the shader needs it.
    /// </summary>
    public bool NeedsInputThreadgroupMem() 
        => spvc_compiler_msl_needs_input_threadgroup_mem(compiler);
    
    /// <summary>
    /// resource is a resource binding to indicate the MSL buffer,
	/// texture or sampler index to use for a particular SPIR-V description set
	/// and binding. If resource bindings are provided,
	/// IsResourceBindingUsed() will return true after calling compile() if
	/// the set/binding combination was used by the MSL code.
    /// </summary>
    public void AddResourceBinding(in ResourceBinding binding) 
        => context.Throw(spvc_compiler_msl_add_resource_binding(compiler, binding));
    
    /// <summary>
    /// input is a shader interface variable description used to fix up shader input variables.
	/// If shader inputs are provided, IsShaderInputUsed() will return true after
	/// calling compile() if the location were used by the MSL code.
    /// </summary>
    public void AddShaderInput(in ShaderInterfaceVar input)
        => context.Throw(spvc_compiler_msl_add_shader_input(compiler, input));
    
    /// <summary>
    /// output is a shader interface variable description used to fix up shader output variables.
	/// If shader outputs are provided, IsShaderOutputUsed() will return true after
	/// calling compile() if the location were used by the MSL code.
    /// </summary>
    public void AddShaderOutput(in ShaderInterfaceVar output) 
        => context.Throw(spvc_compiler_msl_add_shader_output(compiler, output));
    
    /// <summary>
    /// When using MSL argument buffers, we can force "classic" MSL 1.0 binding schemes for certain descriptor sets.
	/// This corresponds to VK_KHR_push_descriptor in Vulkan.
    /// </summary>
    public void AddDiscreteDescriptorSet(uint descSet) 
        => context.Throw(spvc_compiler_msl_add_discrete_descriptor_set(compiler, descSet));
    
    /// <summary>
    /// If an argument buffer is large enough, it may need to be in the device storage space rather than
	/// constant. Opt-in to this behavior here on a per set basis.
    /// </summary>
    public void SetArgumentBufferDeviceAddressSpace(uint descSet, bool deviceAddress)
        => context.Throw(spvc_compiler_msl_set_argument_buffer_device_address_space(compiler, descSet, deviceAddress));
    
    /// <summary>
    /// Query after compilation is done. This allows you to check if an input location was used by the shader.
    /// </summary>
    public bool IsShaderInputUsed(uint location) 
        => spvc_compiler_msl_is_shader_input_used(compiler, location);
    
    /// <summary>
    /// Query after compilation is done. This allows you to check if an output location were used by the shader.
    /// </summary>
    public bool IsShaderOutputUsed(uint location) 
        => spvc_compiler_msl_is_shader_output_used(compiler, location);
    
    /// <summary>
    /// NOTE: Only resources which are remapped using AddResourceBinding() will be reported here.
	/// Constexpr samplers are always assumed to be emitted.
	/// No specific MSLResourceBinding remapping is required for constexpr samplers as long as they are remapped
	/// by RemapConstexprSampler(_by_binding).
    /// </summary>
    public bool IsResourceUsed(ExecutionModel model, uint set, uint binding) 
        => spvc_compiler_msl_is_resource_used(compiler, model, set, binding);

    /// <summary>
    /// <para>Remap a sampler with ID to a constexpr sampler.</para>
	/// <para>Older iOS targets must use constexpr samplers in certain cases (PCF),
	/// so a static sampler must be used.</para>
	/// <para>The sampler will not consume a binding, but be declared in the entry point as a constexpr sampler.
	/// This can be used on both combined image/samplers (sampler2D) or standalone samplers.</para>
	/// <para>The remapped sampler must not be an array of samplers.</para>
	/// Prefer RemapConstexprSamplerByBinding unless you're also doing reflection anyways.
    /// </summary>
    public void RemapConstexprSampler(VariableID id, in ConstexprSampler sampler) 
        => context.Throw(spvc_compiler_msl_remap_constexpr_sampler(compiler, id, sampler));
    
    /// <summary>
    /// Same as RemapConstexprSampler, except you provide set/binding, rather than variable ID.
	/// Remaps based on ID take priority over set/binding remaps.
    /// </summary>
    public void RemapConstexprSamplerByBinding(uint descSet, uint binding, in ConstexprSampler sampler)
        => context.Throw(spvc_compiler_msl_remap_constexpr_sampler_by_binding(compiler, descSet, binding, sampler));
    
    /// <summary>
    /// Equivalent to RemapConstexprSampler, but performs sampler Y'CbCr conversion beforehand.
    /// </summary>
    public void RemapConstexprSamplerYCBCR(VariableID id, in ConstexprSampler sampler, in SamplerYCBCRConversion conv)
        => context.Throw(spvc_compiler_msl_remap_constexpr_sampler_ycbcr(compiler, id, sampler, conv));
    
    /// <summary>
    /// Equivalent to RemapConstexprSamplerByBinding, but performs sampler Y'CbCr conversion beforehand.
    /// </summary>
    public void RemapConstexprSamplerByBindingYCBCR(uint desc_set, uint binding, in ConstexprSampler sampler, in SamplerYCBCRConversion conv)
        => context.Throw(spvc_compiler_msl_remap_constexpr_sampler_by_binding_ycbcr(compiler, desc_set, binding, sampler, conv));
    
    /// <summary>
    /// If using MSLCompilerOptions.padFragmentOutputComponents, override the number of components we expect
	/// to use for a particular location. The default is 4 if number of components is not overridden.
    /// </summary>
    public void SetFragmentOutputComponents(uint location, uint components)
        => context.Throw(spvc_compiler_msl_set_fragment_output_components(compiler, location, components));
    
    /// <summary>
    /// <para>This must only be called after a successful call to Compile().</para>
	/// <para>For a variable resource ID obtained through reflection API, report the automatically assigned resource index.</para>
	/// <para>If the descriptor set was part of an argument buffer, report the [[id(N)]],
	/// or [[buffer/texture/sampler]] binding for other resources.</para>
	/// <para>If the resource was a combined image sampler, report the image binding here,
	/// use the _secondary version of this call to query the sampler half of the resource.</para>
	/// If no binding exists, -1 is returned.
    /// </summary>
    public uint GetAutomaticResourceBinding(VariableID id)
        => spvc_compiler_msl_get_automatic_resource_binding(compiler, id);
    
    /// <summary>
    /// Same as GetAutomaticResourceBinding, but should only be used for combined image samplers, in which case the
	/// sampler's binding is returned instead. For any other resource type, -1 is returned.
	/// Secondary bindings are also used for the auxillary image atomic buffer.
    /// </summary>
    public uint GetAutomaticResourceBindingSecondary(VariableID id)
        => spvc_compiler_msl_get_automatic_resource_binding_secondary(compiler, id);
    
    /// <summary>
    /// <para>descSet and binding are the SPIR-V descriptor set and binding of a buffer resource in this shader.</para>
    /// <para>index is the index within the dynamic offset buffer to use.</para> 
    /// This function marks that resource as using a dynamic offset (VK_DESCRIPTOR_TYPE_UNIFORM_BUFFER_DYNAMIC
	/// or VK_DESCRIPTOR_TYPE_STORAGE_BUFFER_DYNAMIC). This function only has any effect if argument buffers
	/// are enabled. If so, the buffer will have its address adjusted at the beginning of the shader with
	/// an offset taken from the dynamic offset buffer.
    /// </summary>
    public void AddDynamicBuffer(uint descSet, uint binding, uint index)
        => context.Throw(spvc_compiler_msl_add_dynamic_buffer(compiler, descSet, binding, index));
    
    /// <summary>
    /// descSet and binding are the SPIR-V descriptor set and binding of a buffer resource
	/// in this shader. This function marks that resource as an inline uniform block
	/// (VK_DESCRIPTOR_TYPE_INLINE_UNIFORM_BLOCK_EXT). This function only has any effect if argument buffers
	/// are enabled. If so, the buffer block will be directly embedded into the argument
	/// buffer, instead of being referenced indirectly via pointer.
    /// </summary>
    public void AddInlineUniformBlock(uint descSet, uint binding)
        => context.Throw(spvc_compiler_msl_add_inline_uniform_block(compiler, descSet, binding));
    
    /// <summary>
    /// </summary>
    public void SetCombinedSamplerSuffix(string suffix)
        => context.Throw(spvc_compiler_msl_set_combined_sampler_suffix(compiler, suffix));
    
    /// <summary>
    /// </summary>
    public string GetCombinedSamplerSuffix()
        => Marshal.PtrToStringUTF8((IntPtr)spvc_compiler_msl_get_combined_sampler_suffix(compiler)) ?? "";
}
