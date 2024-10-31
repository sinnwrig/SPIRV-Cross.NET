using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET.HLSL;

using static NativeBindings.Compiler;
using Option = NativeBindings.CompilerOption;

/// <summary>
/// HLSL specific SPIR-V cross-compilation options
/// </summary>
public struct HLSLCompilerOptions()
{
	/// <summary>
	/// The HLSL shader model to target.
	/// </summary>
	public uint shaderModel = 30;

	/// <summary>
	/// Allows the PointSize builtin in SM 4.0+, and ignores it, as PointSize is not supported in SM 4+.
	/// </summary>
	public bool pointSizeCompat = false;

	/// <summary>
	/// Allows the PointCoord builtin, returns float2(0.5, 0.5), as PointCoord is not supported in HLSL.
	/// </summary>
	public bool pointCoordCompat = false;

	/// <summary>
	/// <para>If true, the backend will assume that VertexIndex and InstanceIndex will need to apply
	/// a base offset, and you will need to fill in a cbuffer with offsets.</para>
	/// Set to false if you know you will never use base instance or base vertex
	/// functionality as it might remove an internal cbuffer.
	/// </summary>
	public bool supportNonzeroBaseVertexBaseInstance = false;

	/// <summary>
	/// <para>Forces a storage buffer to always be declared as UAV, even if the readonly decoration is used.
	/// By default, a readonly storage buffer will be declared as ByteAddressBuffer (SRV) instead.</para>
	/// Alternatively, use SetHlslForceStorageBufferAsUav() to specify individually.
	/// </summary>
	public bool forceStorageBufferAsUav = false;

	/// <summary>
	/// Forces any storage image type marked as NonWritable to be considered an SRV instead.
	/// For this to work with function call parameters, NonWritable must be considered to be part of the type system
	/// so that NonWritable image arguments are also translated to Texture rather than RWTexture.
	/// </summary>
	public bool nonwritableUAVTextureAsSRV = false;

	/// <summary>
	/// <para>Enables native 16-bit types. Needs SM 6.2.
	/// Uses half/int16_t/uint16_t instead of min16* types.</para>
	/// Also adds support for 16-bit load-store from (RW)ByteAddressBuffer.
	/// </summary>
	public bool enable16bitTypes = false;

	/// <summary>
	/// <para>If matrices are used as IO variables, flatten the attribute declaration to use
	/// TEXCOORD{N,N+1,N+2,...} rather than TEXCOORDN_{0,1,2,3}.</para>
	/// If AddVertexAttributeRemap() is used and this feature is used,
	/// the semantic name will be queried once per active location.
	/// </summary>
	public bool flattenMatrixVertexInputSemantics = false;

	/// <summary>
	/// Rather than emitting main() for the entry point, use the name in SPIR-V.
	/// </summary>
	public bool useEntryPointName = false;

	/// <summary>
	/// <para>Preserve (RW)StructuredBuffer types if the input source was HLSL.</para>
	/// This relies on UserTypeGOOGLE to encode the buffer type either as "structuredbuffer" or "rwstructuredbuffer"
	/// whereas the type can be extended with an optional subtype, e.g. "structuredbuffer:int".
	/// </summary>
	public bool preserveStructuredBuffers = false;


	internal readonly unsafe void Apply(Context ctx, NativeBindings.CompilerOptions* options)
	{
		ctx.Throw(spvc_compiler_options_set_uint(options, Option.HLSL_SHADER_MODEL, shaderModel));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.HLSL_POINT_SIZE_COMPAT, pointSizeCompat));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.HLSL_POINT_COORD_COMPAT, pointCoordCompat));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.HLSL_SUPPORT_NONZERO_BASE_VERTEX_BASE_INSTANCE, supportNonzeroBaseVertexBaseInstance));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.HLSL_FORCE_STORAGE_BUFFER_AS_UAV, forceStorageBufferAsUav));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.HLSL_NONWRITABLE_UAV_TEXTURE_AS_SRV, nonwritableUAVTextureAsSRV));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.HLSL_ENABLE_16BIT_TYPES, enable16bitTypes));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.HLSL_FLATTEN_MATRIX_VERTEX_INPUT_SEMANTICS, flattenMatrixVertexInputSemantics));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.HLSL_USE_ENTRY_POINT_NAME, useEntryPointName));
		ctx.Throw(spvc_compiler_options_set_bool(options, Option.HLSL_PRESERVE_STRUCTURED_BUFFERS, preserveStructuredBuffers));
	}
}