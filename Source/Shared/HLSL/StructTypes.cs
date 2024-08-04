using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace SPIRVCross.NET.HLSL;

/* See C++ API. */
[StructLayout(LayoutKind.Sequential)]
public struct RootConstants
{
	public uint start;
	public uint end;
	public uint binding;
	public uint space;
}

/* See C++ API. */
[StructLayout(LayoutKind.Sequential)]
public struct VertexAttributeRemap
{
	public uint location;
	public string semantic;
}

/* Maps to C++ API. */
[StructLayout(LayoutKind.Sequential)]
public struct ResourceBindingMapping
{
	public uint register_space;
	public uint register_binding;
} 

[StructLayout(LayoutKind.Sequential)]
public struct ResourceBinding()
{
	public ExecutionModel stage = (ExecutionModel)0x7fffffff;

	public uint desc_set;
	public uint binding;

	public ResourceBindingMapping cbv, uav, srv, sampler;
}