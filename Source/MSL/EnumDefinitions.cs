using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET.MSL;

#pragma warning disable 1591 // Missing XML comment for publicly visible type or member

/// <summary>
/// Target Metal platforms.
/// </summary>
public enum Platform
{
    IOS = 0,
    MacOS = 1,
}

/// <summary>
/// Index types used by the SPIR-V to Metal compiler. 
/// </summary>
/// <remarks>
/// This enumeration is under the MSL namespace as only the Metal compiler makes use of index types.
/// </remarks>
public enum IndexType
{
	None = 0,
	UInt16 = 1,
	UInt32 = 2,
}

/// <summary>
/// Indicates the format of a shader interface variable. Currently limited to specifying
/// if the input is an 8-bit unsigned integer, 16-bit unsigned integer, or
/// some other format.
/// </summary>
public enum ShaderVariableFormat
{
	Other = 0,
	UInt8 = 1,
	UInt16 = 2,
	Any16 = 3,
	Any32 = 4,
}

/// <summary>
/// Indicates the rate at which a variable changes value, one of: per-vertex,
/// per-primitive, or per-patch.
/// </summary>
public enum ShaderVariableRate
{
	PerVertex = 0,
	PerPrimitive = 1,
	PerPatch = 2,
} 

/// <summary>
/// MSL sampler coordinate modes.
/// </summary>
public enum SamplerCoord
{
	Normalized = 0,
	Pixel = 1,
}

/// <summary>
/// MSL sampler filter modes.
/// </summary>
public enum SamplerFilter
{
	Nearest = 0,
	Linear = 1,
}

/// <summary>
/// MSL mipmap sampler filter modes.
/// </summary>
public enum SamplerMipFilter
{
	None = 0,
	Nearest = 1,
	Linear = 2,
}

/// <summary>
/// MSL sampler address modes.
/// </summary>
public enum SamplerAddress
{
	ClampToZero = 0,
	ClampToEdge = 1,
	ClampToBorder = 2,
	Repeat = 3,
	MirroredRepeat = 4,
}

/// <summary>
/// MSL sampler compare function modes.
/// </summary>
public enum SamplerCompareFunc
{
	Never = 0,
	Less = 1,
	LessEqual = 2,
	Greater = 3,
	GreaterEqual = 4,
	Equal = 5,
	NotEqual = 6,
	Always = 7,
}

/// <summary>
/// MSL sampler border color. Used with a sampler that has an address mode of <see cref="SamplerAddress.ClampToBorder"/>. 
/// </summary>
public enum SamplerBorderColor
{
	TransparentBlack = 0,
	OpaqueBlack = 1,
	OpaqueWhite = 2,
}

/// <summary>
/// MSL format resolutions.
/// </summary>
public enum FormatResolution
{
	_444 = 0,
	_422,
	_420,
}

/// <summary>
/// MSL chroma location types.
/// </summary>
public enum ChromaLocation
{
	CositedEven = 0,
	Midpoint,
}

/// <summary>
/// MSL component swizzle types. Used by <see cref="SamplerYCBCRConversion"/>. 
/// </summary>
public enum ComponentSwizzle
{
	Identity = 0,
	Zero,
	One,
	R,
	G,
	B,
	A,
}

/*
#define SPVC_MSL_PUSH_CONSTANT_DESC_SET (~(0u))
#define SPVC_MSL_PUSH_CONSTANT_BINDING (0)
#define SPVC_MSL_SWIZZLE_BUFFER_BINDING (~(1u))
#define SPVC_MSL_BUFFER_SIZE_BUFFER_BINDING (~(2u))
#define SPVC_MSL_ARGUMENT_BUFFER_BINDING (~(3u))
*/

/// <summary>
/// MSL Y'CbCr sampler model conversion modes.
/// </summary>
public enum SamplerYCBCRModelConversion
{
	RGB_Identity = 0,
	YCBCR_Identity,
	YCBCR_BT_709,
	YCBCR_BT_601,
	YCBCR_BT_2020,
}

/// <summary>
/// MSL Y'CbCr sampler ranges.
/// </summary>
public enum SamplerYCBCRRange
{
	ITU_Full = 0,
	ITU_Narrow,
}

/// <summary>
/// Defines Metal argument buffer tier levels.
/// </summary>
public enum ArgumentBuffersTier
{
	Tier1 = 0,
	Tier2 = 1,
};