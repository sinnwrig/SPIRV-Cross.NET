using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET;

using static Native.Compiler;

/// <summary>
/// Wraps basic SPIRV-Cross cross-compilation functionality provided by <see cref="Native.Compiler"/> into a type-safe and memory-safe object.
/// </summary>
public abstract unsafe class Compiler : Reflector
{
    /// <summary>
    /// Common SPIRV-Cross options to compile with.
    /// </summary>
    public CompilerOptions options = new();

    internal Native.CompilerOptions* optionsPtr;

    internal Compiler(Context context, Native.Compiler* compiler) : base(context, compiler) 
    { 
        fixed (Native.CompilerOptions** optionsRef = &optionsPtr)
            context.Throw(spvc_compiler_create_compiler_options(compiler, optionsRef));
    }

    /// <summary>
    /// Compile IR into a string.
    /// </summary>
    public virtual string Compile()
    {
        options.Apply(context, optionsPtr);
        context.Throw(spvc_compiler_install_compiler_options(compiler, optionsPtr));

        byte* str = null;
        context.Throw(spvc_compiler_compile(compiler, &str));
        return Marshal.PtrToStringUTF8((IntPtr)str) ?? "";
    }

    /// <summary>
    /// <para>Adds a line to be added right after #version in GLSL backend.
	/// This is useful for enabling custom extensions which are outside the scope of SPIRV-Cross.
	/// This can be combined with variable remapping.</para>
    /// <para>While AddHeaderLine() is a more generic way of adding arbitrary text to the header
	/// of a GLSL file, RequireExtension() should be used when adding extensions since it will
	/// avoid creating collisions with SPIRV-Cross generated extensions.</para>
    /// Code added via AddHeaderLine() is typically backend-specific.
    /// </summary>
    public void AddHeaderLine(string line)
        => context.Throw(spvc_compiler_add_header_line(compiler, line));

    /// <summary>
    /// <para>If a shader output is active in this stage, but inactive in a subsequent stage,
	/// this can be signalled here. This can be used to work around certain cross-stage matching problems
	/// which plagues MSL and HLSL in certain scenarios.</para>
	/// <para>An output which matches one of these will not be emitted in stage output interfaces, but rather treated as a private
	/// variable.</para>
	/// This option is only meaningful for MSL and HLSL, since GLSL matches by location directly.
	/// Masking builtins only takes effect if the builtin in question is part of the stage output interface.
    /// </summary>
    public void MaskStageOutputByLocation(uint location, uint component)
        => context.Throw(spvc_compiler_mask_stage_output_by_location(compiler, location, component));

    /// <summary>
    /// <para>If a shader output is active in this stage, but inactive in a subsequent stage,
	/// this can be signalled here. This can be used to work around certain cross-stage matching problems
	/// which plagues MSL and HLSL in certain scenarios.</para>
	/// <para>An output which matches one of these will not be emitted in stage output interfaces, but rather treated as a private
	/// variable.</para>
	/// This option is only meaningful for MSL and HLSL, since GLSL matches by location directly.
	/// Masking builtins only takes effect if the builtin in question is part of the stage output interface.
    /// </summary>
    public void MaskStageOutputByBuiltin(BuiltIn builtin)
        => context.Throw(spvc_compiler_mask_stage_output_by_builtin(compiler, builtin));

    /// <summary>
    /// <para>Analyzes all OpImageFetch (texelFetch) opcodes and checks if there are instances where
	/// said instruction is used without a combined image sampler.</para>
	/// <para>GLSL targets do not support the use of texelFetch without a sampler.
	/// To workaround this, we must inject a dummy sampler which can be used to form a sampler2D at the call-site of
	/// texelFetch as necessary.</para>
	///
	/// <para>This must be called before BuildCombinedImageIamplers().
	/// BuildCombinedImageSamplers() may refer to the ID returned by this method if the returned ID is non-zero.
	/// The return value will be the ID of a sampler object if a dummy sampler is necessary, or 0 if no sampler object
	/// is required.</para>
	///
	/// If the returned ID is non-zero, it can be decorated with set/bindings as desired before calling Compile().
	/// Calling this function also invalidates GetActiveInterfaceVariables(), so this should be called
	/// before that function.
    /// </summary>
    public void BuildDummySamplerForCombinedImages(out VariableID id) 
        => context.Throw(spvc_compiler_build_dummy_sampler_for_combined_images(compiler, out id));

    /// <summary>
    /// <para>Analyzes all separate image and samplers used from the currently selected entry point,
	/// and re-routes them all to a combined image sampler instead.
	/// This is required to "support" separate image samplers in targets which do not natively support
	/// this feature, like GLSL/ESSL.</para>
	///
	/// <para>This must be called before Compile() if such remapping is desired.
	/// This call will add new sampled images to the SPIR-V,
	/// so it will appear in reflection if GetShaderResources() is called after BuildCombinedImageSamplers.</para>
	///
	/// <para>If any image/sampler remapping was found, no separate image/samplers will appear in the decompiled output,
	/// but will still appear in reflection.</para>
	///
	/// <para>The resulting samplers will be void of any decorations like name, descriptor sets and binding points,
	/// so this can be added before compile() if desired.</para>
	///
	/// <para>Combined image samplers originating from this set are always considered active variables.
	/// Arrays of separate samplers are not supported, but arrays of separate images are supported.
	/// Array of images + sampler -> Array of combined image samplers.</para>
    /// </summary>
    public void BuildCombinedImageSamplers()
        => context.Throw(spvc_compiler_build_combined_image_samplers(compiler));

    /// <summary>
    /// Gets a remapping for the combined image samplers.
    /// </summary>
    public ReadOnlySpan<CombinedImageSampler> GetCombinedImageSamplers()
    {   
        CombinedImageSampler* samplersPtr = null;
        context.Throw(spvc_compiler_get_combined_image_samplers(compiler, &samplersPtr, out nuint numSamplers));
        return new ReadOnlySpan<CombinedImageSampler>(samplersPtr, (int)numSamplers);
    }

    /// <summary>
    /// <para>Sets the interface variables which are used during compilation.</para>
	/// <para>By default, all variables are used.</para>
	/// Once set, compile() will only consider the set in active_variables.
    /// </summary>
    public void SetEnabledInterfaceVariables(in Set set) 
        => context.Throw(spvc_compiler_set_enabled_interface_variables(compiler, set.set));

    /// <summary>
    /// Is this variable an HLSL counter buffer?
    /// </summary>
    public bool IsCompilerBufferHLSLCounterBuffer(VariableID id)
        => spvc_compiler_buffer_is_hlsl_counter_buffer(compiler, id);
    
    /// <summary>
    /// Gets the paired buffer for an HLSL counter buffer.
    /// </summary>
    public bool CompilerBufferGetHLSLCounterBuffer(VariableID id, out VariableID counter_id)
        => spvc_compiler_buffer_get_hlsl_counter_buffer(compiler, id, out counter_id);
}