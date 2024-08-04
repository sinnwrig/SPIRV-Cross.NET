using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET.MSL;

using static Native.Compiler;
using Option = Native.CompilerOption;

/// <summary>
/// MSL specific SPIR-V cross-compilation options
/// </summary>
public struct MSLCompilerOptions()
{
    public Platform platform = Platform.MacOS;
	
    public (byte, byte, byte) msl_version = (1, 2, 0);

	public uint texel_buffer_texture_width = 4096; // Width of 2D Metal textures used as 1D texel buffers
	public uint r32ui_linear_texture_alignment = 4;
	public uint r32ui_alignment_constant_id = 65535;
	public uint swizzle_buffer_index = 30;
	public uint indirect_params_buffer_index = 29;
	public uint shader_output_buffer_index = 28;
	public uint shader_patch_output_buffer_index = 27;
	public uint shader_tess_factor_buffer_index = 26;
	public uint buffer_size_buffer_index = 25;
	public uint view_mask_buffer_index = 24;
	public uint dynamic_offsets_buffer_index = 23;
	public uint shader_input_buffer_index = 22;
	public uint shader_index_buffer_index = 21;
	public uint shader_patch_input_buffer_index = 20;
	public uint shader_input_wg_index = 0;
	public uint device_index = 0;
	public uint enable_frag_output_mask = 0xffffffff;
    
	public bool enable_point_size_builtin = true;
	public bool enable_frag_depth_builtin = true;
	public bool enable_frag_stencil_ref_builtin = true;
	public bool disable_rasterization = false;
	public bool capture_output_to_buffer = false;
	public bool swizzle_texture_samples = false;
	public bool tess_domain_origin_lower_left = false;
	public bool multiview = false;
	public bool multiview_layered_rendering = true;
	public bool view_index_from_device_index = false;
	public bool dispatch_base = false;
	public bool texture_1D_as_2D = false;

	// Enable use of Metal argument buffers.
	// MSL 2.0 must also be enabled.
	public bool argument_buffers = false;

	// When using Metal argument buffers, indicates the Metal argument buffer tier level supported by the Metal platform.
	// Ignored when Options::argument_buffers is disabled.
	// - Tier1 supports writable images on macOS, but not on iOS.
	// - Tier2 supports writable images on macOS and iOS, and higher resource count limits.
	// Tier capabilities based on recommendations from Apple engineering.
	public ArgumentBuffersTier argument_buffers_tier = ArgumentBuffersTier.Tier1;

	// Ensures vertex and instance indices start at zero. This reflects the behavior of HLSL with SV_VertexID and SV_InstanceID.
	public bool enable_base_index_zero = false;

	// Fragment output in MSL must have at least as many components as the render pass.
	// Add support to explicit pad out components.
	public bool pad_fragment_output_components = false;

	// Specifies whether the iOS target version supports the [[base_vertex]] and [[base_instance]] attributes.
	public bool ios_support_base_vertex_instance = false;

	// Use Metal's native frame-buffer fetch API for subpass inputs.
	public bool use_framebuffer_fetch_subpasses = false;

	// Enables use of "fma" intrinsic for invariant float math
	public bool invariant_float_math = false;

	// Emulate texturecube_array with texture2d_array for iOS where this type is not available
	public bool emulate_cube_array = false;

	// Allow user to enable decoration binding
	public bool enable_decoration_binding = false;

	// Requires MSL 2.1, use the native support for texel buffers.
	public bool texture_buffer_native = false;

	// Forces all resources which are part of an argument buffer to be considered active.
	// This ensures ABI compatibility between shaders where some resources might be unused,
	// and would otherwise declare a different IAB.
	public bool force_active_argument_buffer_resources = false;

	// Forces the use of plain arrays, which works around certain driver bugs on certain versions
	// of Intel Macbooks. See https://github.com/KhronosGroup/SPIRV-Cross/issues/1210.
	// May reduce performance in scenarios where arrays are copied around as value-types.
	public bool force_native_arrays = false;

	// If a shader writes clip distance, also emit user varyings which
	// can be read in subsequent stages.
	public bool enable_clip_distance_user_varying = true;

	// In a tessellation control shader, assume that more than one patch can be processed in a
	// single workgroup. This requires changes to the way the InvocationId and PrimitiveId
	// builtins are processed, but should result in more efficient usage of the GPU.
	public bool multi_patch_workgroup = false;

	// Use storage buffers instead of vertex-style attributes for tessellation evaluation
	// input. This may require conversion of inputs in the generated post-tessellation
	// vertex shader, but allows the use of nested arrays.
	public bool raw_buffer_tese_input = false;

	// If set, a vertex shader will be compiled as part of a tessellation pipeline.
	// It will be translated as a compute kernel, so it can use the global invocation ID
	// to index the output buffer.
	public bool vertex_for_tessellation = false;

	// Assume that SubpassData images have multiple layers. Layered input attachments
	// are addressed relative to the Layer output from the vertex pipeline. This option
	// has no effect with multiview, since all input attachments are assumed to be layered
	// and will be addressed using the current ViewIndex.
	public bool arrayed_subpass_input = false;

	// Whether to use SIMD-group or quadgroup functions to implement group non-uniform
	// operations. Some GPUs on iOS do not support the SIMD-group functions, only the
	// quadgroup functions.
	public bool ios_use_simdgroup_functions = false;

	// If set, the subgroup size will be assumed to be one, and subgroup-related
	// builtins and operations will be emitted accordingly. This mode is intended to
	// be used by MoltenVK on hardware/software configurations which do not provide
	// sufficient support for subgroups.
	public bool emulate_subgroups = false;

	// If nonzero, a fixed subgroup size to assume. Metal, similarly to VK_EXT_subgroup_size_control,
	// allows the SIMD-group size (aka thread execution width) to vary depending on
	// register usage and requirements. In certain circumstances--for example, a pipeline
	// in MoltenVK without VK_PIPELINE_SHADER_STAGE_CREATE_ALLOW_VARYING_SUBGROUP_SIZE_BIT_EXT--
	// this is undesirable. This fixes the value of the SubgroupSize builtin, instead of
	// mapping it to the Metal builtin [[thread_execution_width]]. If the thread
	// execution width is reduced, the extra invocations will appear to be inactive.
	// If zero, the SubgroupSize will be allowed to vary, and the builtin will be mapped
	// to the Metal [[thread_execution_width]] builtin.
	public uint fixed_subgroup_size = 0;

	// The type of index in the index buffer, if present. For a compute shader, Metal
	// requires specifying the indexing at pipeline creation, rather than at draw time
	// as with graphics pipelines. This means we must create three different pipelines,
	// for no indexing, 16-bit indices, and 32-bit indices. Each requires different
	// handling for the gl_VertexIndex builtin. We may as well, then, create three
	// different shaders for these three scenarios.
	public IndexType vertex_index_type = IndexType.None;

	// If set, a dummy [[sample_id]] input is added to a fragment shader if none is present.
	// This will force the shader to run at sample rate, assuming Metal does not optimize
	// the extra threads away.
	public bool force_sample_rate_shading = false;

	// If set, gl_HelperInvocation will be set manually whenever a fragment is discarded.
	// Some Metal devices have a bug where simd_is_helper_thread() does not return true
	// after a fragment has been discarded. This is a workaround that is only expected to be needed
	// until the bug is fixed in Metal; it is provided as an option to allow disabling it when that occurs.
	public bool manual_helper_invocation_updates = true;

	// If set, extra checks will be emitted in fragment shaders to prevent writes
	// from discarded fragments. Some Metal devices have a bug where writes to storage resources
	// from discarded fragment threads continue to occur, despite the fragment being
	// discarded. This is a workaround that is only expected to be needed until the
	// bug is fixed in Metal; it is provided as an option so it can be enabled
	// only when the bug is present.
	public bool check_discarded_frag_stores = false;

	// If set, Lod operands to OpImageSample*DrefExplicitLod for 1D and 2D array images
	// will be implemented using a gradient instead of passing the level operand directly.
	// Some Metal devices have a bug where the level() argument to depth2d_array<T>::sample_compare()
	// in a fragment shader is biased by some unknown amount, possibly dependent on the
	// partial derivatives of the texture coordinates. This is a workaround that is only
	// expected to be needed until the bug is fixed in Metal; it is provided as an option
	// so it can be enabled only when the bug is present.
	public bool sample_dref_lod_array_as_grad = false;

	// MSL doesn't guarantee coherence between writes and subsequent reads of read_write textures.
	// This inserts fences before each read of a read_write texture to ensure coherency.
	// If you're sure you never rely on this, you can set this to false for a possible performance improvement.
	// Note: Only Apple's GPU compiler takes advantage of the lack of coherency, so make sure to test on Apple GPUs if you disable this.
	public bool readwrite_texture_fences = true;

	// Metal 3.1 introduced a Metal regression bug which causes infinite recursion during 
	// Metal's analysis of an entry point input structure that is itself recursive. Enabling
	// this option will replace the recursive input declaration with a alternate variable of
	// type void*, and then cast to the correct type at the top of the entry point function.
	// The bug has been reported to Apple, and will hopefully be fixed in future releases.
	public bool replace_recursive_inputs = false;

	// If set, manual fixups of gradient vectors for cube texture lookups will be performed.
	// All released Apple Silicon GPUs to date behave incorrectly when sampling a cube texture
	// with explicit gradients. They will ignore one of the three partial derivatives based
	// on the selected major axis, and expect the remaining derivatives to be partially
	// transformed.
	public bool agx_manual_cube_grad_fixup = false;

	// Metal will discard fragments with side effects under certain circumstances prematurely.
	// Example: CTS test dEQP-VK.fragment_operations.early_fragment.discard_no_early_fragment_tests_depth
	// Test will render a full screen quad with varying depth [0,1] for each fragment.
	// Each fragment will do an operation with side effects, modify the depth value and
	// discard the fragment. The test expects the fragment to be run due to:
	// https://registry.khronos.org/vulkan/specs/1.0-extensions/html/vkspec.html#fragops-shader-depthreplacement
	// which states that the fragment shader must be run due to replacing the depth in shader.
	// However, Metal may prematurely discards fragments without executing them
	// (I believe this to be due to a greedy optimization on their end) making the test fail.
	// This option enforces fragment execution for such cases where the fragment has operations
	// with side effects. Provided as an option hoping Metal will fix this issue in the future.
	public bool force_fragment_with_side_effects_execution = false;

	static uint MakeMSLVersion(uint major, uint minor = 0, uint patch = 0)
	{
		return (major * 10000) + (minor * 100) + patch;
	}

	internal readonly unsafe void Apply(Context ctx, Native.CompilerOptions* options)
	{
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_PLATFORM, (uint)platform));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_VERSION, MakeMSLVersion(msl_version.Item1, msl_version.Item2, msl_version.Item3)));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_TEXEL_BUFFER_TEXTURE_WIDTH, texel_buffer_texture_width));

		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_R32UI_LINEAR_TEXTURE_ALIGNMENT, r32ui_linear_texture_alignment));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_R32UI_ALIGNMENT_CONSTANT_ID, r32ui_alignment_constant_id));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_SWIZZLE_BUFFER_INDEX, swizzle_buffer_index));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_INDIRECT_PARAMS_BUFFER_INDEX, indirect_params_buffer_index));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_SHADER_OUTPUT_BUFFER_INDEX, shader_output_buffer_index));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_SHADER_PATCH_OUTPUT_BUFFER_INDEX, shader_patch_output_buffer_index));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_SHADER_TESS_FACTOR_OUTPUT_BUFFER_INDEX, shader_tess_factor_buffer_index));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_BUFFER_SIZE_BUFFER_INDEX, buffer_size_buffer_index));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_VIEW_MASK_BUFFER_INDEX, view_mask_buffer_index));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_DYNAMIC_OFFSETS_BUFFER_INDEX, dynamic_offsets_buffer_index));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_SHADER_INPUT_BUFFER_INDEX, shader_input_buffer_index));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_SHADER_INDEX_BUFFER_INDEX, shader_index_buffer_index));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_SHADER_PATCH_INPUT_BUFFER_INDEX, shader_patch_input_buffer_index));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_SHADER_INPUT_WORKGROUP_INDEX, shader_input_wg_index));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_DEVICE_INDEX, device_index));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_ENABLE_FRAG_OUTPUT_MASK, enable_frag_output_mask));

		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_ENABLE_POINT_SIZE_BUILTIN, enable_point_size_builtin));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_ENABLE_FRAG_DEPTH_BUILTIN, enable_frag_depth_builtin));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_ENABLE_FRAG_STENCIL_REF_BUILTIN, enable_frag_stencil_ref_builtin));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_DISABLE_RASTERIZATION, disable_rasterization));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_CAPTURE_OUTPUT_TO_BUFFER, capture_output_to_buffer));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_SWIZZLE_TEXTURE_SAMPLES, swizzle_texture_samples));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_TESS_DOMAIN_ORIGIN_LOWER_LEFT, tess_domain_origin_lower_left));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_MULTIVIEW, multiview));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_MULTIVIEW_LAYERED_RENDERING, multiview_layered_rendering));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_VIEW_INDEX_FROM_DEVICE_INDEX, view_index_from_device_index));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_DISPATCH_BASE, dispatch_base));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_TEXTURE_1D_AS_2D, texture_1D_as_2D));

		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_ARGUMENT_BUFFERS, argument_buffers));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_ARGUMENT_BUFFERS_TIER, (uint)argument_buffers_tier));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_ENABLE_BASE_INDEX_ZERO, enable_base_index_zero));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_PAD_FRAGMENT_OUTPUT_COMPONENTS, pad_fragment_output_components));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_IOS_SUPPORT_BASE_VERTEX_INSTANCE, ios_support_base_vertex_instance));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_FRAMEBUFFER_FETCH_SUBPASS, use_framebuffer_fetch_subpasses));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_INVARIANT_FP_MATH, invariant_float_math));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_EMULATE_CUBEMAP_ARRAY, emulate_cube_array));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_ENABLE_DECORATION_BINDING, enable_decoration_binding));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_TEXTURE_BUFFER_NATIVE, texture_buffer_native));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_FORCE_ACTIVE_ARGUMENT_BUFFER_RESOURCES, force_active_argument_buffer_resources));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_FORCE_NATIVE_ARRAYS, force_native_arrays));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_ENABLE_CLIP_DISTANCE_USER_VARYING, enable_clip_distance_user_varying));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_MULTI_PATCH_WORKGROUP, multi_patch_workgroup));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_RAW_BUFFER_TESE_INPUT, raw_buffer_tese_input));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_VERTEX_FOR_TESSELLATION, vertex_for_tessellation));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_ARRAYED_SUBPASS_INPUT, arrayed_subpass_input));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_IOS_USE_SIMDGROUP_FUNCTIONS, ios_use_simdgroup_functions));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_EMULATE_SUBGROUPS, emulate_subgroups));
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_FIXED_SUBGROUP_SIZE, fixed_subgroup_size));

		ctx.Throw(spvc_compiler_options_set_uint(options, Option.MSL_VERTEX_INDEX_TYPE, (uint)vertex_index_type));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_FORCE_SAMPLE_RATE_SHADING, force_sample_rate_shading));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_MANUAL_HELPER_INVOCATION_UPDATES, manual_helper_invocation_updates));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_CHECK_DISCARDED_FRAG_STORES, check_discarded_frag_stores));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_SAMPLE_DREF_LOD_ARRAY_AS_GRAD, sample_dref_lod_array_as_grad));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_READWRITE_TEXTURE_FENCES, readwrite_texture_fences));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_REPLACE_RECURSIVE_INPUTS, replace_recursive_inputs));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_AGX_MANUAL_CUBE_GRAD_FIXUP, agx_manual_cube_grad_fixup));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.MSL_FORCE_FRAGMENT_WITH_SIDE_EFFECTS_EXECUTION, force_fragment_with_side_effects_execution));
	}
}