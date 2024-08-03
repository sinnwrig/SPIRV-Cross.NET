using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET.MSL;

/* Maps to C++ API. */
public enum Platform
{
	IOS = 0,
	MacOS = 1,
}

/* Maps to C++ API. */
public enum IndexType
{
	None = 0,
	UInt16 = 1,
	UInt32 = 2,
}

/* Maps to C++ API. */
public enum ShaderVariableFormat
{
	Other = 0,
	UInt8 = 1,
	UInt16 = 2,
	Any16 = 3,
	Any32 = 4,
}

/* Maps to C++ API. */
public enum ShaderVariableRate
{
	PerVertex = 0,
	PerPrimitive = 1,
	PerPatch = 2,
} 



/* Maps to C++ API. */
public enum SamplerCoord
{
	Normalized = 0,
	Pixel = 1,
}

/* Maps to C++ API. */
public enum SamplerFilter
{
	Nearest = 0,
	Linear = 1,
}

/* Maps to C++ API. */
public enum SamplerMipFilter
{
	None = 0,
	Nearest = 1,
	Linear = 2,
}

/* Maps to C++ API. */
public enum SamplerAddress
{
	ClampToZero = 0,
	ClampToEdge = 1,
	ClampToBorder = 2,
	Repeat = 3,
	MirroredRepeat = 4,
}

/* Maps to C++ API. */
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

/* Maps to C++ API. */
public enum SamplerBorderColor
{
	TransparentBlack = 0,
	OpaqueBlack = 1,
	OpaqueWhite = 2,
}

/* Maps to C++ API. */
public enum FormatResolution
{
	_444 = 0,
	_422,
	_420,
}

/* Maps to C++ API. */
public enum ChromaLocation
{
	CositedEven = 0,
	Midpoint,
}

/* Maps to C++ API. */
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

/* Maps to C++ API. */
public enum SamplerYCBCRModelConversion
{
	RGB_Identity = 0,
	YCBCR_Identity,
	YCBCR_BT_709,
	YCBCR_BT_601,
	YCBCR_BT_2020,
}

/* Maps to C+ API. */
public enum SamplerYCBCRRange
{
	ITU_Full = 0,
	ITU_Narrow,
}

// Defines Metal argument buffer tier levels.
// Uses same values as Metal MTLArgumentBuffersTier enumeration.
public enum ArgumentBuffersTier
{
	Tier1 = 0,
	Tier2 = 1,
};