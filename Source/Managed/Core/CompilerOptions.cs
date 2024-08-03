using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET;

using static Native.Compiler;
using Option = Native.CompilerOption;

public struct CompilerOptions()
{
	// Debug option to always emit temporary variables for all expressions.
	public bool forceTemporary = false;

	// Flattens multidimensional arrays, e.g. float foo[a][b][c] into single-dimensional arrays,
	// e.g. float foo[a * b * c].
	// This function does not change the actual SPIRType of any object.
	// Only the generated code, including declarations of interface variables are changed to be single array dimension.
	public bool flattenMultidimensionalArrays = false;

	// In cases where readonly/writeonly decoration are not used at all,
	// we try to deduce which qualifier(s) we should actually used, since actually emitting
	// read-write decoration is very rare, and older glslang/HLSL compilers tend to just emit readwrite as a matter of fact.
	// The default (true) is to enable automatic deduction for these cases, but if you trust the decorations set
	// by the SPIR-V, it's recommended to set this to false.
	public bool enableStorageImageQualifierDeduction = false;

	// On some targets (WebGPU), uninitialized variables are banned.
	// If this is enabled, all variables (temporaries, Private, Function)
	// which would otherwise be uninitialized will now be initialized to 0 instead.
	public bool forceZeroInitializedVariables = false;

	// For opcodes where we have to perform explicit additional nan checks, very ugly code is generated.
	// If we opt-in, ignore these requirements.
	// In opcodes like NClamp/NMin/NMax and FP compare, ignore NaN behavior.
	// Use FClamp/FMin/FMax semantics for clamps and lets implementation choose ordered or unordered
	// compares.
	public bool relaxNanChecks = false;

	// GLSL: In vertex-like shaders, rewrite [0, w] depth (Vulkan/D3D style) to [-w, w] depth (GL style).
	// MSL: In vertex-like shaders, rewrite [-w, w] depth (GL style) to [0, w] depth.
	// HLSL: In vertex-like shaders, rewrite [-w, w] depth (GL style) to [0, w] depth.
    public bool fixupClipSpace = false;

    // In vertex-like shaders, inverts gl_Position.y or equivalent.
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