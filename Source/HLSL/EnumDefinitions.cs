using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET.HLSL;

#pragma warning disable 1591 // Missing XML comment for publicly visible type or member

/* Maps to C++ API. */

/// <summary>
/// Flags used by <see cref="HLSLCrossCompiler.SetResourceBindingFlags"/> 
/// </summary>
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