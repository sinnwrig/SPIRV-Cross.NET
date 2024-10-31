using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET.NativeBindings;

#pragma warning disable 1591

public enum CaptureMode
{
	/* The Parsed IR payload will be copied, and the handle can be reused to create other compiler instances. */
	Copy = 0,

	/*
	 * The payload will now be owned by the compiler.
	 * parsed_ir should now be considered a dead blob and must not be used further.
	 * This is optimal for performance and should be the go-to option.
	 */
	TakeOwnership = 1,
}

public enum Backend
{
	/* This backend can only perform reflection, no compiler options are supported. Maps to spirv_cross::Compiler. */
	None = 0,
	GLSL = 1, /* CompilerGLSL */
	HLSL = 2, /* CompilerHLSL */
	MSL = 3, /* CompilerMSL */
	CPP = 4, /* CompilerCPP */
	Json = 5, /* CompilerReflection w/ JSON backend */
}

public enum Result
{
	/* Success. */
	Success = 0,

	/* The SPIR-V is invalid. Should have been caught by validation ideally. */
	InvalidSPIRV = -1,

	/* The SPIR-V might be valid or invalid, but SPIRV-Cross currently cannot correctly translate this to your target language. */
	UnsupportedSPIRV = -2,

	/* If for some reason we hit this, new or malloc failed. */
	OutOfMemory = -3,

	/* Invalid API argument. */
	InvalidArgument = -4,
}

internal static class OptionBits
{
	internal const int Common = 0x1000000;
	internal const int GLSL = 0x2000000;
	internal const int HLSL = 0x4000000;
	internal const int MSL = 0x8000000;
}

public enum CompilerOption
{
	FORCE_TEMPORARY = 1 | OptionBits.Common,
	FLATTEN_MULTIDIMENSIONAL_ARRAYS = 2 | OptionBits.Common,
	FIXUP_DEPTH_CONVENTION = 3 | OptionBits.Common,
	FLIP_VERTEX_Y = 4 | OptionBits.Common,

	GLSL_SUPPORT_NONZERO_BASE_INSTANCE = 5 | OptionBits.GLSL,
	GLSL_SEPARATE_SHADER_OBJECTS = 6 | OptionBits.GLSL,
	GLSL_ENABLE_420PACK_EXTENSION = 7 | OptionBits.GLSL,
	GLSL_VERSION = 8 | OptionBits.GLSL,
	GLSL_ES = 9 | OptionBits.GLSL,
	GLSL_VULKAN_SEMANTICS = 10 | OptionBits.GLSL,
	GLSL_ES_DEFAULT_FLOAT_PRECISION_HIGHP = 11 | OptionBits.GLSL,
	GLSL_ES_DEFAULT_INT_PRECISION_HIGHP = 12 | OptionBits.GLSL,

	HLSL_SHADER_MODEL = 13 | OptionBits.HLSL,
	HLSL_POINT_SIZE_COMPAT = 14 | OptionBits.HLSL,
	HLSL_POINT_COORD_COMPAT = 15 | OptionBits.HLSL,
	HLSL_SUPPORT_NONZERO_BASE_VERTEX_BASE_INSTANCE = 16 | OptionBits.HLSL,

	MSL_VERSION = 17 | OptionBits.MSL,
	MSL_TEXEL_BUFFER_TEXTURE_WIDTH = 18 | OptionBits.MSL,

	/* Obsolete, use SWIZZLE_BUFFER_INDEX instead. */
	MSL_AUX_BUFFER_INDEX = 19 | OptionBits.MSL,
	MSL_SWIZZLE_BUFFER_INDEX = 19 | OptionBits.MSL,

	MSL_INDIRECT_PARAMS_BUFFER_INDEX = 20 | OptionBits.MSL,
	MSL_SHADER_OUTPUT_BUFFER_INDEX = 21 | OptionBits.MSL,
	MSL_SHADER_PATCH_OUTPUT_BUFFER_INDEX = 22 | OptionBits.MSL,
	MSL_SHADER_TESS_FACTOR_OUTPUT_BUFFER_INDEX = 23 | OptionBits.MSL,
	MSL_SHADER_INPUT_WORKGROUP_INDEX = 24 | OptionBits.MSL,
	MSL_ENABLE_POINT_SIZE_BUILTIN = 25 | OptionBits.MSL,
	MSL_DISABLE_RASTERIZATION = 26 | OptionBits.MSL,
	MSL_CAPTURE_OUTPUT_TO_BUFFER = 27 | OptionBits.MSL,
	MSL_SWIZZLE_TEXTURE_SAMPLES = 28 | OptionBits.MSL,
	MSL_PAD_FRAGMENT_OUTPUT_COMPONENTS = 29 | OptionBits.MSL,
	MSL_TESS_DOMAIN_ORIGIN_LOWER_LEFT = 30 | OptionBits.MSL,
	MSL_PLATFORM = 31 | OptionBits.MSL,
	MSL_ARGUMENT_BUFFERS = 32 | OptionBits.MSL,

	GLSL_EMIT_PUSH_CONSTANT_AS_UNIFORM_BUFFER = 33 | OptionBits.GLSL,

	MSL_TEXTURE_BUFFER_NATIVE = 34 | OptionBits.MSL,

	GLSL_EMIT_UNIFORM_BUFFER_AS_PLAIN_UNIFORMS = 35 | OptionBits.GLSL,

	MSL_BUFFER_SIZE_BUFFER_INDEX = 36 | OptionBits.MSL,

	EMIT_LINE_DIRECTIVES = 37 | OptionBits.Common,

	MSL_MULTIVIEW = 38 | OptionBits.MSL,
	MSL_VIEW_MASK_BUFFER_INDEX = 39 | OptionBits.MSL,
	MSL_DEVICE_INDEX = 40 | OptionBits.MSL,
	MSL_VIEW_INDEX_FROM_DEVICE_INDEX = 41 | OptionBits.MSL,
	MSL_DISPATCH_BASE = 42 | OptionBits.MSL,
	MSL_DYNAMIC_OFFSETS_BUFFER_INDEX = 43 | OptionBits.MSL,
	MSL_TEXTURE_1D_AS_2D = 44 | OptionBits.MSL,
	MSL_ENABLE_BASE_INDEX_ZERO = 45 | OptionBits.MSL,

	/* Obsolete. Use MSL_FRAMEBUFFER_FETCH_SUBPASS instead. */
	MSL_IOS_FRAMEBUFFER_FETCH_SUBPASS = 46 | OptionBits.MSL,
	MSL_FRAMEBUFFER_FETCH_SUBPASS = 46 | OptionBits.MSL,

	MSL_INVARIANT_FP_MATH = 47 | OptionBits.MSL,
	MSL_EMULATE_CUBEMAP_ARRAY = 48 | OptionBits.MSL,
	MSL_ENABLE_DECORATION_BINDING = 49 | OptionBits.MSL,
	MSL_FORCE_ACTIVE_ARGUMENT_BUFFER_RESOURCES = 50 | OptionBits.MSL,
	MSL_FORCE_NATIVE_ARRAYS = 51 | OptionBits.MSL,

	ENABLE_STORAGE_IMAGE_QUALIFIER_DEDUCTION = 52 | OptionBits.Common,

	HLSL_FORCE_STORAGE_BUFFER_AS_UAV = 53 | OptionBits.HLSL,

	FORCE_ZERO_INITIALIZED_VARIABLES = 54 | OptionBits.Common,

	HLSL_NONWRITABLE_UAV_TEXTURE_AS_SRV = 55 | OptionBits.HLSL,

	MSL_ENABLE_FRAG_OUTPUT_MASK = 56 | OptionBits.MSL,
	MSL_ENABLE_FRAG_DEPTH_BUILTIN = 57 | OptionBits.MSL,
	MSL_ENABLE_FRAG_STENCIL_REF_BUILTIN = 58 | OptionBits.MSL,
	MSL_ENABLE_CLIP_DISTANCE_USER_VARYING = 59 | OptionBits.MSL,

	HLSL_ENABLE_16BIT_TYPES = 60 | OptionBits.HLSL,

	MSL_MULTI_PATCH_WORKGROUP = 61 | OptionBits.MSL,
	MSL_SHADER_INPUT_BUFFER_INDEX = 62 | OptionBits.MSL,
	MSL_SHADER_INDEX_BUFFER_INDEX = 63 | OptionBits.MSL,
	MSL_VERTEX_FOR_TESSELLATION = 64 | OptionBits.MSL,
	MSL_VERTEX_INDEX_TYPE = 65 | OptionBits.MSL,

	GLSL_FORCE_FLATTENED_IO_BLOCKS = 66 | OptionBits.GLSL,

	MSL_MULTIVIEW_LAYERED_RENDERING = 67 | OptionBits.MSL,
	MSL_ARRAYED_SUBPASS_INPUT = 68 | OptionBits.MSL,
	MSL_R32UI_LINEAR_TEXTURE_ALIGNMENT = 69 | OptionBits.MSL,
	MSL_R32UI_ALIGNMENT_CONSTANT_ID = 70 | OptionBits.MSL,

	HLSL_FLATTEN_MATRIX_VERTEX_INPUT_SEMANTICS = 71 | OptionBits.HLSL,

	MSL_IOS_USE_SIMDGROUP_FUNCTIONS = 72 | OptionBits.MSL,
	MSL_EMULATE_SUBGROUPS = 73 | OptionBits.MSL,
	MSL_FIXED_SUBGROUP_SIZE = 74 | OptionBits.MSL,
	MSL_FORCE_SAMPLE_RATE_SHADING = 75 | OptionBits.MSL,
	MSL_IOS_SUPPORT_BASE_VERTEX_INSTANCE = 76 | OptionBits.MSL,

	GLSL_OVR_MULTIVIEW_VIEW_COUNT = 77 | OptionBits.GLSL,

	RELAX_NAN_CHECKS = 78 | OptionBits.Common,

	MSL_RAW_BUFFER_TESE_INPUT = 79 | OptionBits.MSL,
	MSL_SHADER_PATCH_INPUT_BUFFER_INDEX = 80 | OptionBits.MSL,
	MSL_MANUAL_HELPER_INVOCATION_UPDATES = 81 | OptionBits.MSL,
	MSL_CHECK_DISCARDED_FRAG_STORES = 82 | OptionBits.MSL,

	GLSL_ENABLE_ROW_MAJOR_LOAD_WORKAROUND = 83 | OptionBits.GLSL,

	MSL_ARGUMENT_BUFFERS_TIER = 84 | OptionBits.MSL,
	MSL_SAMPLE_DREF_LOD_ARRAY_AS_GRAD = 85 | OptionBits.MSL,
	MSL_READWRITE_TEXTURE_FENCES = 86 | OptionBits.MSL,
	MSL_REPLACE_RECURSIVE_INPUTS = 87 | OptionBits.MSL,
	MSL_AGX_MANUAL_CUBE_GRAD_FIXUP = 88 | OptionBits.MSL,
	MSL_FORCE_FRAGMENT_WITH_SIDE_EFFECTS_EXECUTION = 89 | OptionBits.MSL,

	HLSL_USE_ENTRY_POINT_NAME = 90 | OptionBits.HLSL,
	HLSL_PRESERVE_STRUCTURED_BUFFERS = 91 | OptionBits.HLSL,
}

public enum ResourceType
{
	Unknown = 0,
	UniformBuffer = 1,
	StorageBuffer = 2,
	StageInput = 3,
	StageOutput = 4,
	SubpassInput = 5,
	StorageImage = 6,
	SampledImage = 7,
	AtomicCounter = 8,
	PushConstant = 9,
	SeparateImage = 10,
	SeparateSamplers = 11,
	AccelerationStructure = 12,
	RayQuery = 13,
	ShaderRecordBuffer = 14,
	GLPlainUniform = 15,
}

public enum BuiltinResourceType
{
	StageInput = 1,
	StageOutput = 2,
}