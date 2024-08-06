using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET.NativeBindings;

#pragma warning disable 1591

public partial struct Constant 
{ 
    const string LibName = NativeLibrary.LibraryName;

    /*
     * No stdint.h until C99, sigh :(
     * For smaller types, the result is sign or zero-extended as appropriate.
     * Maps to C++ API.
     * TODO: The SPIRConstant query interface and modification interface is not quite complete.
     */
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial float spvc_constant_get_scalar_fp16(Constant* constant, uint column, uint row);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial float spvc_constant_get_scalar_fp32(Constant* constant, uint column, uint row);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial double spvc_constant_get_scalar_fp64(Constant* constant, uint column, uint row);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial uint spvc_constant_get_scalar_u32(Constant* constant, uint column, uint row);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial int spvc_constant_get_scalar_i32(Constant* constant, uint column, uint row);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial ushort spvc_constant_get_scalar_u16(Constant* constant, uint column, uint row);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial short spvc_constant_get_scalar_i16(Constant* constant, uint column, uint row);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial byte spvc_constant_get_scalar_u8(Constant* constant, uint column, uint row);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial sbyte spvc_constant_get_scalar_i8(Constant* constant, uint column, uint row);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial void spvc_constant_get_subconstants(Constant* constant, ConstantID** constituents, out nuint count);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial ulong spvc_constant_get_scalar_u64(Constant* constant, uint column, uint row);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial long spvc_constant_get_scalar_i64(Constant* constant, uint column, uint row);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial TypeID spvc_constant_get_type(Constant* constant);

/*
 * C implementation of the C++ api.
 */
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial void spvc_constant_set_scalar_fp16(Constant* constant, uint column, uint row, ushort value);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial void spvc_constant_set_scalar_fp32(Constant* constant, uint column, uint row, float value);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial void spvc_constant_set_scalar_fp64(Constant* constant, uint column, uint row, double value);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial void spvc_constant_set_scalar_u32(Constant* constant, uint column, uint row, uint value);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial void spvc_constant_set_scalar_i32(Constant* constant, uint column, uint row, int value);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial void spvc_constant_set_scalar_u64(Constant* constant, uint column, uint row, ulong value);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial void spvc_constant_set_scalar_i64(Constant* constant, uint column, uint row, long value);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial void spvc_constant_set_scalar_u16(Constant* constant, uint column, uint row, ushort value);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial void spvc_constant_set_scalar_i16(Constant* constant, uint column, uint row, short value);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial void spvc_constant_set_scalar_u8(Constant* constant, uint column, uint row, byte value);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial void spvc_constant_set_scalar_i8(Constant* constant, uint column, uint row, sbyte value);
}