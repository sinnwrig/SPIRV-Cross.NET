using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET.HLSL;

/* Maps to C++ API. */
[Flags]
public enum BindingFlags
{
	None = 0,
	PushConstant = 1 << 0,
	CBV = 1 << 1,
	SRV = 1 << 2,
	UAV = 1 << 3,
	Sampler = 1 << 4,
	All = 0x7fffffff
}