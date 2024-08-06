using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET.MSL;

using static NativeBindings.Compiler;
using Option = NativeBindings.CompilerOption;

#pragma warning disable 1591 // Missing XML comment for publicly visible type or member

/// <summary>
/// MSL specific SPIR-V cross-compilation options
/// </summary>
public struct MSLCompilerOptions()
{
	/// <summary>
	/// Target Metal platform.
	/// </summary>
    public Platform platform = Platform.MacOS;
	
	/// <summary>
	/// Metal shading language version. Tuple stores version as (major, minor, patch).
	/// </summary>
    public (byte, byte, byte) msl_version = (1, 2, 0);

    public uint texelBufferTextureWidth = 4096; // Width of 2D Metal textures used as 1D texel buffers
    public uint r32uiLinearTextureAlignment = 4;
	public uint r32uiAlignmentConstantID = 65535;
	public uint swizzleBufferIndex = 30;
	public uint indirectParamsBufferIndex = 29;
	public uint shaderOutputBufferIndex = 28;
	public uint shaderPatchOutputBufferIndex = 27;
	public uint shaderTessFactorBufferIndex = 26;
	public uint bufferSizeBufferIndex = 25;
	public uint viewMaskBufferIndex = 24;
	public uint dynamicOffsetsBufferIndex = 23;
	public uint shaderInputBufferIndex = 22;
	public uint shaderIndexBufferIndex = 21;
	public uint shaderPatchInputBufferIndex = 20;
	public uint shaderInputWorkgroupIndex = 0;
	public uint deviceIndex = 0;
	public uint enableFragOutputMask = 0xffffffff;
    
	public bool enablePointSizeBuiltin = true;
	public bool enableFragDepthBuiltin = true;
	public bool enableFragStencilRefBuiltin = true;
	public bool disableRasterization = false;
	public bool captureOutputToBuffer = false;
	public bool swizzleTextureSamples = false;
	public bool tessDomainOriginLowerLeft = false;
	public bool multiview = false;
	public bool multiviewLayeredRendering = true;
	public bool viewIndexFromDeviceIndex = false;
	public bool dispatchBase = false;
	public bool texture1DAs2D = false;

	/// <summary>
	/// Enable use of Metal argument buffers.
	/// MSL 2.0 must also be enabled.
	/// </summary>
	public bool argumentBuffers = false;

	/// <summary>
	/// <para>When using Metal argument buffers, indicates the Metal argument buffer tier level supported by the Metal platform.</para>
	/// <para>Ignored when Options::argument_buffers is disabled.</para>
	/// <para>- Tier1 supports writable images on macOS, but not on iOS.</para>
	/// <para>- Tier2 supports writable images on macOS and iOS, and higher resource count limits.</para>
	/// Tier capabilities based on recommendations from Apple engineering.
	/// </summary>
	public ArgumentBuffersTier argumentBuffersTier = ArgumentBuffersTier.Tier1;

	/// <summary>
	/// Ensures vertex and instance indices start at zero. This reflects the behavior of HLSL with SV_VertexID and SV_InstanceID.
	/// </summary>
	public bool enableBaseIndexZero = false;

	/// <summary>
	/// Fragment output in MSL must have at least as many components as the render pass.
	/// Adds support to explicitly pad out components.
	/// </summary>
	public bool padFragmentOutputComponents = false;

	/// <summary>
	/// Specifies whether the iOS target version supports the [[base_vertex]] and [[base_instance]] attributes.
	/// </summary>
	public bool IOSSupportBaseVertexInstance = false;

	/// <summary>
	/// Use Metal's native frame-buffer fetch API for subpass inputs.
	/// </summary>
	public bool useFramebufferFetchSubpasses = false;

	/// <summary>
	/// Enables use of "fma" intrinsic for invariant float math
	/// </summary>
	public bool invariantFloatMath = false;

	/// <summary>
	/// Emulate texturecube_array with texture2d_array for iOS where this type is not available
	/// </summary>
	public bool emulateCubeArray = false;

	/// <summary>
	/// Allow user to enable decoration binding
	/// </summary>
	public bool enableDecorationBinding = false;

	/// <summary>
	/// Requires MSL 2.1, use the native support for texel buffers.
	/// </summary>
	public bool textureBufferNative = false;

	/// <summary>
	/// <para>Forces all resources which are part of an argument buffer to be considered active.</para>
	/// This ensures ABI compatibility between shaders where some resources might be unused,
	/// and would otherwise declare a different IAB.
	/// </summary>
	public bool forceActiveArgumentBufferResources = false;

	/// <summary>
	/// <para>Forces the use of plain arrays, which works around certain driver bugs on certain versions
	/// of Intel Macbooks. See https://github.com/KhronosGroup/SPIRV-Cross/issues/1210.</para>
	/// May reduce performance in scenarios where arrays are copied around as value-types.
	/// </summary>
	public bool forceNativeArrays = false;

	/// <summary>
	/// If a shader writes clip distance, also emit user varyings which
	/// can be read in subsequent stages.
	/// </summary>
	public bool enableClipDistanceUserVarying = true;

	/// <summary>
	/// In a tessellation control shader, assume that more than one patch can be processed in a
	/// single workgroup. This requires changes to the way the InvocationId and PrimitiveId
	/// builtins are processed, but should result in more efficient usage of the GPU.
	/// </summary>
	public bool multiPatchWorkgroup = false;

	/// <summary>
	/// Use storage buffers instead of vertex-style attributes for tessellation evaluation
	/// input. This may require conversion of inputs in the generated post-tessellation
	/// vertex shader, but allows the use of nested arrays.
	/// </summary>
	public bool rawBufferTeseInput = false;

	/// <summary>
	/// If set, a vertex shader will be compiled as part of a tessellation pipeline.
	/// It will be translated as a compute kernel, so it can use the global invocation ID
	/// to index the output buffer.
	/// </summary>
	public bool vertexForTessellation = false;

	/// <summary>
	/// Assume that SubpassData images have multiple layers. Layered input attachments
	/// are addressed relative to the Layer output from the vertex pipeline. 
	/// <para>This option has no effect with multiview, since all input attachments are assumed to be layered
	/// and will be addressed using the current ViewIndex.</para>
	/// </summary>
	public bool arrayedSubpassInput = false;

	/// <summary>
	/// Whether to use SIMD-group or quadgroup functions to implement group non-uniform
	/// operations. Some GPUs on iOS do not support the SIMD-group functions, only the
	/// quadgroup functions.
	/// </summary>
	public bool IOSUseSimdgroupFunctions = false;

	/// <summary>
	/// If set, the subgroup size will be assumed to be one, and subgroup-related
	/// builtins and operations will be emitted accordingly. This mode is intended to
	/// be used by MoltenVK on hardware/software configurations which do not provide
	/// sufficient support for subgroups.
	/// </summary>
	public bool emulateSubgroups = false;

	/// <summary>
	/// <para>If nonzero, a fixed subgroup size to assume. Metal, similarly to VK_EXT_subgroup_size_control,
	/// allows the SIMD-group size (aka thread execution width) to vary depending on
	/// register usage and requirements. In certain circumstances--for example, a pipeline
	/// in MoltenVK without VK_PIPELINE_SHADER_STAGE_CREATE_ALLOW_VARYING_SUBGROUP_SIZE_BIT_EXT--
	/// this is undesirable.</para> 
	/// This fixes the value of the SubgroupSize builtin, instead of
	/// mapping it to the Metal builtin [[thread_execution_width]]. If the thread
	/// execution width is reduced, the extra invocations will appear to be inactive.
	/// If zero, the SubgroupSize will be allowed to vary, and the builtin will be mapped
	/// to the Metal [[thread_execution_width]] builtin.
	/// </summary>
	public uint fixedSubgroupSize = 0;

	/// <summary>
	/// The type of index in the index buffer, if present. For a compute shader, Metal
	/// requires specifying the indexing at pipeline creation, rather than at draw time
	/// as with graphics pipelines. This means we must create three different pipelines,
	/// for no indexing, 16-bit indices, and 32-bit indices. Each requires different
	/// handling for the gl_VertexIndex builtin. We may as well, then, create three
	/// different shaders for these three scenarios.
	/// </summary>
	public IndexType vertexIndexType = IndexType.None;

	/// <summary>
	/// If set, a dummy [[sample_id]] input is added to a fragment shader if none is present.
	/// This will force the shader to run at sample rate, assuming Metal does not optimize
	/// the extra threads away.
	/// </summary>
	public bool forceSampleRateShading = false;

	/// <summary>
	/// <para>If set, gl_HelperInvocation will be set manually whenever a fragment is discarded.</para>
	/// Some Metal devices have a bug where simd_is_helper_thread() does not return true
	/// after a fragment has been discarded. This is a workaround that is only expected to be needed
	/// until the bug is fixed in Metal; it is provided as an option to allow disabling it when that occurs.
	/// </summary>
	public bool manualHelperInvocationUpdates = true;

	/// <summary>
	/// <para>If set, extra checks will be emitted in fragment shaders to prevent writes
	/// from discarded fragments.</para>
	/// Some Metal devices have a bug where writes to storage resources
	/// from discarded fragment threads continue to occur, despite the fragment being
	/// discarded. This is a workaround that is only expected to be needed until the
	/// bug is fixed in Metal; it is provided as an option so it can be enabled
	/// only when the bug is present.
	/// </summary>
	public bool checkDiscardedFragStores = false;

	/// <summary>
	/// <para>If set, Lod operands to OpImageSample*DrefExplicitLod for 1D and 2D array images
	/// will be implemented using a gradient instead of passing the level operand directly.</para>
	/// Some Metal devices have a bug where the level() argument to depth2d_array(T)::sample_compare()
	/// in a fragment shader is biased by some unknown amount, possibly dependent on the
	/// partial derivatives of the texture coordinates. This is a workaround that is only
	/// expected to be needed until the bug is fixed in Metal; it is provided as an option
	/// so it can be enabled only when the bug is present.
	/// </summary>
	public bool sampleDrefLodArrayAsGrad = false;

	/// <summary>
	/// <para>MSL doesn't guarantee coherence between writes and subsequent reads of read_write textures.
	/// This inserts fences before each read of a read_write texture to ensure coherency.</para>
	/// <para>If you're sure you never rely on this, you can set this to false for a possible performance improvement.</para>
	/// Note: Only Apple's GPU compiler takes advantage of the lack of coherency, so make sure to test on Apple GPUs if you disable this.
	/// </summary>
	public bool readwriteTextureFences = true;

	/// <summary>
	/// <para>Metal 3.1 introduced a Metal regression bug which causes infinite recursion during 
	/// Metal's analysis of an entry point input structure that is itself recursive. Enabling
	/// this option will replace the recursive input declaration with a alternate variable of
	/// type void*, and then cast to the correct type at the top of the entry point function.</para>
	/// The bug has been reported to Apple, and will hopefully be fixed in future releases.
	/// </summary>
	public bool replaceRecursiveInputs = false;

	/// <summary>
	/// If set, manual fixups of gradient vectors for cube texture lookups will be performed.
	/// All released Apple Silicon GPUs to date behave incorrectly when sampling a cube texture
	/// with explicit gradients. They will ignore one of the three partial derivatives based
	/// on the selected major axis, and expect the remaining derivatives to be partially
	/// transformed.
	/// </summary>
	public bool agxManualCubeGradFixup = false;

	/// <summary>
	/// <para>Metal will discard fragments with side effects under certain circumstances prematurely.</para>
	/// <para>Example: CTS test dEQP-VK.fragment_operations.early_fragment.discard_no_early_fragment_tests_depth
	/// Test will render a full screen quad with varying depth [0,1] for each fragment.</para>
	/// <para>Each fragment will do an operation with side effects, modify the depth value and
	/// discard the fragment. The test expects the fragment to be run due to:
	/// https://registry.khronos.org/vulkan/specs/1.0-extensions/html/vkspec.html#fragops-shader-depthreplacement
	/// which states that the fragment shader must be run due to replacing the depth in shader.
	/// However, Metal may prematurely discards fragments without executing them
	/// (I believe this to be due to a greedy optimization on their end) making the test fail.</para>
	/// This option enforces fragment execution for such cases where the fragment has operations
	/// with side effects. Provided as an option hoping Metal will fix this issue in the future.
	/// </summary>
	public bool forceFragmentWithSideEffectsExecution = false;

	static uint MakeMSLVersion(uint major, uint minor = 0, uint patch = 0)
	{
		return (major * 10000) + (minor * 100) + patch;
	}

	internal readonly unsafe void Apply(Context ctx, NativeBindings.CompilerOptions* options)
	{
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_PLATFORM, (uint)platform));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_VERSION, MakeMSLVersion(msl_version.Item1, msl_version.Item2, msl_version.Item3)));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_TEXEL_BUFFER_TEXTURE_WIDTH, texelBufferTextureWidth));

		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_R32UI_LINEAR_TEXTURE_ALIGNMENT, r32uiLinearTextureAlignment));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_R32UI_ALIGNMENT_CONSTANT_ID, r32uiAlignmentConstantID));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_SWIZZLE_BUFFER_INDEX, swizzleBufferIndex));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_INDIRECT_PARAMS_BUFFER_INDEX, indirectParamsBufferIndex));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_SHADER_OUTPUT_BUFFER_INDEX, shaderOutputBufferIndex));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_SHADER_PATCH_OUTPUT_BUFFER_INDEX, shaderPatchOutputBufferIndex));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_SHADER_TESS_FACTOR_OUTPUT_BUFFER_INDEX, shaderTessFactorBufferIndex));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_BUFFER_SIZE_BUFFER_INDEX, bufferSizeBufferIndex));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_VIEW_MASK_BUFFER_INDEX, viewMaskBufferIndex));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_DYNAMIC_OFFSETS_BUFFER_INDEX, dynamicOffsetsBufferIndex));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_SHADER_INPUT_BUFFER_INDEX, shaderInputBufferIndex));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_SHADER_INDEX_BUFFER_INDEX, shaderIndexBufferIndex));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_SHADER_PATCH_INPUT_BUFFER_INDEX, shaderPatchInputBufferIndex));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_SHADER_INPUT_WORKGROUP_INDEX, shaderInputWorkgroupIndex));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_DEVICE_INDEX, deviceIndex));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_ENABLE_FRAG_OUTPUT_MASK, enableFragOutputMask));

		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_ENABLE_POINT_SIZE_BUILTIN, enablePointSizeBuiltin));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_ENABLE_FRAG_DEPTH_BUILTIN, enableFragDepthBuiltin));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_ENABLE_FRAG_STENCIL_REF_BUILTIN, enableFragStencilRefBuiltin));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_DISABLE_RASTERIZATION, disableRasterization));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_CAPTURE_OUTPUT_TO_BUFFER, captureOutputToBuffer));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_SWIZZLE_TEXTURE_SAMPLES, swizzleTextureSamples));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_TESS_DOMAIN_ORIGIN_LOWER_LEFT, tessDomainOriginLowerLeft));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_MULTIVIEW, multiview));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_MULTIVIEW_LAYERED_RENDERING, multiviewLayeredRendering));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_VIEW_INDEX_FROM_DEVICE_INDEX, viewIndexFromDeviceIndex));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_DISPATCH_BASE, dispatchBase));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_TEXTURE_1D_AS_2D, texture1DAs2D));

		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_ARGUMENT_BUFFERS, argumentBuffers));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_ARGUMENT_BUFFERS_TIER, (uint)argumentBuffersTier));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_ENABLE_BASE_INDEX_ZERO, enableBaseIndexZero));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_PAD_FRAGMENT_OUTPUT_COMPONENTS, padFragmentOutputComponents));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_IOS_SUPPORT_BASE_VERTEX_INSTANCE, IOSSupportBaseVertexInstance));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_FRAMEBUFFER_FETCH_SUBPASS, useFramebufferFetchSubpasses));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_INVARIANT_FP_MATH, invariantFloatMath));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_EMULATE_CUBEMAP_ARRAY, emulateCubeArray));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_ENABLE_DECORATION_BINDING, enableDecorationBinding));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_TEXTURE_BUFFER_NATIVE, textureBufferNative));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_FORCE_ACTIVE_ARGUMENT_BUFFER_RESOURCES, forceActiveArgumentBufferResources));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_FORCE_NATIVE_ARRAYS, forceNativeArrays));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_ENABLE_CLIP_DISTANCE_USER_VARYING, enableClipDistanceUserVarying));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_MULTI_PATCH_WORKGROUP, multiPatchWorkgroup));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_RAW_BUFFER_TESE_INPUT, rawBufferTeseInput));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_VERTEX_FOR_TESSELLATION, vertexForTessellation));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_ARRAYED_SUBPASS_INPUT, arrayedSubpassInput));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_IOS_USE_SIMDGROUP_FUNCTIONS, IOSUseSimdgroupFunctions));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_EMULATE_SUBGROUPS, emulateSubgroups));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_FIXED_SUBGROUP_SIZE, fixedSubgroupSize));

		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_VERTEX_INDEX_TYPE, (uint)vertexIndexType));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_FORCE_SAMPLE_RATE_SHADING, forceSampleRateShading));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_MANUAL_HELPER_INVOCATION_UPDATES, manualHelperInvocationUpdates));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_CHECK_DISCARDED_FRAG_STORES, checkDiscardedFragStores));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_SAMPLE_DREF_LOD_ARRAY_AS_GRAD, sampleDrefLodArrayAsGrad));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_READWRITE_TEXTURE_FENCES, readwriteTextureFences));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_REPLACE_RECURSIVE_INPUTS, replaceRecursiveInputs));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_AGX_MANUAL_CUBE_GRAD_FIXUP, agxManualCubeGradFixup));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_FORCE_FRAGMENT_WITH_SIDE_EFFECTS_EXECUTION, forceFragmentWithSideEffectsExecution));
	}
}