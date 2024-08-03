using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET.HLSL;

using static Native.Compiler;
using Option = Native.CompilerOption;

public struct HLSLCompilerOptions()
{
    public uint shaderModel = 30; // TODO: map ps_4_0_level_9_0,... somehow

	// Allows the PointSize builtin in SM 4.0+, and ignores it, as PointSize is not supported in SM 4+.
	public bool pointSizeCompat = false;

	// Allows the PointCoord builtin, returns float2(0.5, 0.5), as PointCoord is not supported in HLSL.
	public bool pointCoordCompat = false;

	// If true, the backend will assume that VertexIndex and InstanceIndex will need to apply
	// a base offset, and you will need to fill in a cbuffer with offsets.
	// Set to false if you know you will never use base instance or base vertex
	// functionality as it might remove an internal cbuffer.
	public bool supportNonzeroBaseVertexBaseInstance = false;

	// Forces a storage buffer to always be declared as UAV, even if the readonly decoration is used.
	// By default, a readonly storage buffer will be declared as ByteAddressBuffer (SRV) instead.
	// Alternatively, use set_hlsl_force_storage_buffer_as_uav to specify individually.
	public bool forceStorageBufferAsUav = false;
	
	// Forces any storage image type marked as NonWritable to be considered an SRV instead.
	// For this to work with function call parameters, NonWritable must be considered to be part of the type system
	// so that NonWritable image arguments are also translated to Texture rather than RWTexture.
	public bool nonwritableUAVTextureAsSRV = false;

	// Enables native 16-bit types. Needs SM 6.2.
	// Uses half/int16_t/uint16_t instead of min16* types.
	// Also adds support for 16-bit load-store from (RW)ByteAddressBuffer.
	public bool enable16bitTypes = false;

	// If matrices are used as IO variables, flatten the attribute declaration to use
	// TEXCOORD{N,N+1,N+2,...} rather than TEXCOORDN_{0,1,2,3}.
	// If add_vertex_attribute_remap is used and this feature is used,
	// the semantic name will be queried once per active location.
	public bool flattenMatrixVertexInputSemantics = false;


    internal readonly unsafe void Apply(Context ctx, Native.CompilerOptions* options)
	{
        ctx.Throw(spvc_compiler_options_set_uint(options, Option.HLSL_SHADER_MODEL, shaderModel));
	    ctx.Throw(spvc_compiler_options_set_bool(options, Option.HLSL_POINT_SIZE_COMPAT, pointSizeCompat));
	    ctx.Throw(spvc_compiler_options_set_bool(options, Option.HLSL_POINT_COORD_COMPAT, pointCoordCompat));
	    ctx.Throw(spvc_compiler_options_set_bool(options, Option.HLSL_SUPPORT_NONZERO_BASE_VERTEX_BASE_INSTANCE, supportNonzeroBaseVertexBaseInstance));
	    ctx.Throw(spvc_compiler_options_set_bool(options, Option.HLSL_FORCE_STORAGE_BUFFER_AS_UAV, forceStorageBufferAsUav));
	    ctx.Throw(spvc_compiler_options_set_bool(options, Option.HLSL_NONWRITABLE_UAV_TEXTURE_AS_SRV, nonwritableUAVTextureAsSRV));
	    ctx.Throw(spvc_compiler_options_set_bool(options, Option.HLSL_ENABLE_16BIT_TYPES, enable16bitTypes));
	    ctx.Throw(spvc_compiler_options_set_bool(options, Option.HLSL_FLATTEN_MATRIX_VERTEX_INPUT_SEMANTICS, flattenMatrixVertexInputSemantics));
    }
}