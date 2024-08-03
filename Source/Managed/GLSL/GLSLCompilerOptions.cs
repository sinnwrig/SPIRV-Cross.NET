using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET.GLSL;

using static Native.Compiler;
using Option = Native.CompilerOption;

public struct GLSLCompilerOptions()
{
    // The shading language version.
    public uint version = 450;

    // Emit the OpenGL ES shading language instead of desktop OpenGL.
    public bool ES = false;

	// If true, Vulkan GLSL features are used instead of GL-compatible features.
	// Mostly useful for debugging SPIR-V files.
	public bool vulkanSemantics = false;

	// If true, gl_PerVertex is explicitly redeclared in vertex, geometry and tessellation shaders.
	// The members of gl_PerVertex is determined by which built-ins are declared by the shader.
	// This option is ignored in ES versions, as redeclaration in ES is not required, and it depends on a different extension
	// (EXT_shader_io_blocks) which makes things a bit more fuzzy.
	public bool separateShaderObjects = false;

	// For older desktop GLSL targets than version 420, the
	// GL_ARB_shading_language_420pack extensions is used to be able to support
	// layout(binding) on UBOs and samplers.
	// If disabled on older targets, binding decorations will be stripped.
	public bool enable420PackExtension = true;

	// In non-Vulkan GLSL, emit push constant blocks as UBOs rather than plain uniforms.
	public bool emitPushConstantAsUniformBuffer = false;

	// Always emit uniform blocks as plain uniforms, regardless of the GLSL version, even when UBOs are supported.
	// Does not apply to shader storage or push constant blocks.
	public bool emitUniformBufferAsPlainUniforms = false;

	// In GLSL, force use of I/O block flattening, similar to
	// what happens on legacy GLSL targets for blocks and structs.
	public bool forceFlattenedIoBlocks = false;

	// Loading row-major matrices from UBOs on older AMD Windows OpenGL drivers is problematic.
	// To load these types correctly, we must generate a wrapper. them in a dummy function which only purpose is to
	// ensure row_major decoration is actually respected.
	// This workaround may cause significant performance degeneration on some Android devices.
	public bool enableRowMajorLoadWorkaround = true;

	// If non-zero, controls layout(num_views = N) in; in GL_OVR_multiview2.
	public uint ovrMultiviewViewCount = 0;

    // GLSL only, for HLSL version of this option, see CompilerHLSL.
	// If true, the backend will assume that InstanceIndex will need to apply
	// a base instance offset. Set to false if you know you will never use base instance
	// functionality as it might remove some internal uniforms.
    public bool supportNonzeroBaseInstance = true;
	
	public Precision defaultFloatPrecision = Precision.Mediump;
    public Precision defaultIntPrecision = Precision.Highp;

    internal readonly unsafe void Apply(Context ctx, Native.CompilerOptions* options)
	{
        ctx.Throw(spvc_compiler_options_set_uint(options, Option.GLSL_VERSION, version));
        ctx.Throw(spvc_compiler_options_set_bool(options, Option.GLSL_ES, ES));
	    ctx.Throw(spvc_compiler_options_set_bool(options, Option.GLSL_VULKAN_SEMANTICS, vulkanSemantics));
	    ctx.Throw(spvc_compiler_options_set_bool(options, Option.GLSL_SEPARATE_SHADER_OBJECTS, separateShaderObjects));
	    ctx.Throw(spvc_compiler_options_set_bool(options, Option.GLSL_ENABLE_420PACK_EXTENSION, enable420PackExtension));
	    ctx.Throw(spvc_compiler_options_set_bool(options, Option.GLSL_EMIT_PUSH_CONSTANT_AS_UNIFORM_BUFFER, emitPushConstantAsUniformBuffer));
	    ctx.Throw(spvc_compiler_options_set_bool(options, Option.GLSL_EMIT_UNIFORM_BUFFER_AS_PLAIN_UNIFORMS, emitUniformBufferAsPlainUniforms));
	    ctx.Throw(spvc_compiler_options_set_bool(options, Option.GLSL_FORCE_FLATTENED_IO_BLOCKS, forceFlattenedIoBlocks));
	    ctx.Throw(spvc_compiler_options_set_bool(options, Option.GLSL_ENABLE_ROW_MAJOR_LOAD_WORKAROUND, enableRowMajorLoadWorkaround));
	    ctx.Throw(spvc_compiler_options_set_uint(options, Option.GLSL_OVR_MULTIVIEW_VIEW_COUNT, ovrMultiviewViewCount));
        ctx.Throw(spvc_compiler_options_set_bool(options, Option.GLSL_SUPPORT_NONZERO_BASE_INSTANCE, supportNonzeroBaseInstance));
	    ctx.Throw(spvc_compiler_options_set_uint(options, Option.GLSL_ES_DEFAULT_FLOAT_PRECISION_HIGHP, (uint)defaultFloatPrecision));
        ctx.Throw(spvc_compiler_options_set_uint(options, Option.GLSL_ES_DEFAULT_INT_PRECISION_HIGHP, (uint)defaultIntPrecision));
    }
}