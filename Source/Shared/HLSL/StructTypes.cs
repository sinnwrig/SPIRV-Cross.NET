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

[CustomMarshaller(typeof(VertexAttributeRemap), MarshalMode.Default, typeof(RemapMarshaller))]
internal static unsafe class RemapMarshaller
{
	[StructLayout(LayoutKind.Sequential)]
	internal struct VertexAttributeRemapUnmanaged
	{
	    public uint location;
	    public IntPtr semantic;
	}

	public static VertexAttributeRemapUnmanaged* ConvertToUnmanaged(VertexAttributeRemap managed)
    {
        if (managed.semantic == null)
        {
            throw new ArgumentNullException(nameof(managed.semantic));
        }

        // Allocate memory for the unmanaged struct
        var unmanaged = (VertexAttributeRemapUnmanaged*)Marshal.AllocHGlobal(sizeof(VertexAttributeRemapUnmanaged));

        unmanaged->location = managed.location;
        unmanaged->semantic = Marshal.StringToHGlobalAnsi(managed.semantic);

        return unmanaged;
    }

    public static VertexAttributeRemap ConvertToManaged(VertexAttributeRemapUnmanaged* unmanaged)
    {
        if (unmanaged == null)
        {
            throw new ArgumentNullException(nameof(unmanaged));
        }

        var managed = new VertexAttributeRemap
        {
            location = unmanaged->location,
            semantic = Marshal.PtrToStringAnsi(unmanaged->semantic) ?? ""
        };

        return managed;
    }

    public static void Free(VertexAttributeRemapUnmanaged* unmanaged)
    {
        if (unmanaged == null)
        {
            return;
        }

        // Free the unmanaged semantic string
        Marshal.FreeHGlobal(unmanaged->semantic);

        // Free the unmanaged struct
        Marshal.FreeHGlobal((IntPtr)unmanaged);
    }
}

/* See C++ API. */
[StructLayout(LayoutKind.Sequential)]
[NativeMarshalling(typeof(RemapMarshaller))]
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