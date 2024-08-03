using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET.Native;

#pragma warning disable 1591

public partial struct Resources 
{ 
    const string LibName = NativeLibrary.LibraryName;

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_resources_get_resource_list_for_type(Resources* resources, ResourceType type, ReflectedResource** resource_list, out nuint resource_size);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_resources_get_builtin_resource_list_for_type(Resources* resources, BuiltinResourceType type, ReflectedBuiltinResource** resource_list, out nuint resource_size);
}