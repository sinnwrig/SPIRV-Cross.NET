using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET.Native;

#pragma warning disable 1591

public partial struct Type 
{
    const string LibName = NativeLibrary.LibraryName;

    /* Pulls out SPIRType::self. This effectively gives the type ID without array or pointer qualifiers.
     * This is necessary when reflecting decoration/name information on members of a struct,
     * which are placed in the base type, not the qualified type.
     * This is similar to SpirvCrossReflectedResource::base_type_id. */
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial TypeID spvc_type_get_base_type_id(Type* type);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial BaseValueType spvc_type_get_basetype(Type* type);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial uint spvc_type_get_bit_width(Type* type);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial uint spvc_type_get_vector_size(Type* type);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial uint spvc_type_get_columns(Type* type);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial uint spvc_type_get_num_array_dimensions(Type* type);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial CBool spvc_type_array_dimension_is_literal(Type* type, uint dimension);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial ID spvc_type_get_array_dimension(Type* type, uint dimension);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial uint spvc_type_get_num_member_types(Type* type);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial TypeID spvc_type_get_member_type(Type* type, uint index);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial StorageClass spvc_type_get_storage_class(Type* type);

    /* Image type query. */
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial TypeID spvc_type_get_image_sampled_type(Type* type);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Dimension spvc_type_get_image_dimension(Type* type);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial CBool spvc_type_get_image_is_depth(Type* type);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial CBool spvc_type_get_image_arrayed(Type* type);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial CBool spvc_type_get_image_multisampled(Type* type);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial CBool spvc_type_get_image_is_storage(Type* type);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial ImageFormat spvc_type_get_image_storage_format(Type* type);
    
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial AccessQualifier spvc_type_get_image_access_qualifier(Type* type);
}