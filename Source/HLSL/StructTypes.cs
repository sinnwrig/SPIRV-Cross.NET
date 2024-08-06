using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET.HLSL;

#pragma warning disable 1591 // Missing XML comment for publicly visible type or member

/// <summary>
/// <para>Specifying a root constant (d3d12) or push constant range (vulkan).</para>
///
/// `start` and `end` denotes the range of the root constant in bytes.
/// Both values need to be multiple of 4.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct RootConstants
{
	public uint start;
	public uint end;
	public uint binding;
	public uint space;
}

/// <summary>
/// Interface which remaps vertex inputs to a fixed semantic name to make linking easier.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct VertexAttributeRemap
{
	public uint location;
	public string semantic;
}

/// <summary>
/// A mapping for the register space and binding of a <see cref="ResourceBinding"/>'s resource types. 
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct ResourceBindingMapping
{
	public uint register_space;
	public uint register_binding;
} 

/// <summary>
/// <para>By matching stage, desc_set and binding for a SPIR-V resource,
/// register bindings are set based on whether the HLSL resource is a
/// CBV, UAV, SRV or Sampler. A single binding in SPIR-V might contain multiple
/// resource types, e.g. COMBINED_IMAGE_SAMPLER, and SRV/Sampler bindings will be used respectively.</para>
/// <para>On SM 5.0 and lower, register_space is ignored.</para>
///
/// To remap a push constant block which does not have any desc_set/binding associated with it,
/// use ResourceBindingPushConstant{DescriptorSet,Binding} as values for desc_set/binding.
/// For deeper control of push constants, SetRootConstantLayouts() can be used instead.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct ResourceBinding()
{
	public ExecutionModel stage = (ExecutionModel)0x7fffffff;

	public uint desc_set;
	public uint binding;

	public ResourceBindingMapping cbv, uav, srv, sampler;
}