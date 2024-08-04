using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET;

using static Native.Compiler;
using Option = Native.CompilerOption;

/// <summary>
/// Common SPIR-V cross-compilation options
/// </summary>
public struct CompilerOptions()
{
	/// <summary>
	/// Debug option to always emit temporary variables for all expressions.
	/// </summary>
	public bool forceTemporary = false;

	/// <summary>
	/// <para>Flattens multidimensional arrays, e.g. float foo[a][b][c] into single-dimensional arrays,
	/// e.g. float foo[a * b * c].</para>
	/// This function does not change the actual SPIRType of any object.
	/// Only the generated code, including declarations of interface variables are changed to be single array dimension.
	/// </summary>
	public bool flattenMultidimensionalArrays = false;

	/// <summary>
	/// <para>In cases where readonly/writeonly decoration are not used at all,
	/// we try to deduce which qualifier(s) we should actually used, since actually emitting
	/// read-write decoration is very rare, and older glslang/HLSL compilers tend to just emit readwrite as a matter of fact.</para>
	/// The default (false) is to trust the decorations set by the SPIR-V, 
	/// but if the SPIR-V is likely to have bad decorations, it's recommended to set this to true and enable automatic deduction for these cases.
	/// </summary>
	public bool enableStorageImageQualifierDeduction = false;

	/// <summary>
	/// On some targets (WebGPU), uninitialized variables are banned.
	/// If this is enabled, all variables (temporaries, Private, Function)
	/// which would otherwise be uninitialized will now be initialized to 0 instead.
	/// </summary>
	public bool forceZeroInitializedVariables = false;

	/// <summary>
	/// <para>For opcodes where we have to perform explicit additional nan checks, very ugly code is generated.</para>
	/// <para>If we opt-in, ignore these requirements.</para>
	/// <para>In opcodes like NClamp/NMin/NMax and FP compare, ignore NaN behavior.</para>
	/// Use FClamp/FMin/FMax semantics for clamps and lets implementation choose ordered or unordered
	/// compares.
	/// </summary>
	public bool relaxNanChecks = false;


	/// <summary>
	/// GLSL: In vertex-like shaders, rewrite [0, w] depth (Vulkan/D3D style) to [-w, w] depth (GL style).
	/// MSL: In vertex-like shaders, rewrite [-w, w] depth (GL style) to [0, w] depth.
	/// HLSL: In vertex-like shaders, rewrite [-w, w] depth (GL style) to [0, w] depth.
	/// </summary>
    public bool fixupClipSpace = false;

	/// <summary>
	/// In vertex-like shaders, inverts gl_Position.y or equivalent.
	/// </summary>
    public bool flipVertexY = false;

    internal readonly unsafe void Apply(Context ctx, Native.CompilerOptions* options)
	{
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.FORCE_TEMPORARY, forceTemporary));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.FLATTEN_MULTIDIMENSIONAL_ARRAYS, flattenMultidimensionalArrays));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.ENABLE_STORAGE_IMAGE_QUALIFIER_DEDUCTION, enableStorageImageQualifierDeduction));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.FORCE_ZERO_INITIALIZED_VARIABLES, forceZeroInitializedVariables));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.RELAX_NAN_CHECKS, relaxNanChecks));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.FIXUP_DEPTH_CONVENTION, fixupClipSpace));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.FLIP_VERTEX_Y, flipVertexY));
    }
}