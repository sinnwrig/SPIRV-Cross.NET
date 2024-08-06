using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET.NativeBindings;

#pragma warning disable 1591

/* See C++ API. */
[StructLayout(LayoutKind.Sequential)]
public unsafe struct VertexAttributeRemap
{
	public uint location;
	public byte* semantic;
}