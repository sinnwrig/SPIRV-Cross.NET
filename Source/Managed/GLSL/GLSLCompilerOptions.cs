using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET.GLSL;

using static Native.Compiler;
using Option = Native.CompilerOption;

/// <summary>
/// GLSL specific SPIR-V cross-compilation options
/// </summary>
public struct GLSLCompilerOptions()
{
    /// <summary>
	/// The shading language version.
	/// </summary>
    public uint version = 450;

    /// <summary>
	/// Emit the OpenGL ES shading language instead of desktop OpenGL.
	/// </summary>
    public bool ES = false;

	/// <summary>
	/// <para>If true, Vulkan GLSL features are used instead of GL-compatible features.</para>
	/// Mostly useful for debugging SPIR-V files.
	/// </summary>
	public bool vulkanSemantics = false;

	/// <summary>
	/// <para>If true, gl_PerVertex is explicitly redeclared in vertex, geometry and tessellation shaders.
	/// The members of gl_PerVertex is determined by which built-ins are declared by the shader.</para>
	/// This option is ignored in ES versions, as redeclaration in ES is not required, and it depends on a different extension
	/// (EXT_shader_io_blocks) which makes things a bit more fuzzy.
	/// </summary>
	public bool separateShaderObjects = false;

	/// <summary>
	/// For older desktop GLSL targets than version 420, the
	/// GL_ARB_shading_language_420pack extensions is used to be able to support
	/// layout(binding) on UBOs and samplers.
	/// <para>If disabled on older targets, binding decorations will be stripped.</para>
	/// </summary>
	public bool enable420PackExtension = true;

	/// <summary>
	/// In non-Vulkan GLSL, emit push constant blocks as UBOs rather than plain uniforms.
	/// </summary>
	public bool emitPushConstantAsUniformBuffer = false;

	/// <summary>
	/// <para>Always emit uniform blocks as plain uniforms, regardless of the GLSL version, even when UBOs are supported.</para>
	/// Does not apply to shader storage or push constant blocks.
	/// </summary>
	public bool emitUniformBufferAsPlainUniforms = false;

	/// <summary>
	/// In GLSL, force use of I/O block flattening, similar to
	/// what happens on legacy GLSL targets for blocks and structs.
	/// </summary>
	public bool forceFlattenedIoBlocks = false;

	/// <summary>
	/// <para>Loading row-major matrices from UBOs on older AMD Windows OpenGL drivers is problematic.
	/// To load these types correctly, we must generate a wrapper dummy function whose only purpose is to
	/// ensure row_major decoration is actually respected.</para>
	/// This workaround may cause significant performance degeneration on some Android devices.
	/// </summary>
	public bool enableRowMajorLoadWorkaround = true;

	/// <summary>
	/// If non-zero, controls layout(num_views = N) in; in GL_OVR_multiview2.
	/// </summary>
	public uint ovrMultiviewViewCount = 0;

    /// <summary>
	/// If true, the backend will assume that InstanceIndex will need to apply
	/// a base instance offset. Set to false if you know you will never use base instance
	/// functionality as it might remove some internal uniforms.
	/// </summary>
    public bool supportNonzeroBaseInstance = true;
	
	/// <summary>
	/// The default floating point precision to set in the GLSL source.
	/// </summary>
	public Precision defaultFloatPrecision = Precision.Mediump;

	/// <summary>
	/// The default integer precision to set in the GLSL source.
	/// </summary>
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