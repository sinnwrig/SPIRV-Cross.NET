using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using SPIRVCross.NET.HLSL;

namespace SPIRVCross.NET.NativeBindings;

#pragma warning disable 1591

public static partial class HLSLCompiler
{
    const string LibName = "spirv-cross";
    
    /*
    * HLSL specifics.
    * Maps to C++ API.
    */

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_hlsl_set_root_constants_layout(Compiler* compiler, RootConstants* constant_info, nuint count);

    [LibraryImport(LibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_hlsl_add_vertex_attribute_remap(Compiler* compiler, VertexAttributeRemap* remap, nuint remaps);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial VariableID spvc_compiler_hlsl_remap_num_workgroups_builtin(Compiler* compiler);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_hlsl_set_resource_binding_flags(Compiler* compiler, BindingFlags flags);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_compiler_hlsl_add_resource_binding(Compiler* compiler, in ResourceBinding binding);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial CBool spvc_compiler_hlsl_is_resource_used(Compiler* compiler, ExecutionModel model, uint set, uint binding);
}