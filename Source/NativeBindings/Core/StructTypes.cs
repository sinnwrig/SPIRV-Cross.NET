using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET.NativeBindings;

#pragma warning disable 1591

public unsafe struct Set { }
public unsafe struct ParsedIR { }
public unsafe struct CompilerOptions { }

/* See C++ API. */
[StructLayout(LayoutKind.Sequential)]
public unsafe struct EntryPoint
{
	public ExecutionModel executionModel;

	internal byte* name;
}

/* See C++ API. */
[StructLayout(LayoutKind.Sequential)]
public unsafe struct ReflectedResource
{
	public VariableID id;
	public TypeID base_type_id;
	public TypeID type_id;

	public byte* name;
}

[StructLayout(LayoutKind.Sequential)]
public struct ReflectedBuiltinResource
{
	public BuiltIn builtin;
	public TypeID value_type_id;
	public ReflectedResource resource;
}