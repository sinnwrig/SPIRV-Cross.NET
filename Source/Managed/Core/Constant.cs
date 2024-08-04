using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET;

using static Native.Constant;

/// <summary>
/// A constant value
/// </summary>
public sealed unsafe class Constant : ContextChild
{ 
    private Native.Constant* _constant;
    internal Native.Constant* constant
    {
        get
        {
            Validate();
            return _constant;
        }
    }

    internal Constant(Context context, Native.Constant* constant) : base(context)
    {
        this._constant = constant;
    }

    /// <summary>
    /// Returns a list of subconstants for composites which are constant arrays, etc.
    /// </summary>
    public ReadOnlySpan<ConstantID> GetSubconstants()
    {
        ConstantID* constituentsPtr = null;
        spvc_constant_get_subconstants(constant, &constituentsPtr, out nuint count);
        return new ReadOnlySpan<ConstantID>(constituentsPtr, (int)count);
    }

    /// <summary>
    /// The type of the constant.
    /// </summary>
    public TypeID TypeID
        => spvc_constant_get_type(constant);

    /// <summary>
    /// Gets the scalar 16-bit float value of this constant. If this constant is a matrix or vector, column and row must be set appropriately.
    /// </summary>
    public float GetScalarFloat16(uint column, uint row)
        => spvc_constant_get_scalar_fp16(constant, column, row);

    /// <summary>
    /// Gets the scalar 32-bit float value of this constant. If this constant is a matrix or vector, column and row must be set appropriately.
    /// </summary>
    public float GetScalarFloat32(uint column, uint row)
        => spvc_constant_get_scalar_fp32(constant, column, row);

    /// <summary>
    /// Gets the scalar 64-bit float value of this constant. If this constant is a matrix or vector, column and row must be set appropriately.
    /// </summary>
    public double GetScalarFloat64(uint column, uint row)
        => spvc_constant_get_scalar_fp64(constant, column, row);

    /// <summary>
    /// Gets the scalar unsigned 8-bit integer value of this constant. If this constant is a matrix or vector, column and row must be set appropriately.
    /// </summary>
    public byte GetScalarUInt8(uint column, uint row)
        => spvc_constant_get_scalar_u8(constant, column, row);

    /// <summary>
    /// Gets the scalar signed 8-bit integer value of this constant. If this constant is a matrix or vector, column and row must be set appropriately.
    /// </summary>
    public sbyte GetScalarInt8(uint column, uint row)
        => spvc_constant_get_scalar_i8(constant, column, row);

    /// <summary>
    /// Gets the scalar unsigned 16-bit integer value of this constant. If this constant is a matrix or vector, column and row must be set appropriately.
    /// </summary>
    public ushort GetScalarUInt16(uint column, uint row)
        => spvc_constant_get_scalar_u16(constant, column, row);

    /// <summary>
    /// Gets the scalar signed 16-bit integer value of this constant. If this constant is a matrix or vector, column and row must be set appropriately.
    /// </summary>
    public short GetScalarInt16(uint column, uint row)
        => spvc_constant_get_scalar_i16(constant, column, row);

    /// <summary>
    /// Gets the scalar unsigned 32-bit integer value of this constant. If this constant is a matrix or vector, column and row must be set appropriately.
    /// </summary>
    public uint GetScalarUInt32(uint column, uint row)
        => spvc_constant_get_scalar_u32(constant, column, row);

    /// <summary>
    /// Gets the scalar signed 32-bit integer value of this constant. If this constant is a matrix or vector, column and row must be set appropriately.
    /// </summary>
    public int GetScalarInt32(uint column, uint row)
        => spvc_constant_get_scalar_i32(constant, column, row);

    /// <summary>
    /// Gets the scalar unsigned 64-bit integer value of this constant. If this constant is a matrix or vector, column and row must be set appropriately.
    /// </summary>
    public ulong GetScalarUInt64(uint column, uint row)
        => spvc_constant_get_scalar_u64(constant, column, row);

    /// <summary>
    /// Gets the scalar signed 64-bit integer value of this constant. If this constant is a matrix or vector, column and row must be set appropriately.
    /// </summary>
    public long GetScalarInt64(uint column, uint row)
        => spvc_constant_get_scalar_i64(constant, column, row);


    /// <summary>
    /// Sets the scalar 16-bit float value of this constant. If this constant is a matrix or vector, column and row must be set appropriately.
    /// </summary>
    /// <remarks>
    /// As there is no 16-bit floating point type available in C, a 16-bit integer must be substituted.
    /// </remarks>
    public void SetScalarFloat16(uint column, uint row, ushort value)
        => spvc_constant_set_scalar_fp16(constant, column, row, value);

    /// <summary>
    /// Sets the scalar 32-bit float value of this constant. If this constant is a matrix or vector, column and row must be set appropriately.
    /// </summary>
    public void SetScalarFloat32(uint column, uint row, float value)
        => spvc_constant_set_scalar_fp32(constant, column, row, value);

    /// <summary>
    /// Sets the scalar 64-bit float value of this constant. If this constant is a matrix or vector, column and row must be set appropriately.
    /// </summary>
    public void SetScalarFloat64(uint column, uint row, double value)
        => spvc_constant_set_scalar_fp64(constant, column, row, value);

    /// <summary>
    /// Sets the scalar unsigned 8-bit integer value of this constant. If this constant is a matrix or vector, column and row must be set appropriately.
    /// </summary>
    public void SetScalarUInt8(uint column, uint row, byte value)
        => spvc_constant_set_scalar_u8(constant, column, row, value);
    
    /// <summary>
    /// Sets the scalar signed 8-bit integer value of this constant. If this constant is a matrix or vector, column and row must be set appropriately.
    /// </summary>
    public void SetScalarInt8(uint column, uint row, sbyte value)
        => spvc_constant_set_scalar_i8(constant, column, row, value);

    /// <summary>
    /// Sets the scalar unsigned 16-bit integer value of this constant. If this constant is a matrix or vector, column and row must be set appropriately.
    /// </summary>
    public void SetScalarUInt16(uint column, uint row, ushort value)
        => spvc_constant_set_scalar_u16(constant, column, row, value);

    /// <summary>
    /// Sets the scalar signed 16-bit integer value of this constant. If this constant is a matrix or vector, column and row must be set appropriately.
    /// </summary>
    public void SetScalarInt16(uint column, uint row, short value)
        => spvc_constant_set_scalar_i16(constant, column, row, value);

    /// <summary>
    /// Sets the scalar unsigned 32-bit integer value of this constant. If this constant is a matrix or vector, column and row must be set appropriately.
    /// </summary>
    public void SetScalarUInt32(uint column, uint row, uint value)
        => spvc_constant_set_scalar_u32(constant, column, row, value);

    /// <summary>
    /// Sets the scalar signed 32-bit integer value of this constant. If this constant is a matrix or vector, column and row must be set appropriately.
    /// </summary>
    public void SetScalarInt32(uint column, uint row, int value)
        => spvc_constant_set_scalar_i32(constant, column, row, value);

    /// <summary>
    /// Sets the scalar unsigned 64-bit integer value of this constant. If this constant is a matrix or vector, column and row must be set appropriately.
    /// </summary>
    public void SetScalarUInt64(uint column, uint row, ulong value)
        => spvc_constant_set_scalar_u64(constant, column, row, value);

    /// <summary>
    /// Sets the scalar signed 64-bit integer value of this constant. If this constant is a matrix or vector, column and row must be set appropriately.
    /// </summary>
    public void SetScalarInt64(uint column, uint row, long value)
        => spvc_constant_set_scalar_i64(constant, column, row, value);
}