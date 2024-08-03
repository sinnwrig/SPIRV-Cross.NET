using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET;

using static Native.Constant;

public sealed unsafe class Constant : ChildObject<Reflector>
{ 
    internal Native.Constant* constant;

    internal Constant(Reflector reflector, Native.Constant* constant) : base(reflector)
    {
        this.constant = constant;
    }

    private Constant[] _subconstants;
    public Constant[] Subconstants 
    {
        get 
        {
            if (_subconstants == null)
            {
                ConstantID* constituentsPtr = null;
                spvc_constant_get_subconstants(constant, &constituentsPtr, out nuint count);
                var ids = new ReadOnlySpan<ConstantID>(constituentsPtr, (int)count);

                _subconstants = new Constant[(int)count];

                for (int i = 0; i < _subconstants.Length; i++)
                    _subconstants[i] = parent.GetConstantHandle(ids[i]);
            }

            return _subconstants;
        }
    }

    public Type Type
        => parent.GetTypeHandle(spvc_constant_get_type(constant));

    public float GetScalarFloat16(uint column, uint row)
        => spvc_constant_get_scalar_fp16(constant, column, row);

    public float GetScalarFloat32(uint column, uint row)
        => spvc_constant_get_scalar_fp32(constant, column, row);

    public double GetScalarFloat64(uint column, uint row)
        => spvc_constant_get_scalar_fp64(constant, column, row);

    public byte GetScalarUInt8(uint column, uint row)
        => spvc_constant_get_scalar_u8(constant, column, row);

    public sbyte GetScalarInt8(uint column, uint row)
        => spvc_constant_get_scalar_i8(constant, column, row);

    public ushort GetScalarUInt16(uint column, uint row)
        => spvc_constant_get_scalar_u16(constant, column, row);

    public short GetScalarInt16(uint column, uint row)
        => spvc_constant_get_scalar_i16(constant, column, row);
    
    public uint GetScalarUInt32(uint column, uint row)
        => spvc_constant_get_scalar_u32(constant, column, row);

    public int GetScalarInt32(uint column, uint row)
        => spvc_constant_get_scalar_i32(constant, column, row);

    public ulong GetScalarUInt64(uint column, uint row)
        => spvc_constant_get_scalar_u64(constant, column, row);

    public long GetScalarInt64(uint column, uint row)
        => spvc_constant_get_scalar_i64(constant, column, row);


    public void SetScalarFloat16(uint column, uint row, ushort value)
        => spvc_constant_set_scalar_fp16(constant, column, row, value);

    public void SetScalarFloat32(uint column, uint row, float value)
        => spvc_constant_set_scalar_fp32(constant, column, row, value);

    public void SetScalarFloat64(uint column, uint row, double value)
        => spvc_constant_set_scalar_fp64(constant, column, row, value);

    public void SetScalarUInt8(uint column, uint row, byte value)
        => spvc_constant_set_scalar_u8(constant, column, row, value);

    public void SetScalarInt8(uint column, uint row, sbyte value)
        => spvc_constant_set_scalar_i8(constant, column, row, value);
    
    public void SetScalarUInt16(uint column, uint row, ushort value)
        => spvc_constant_set_scalar_u16(constant, column, row, value);

    public void SetScalarInt16(uint column, uint row, short value)
        => spvc_constant_set_scalar_i16(constant, column, row, value);

    public void SetScalarUInt32(uint column, uint row, uint value)
        => spvc_constant_set_scalar_u32(constant, column, row, value);

    public void SetScalarInt32(uint column, uint row, int value)
        => spvc_constant_set_scalar_i32(constant, column, row, value);

    public void SetScalarUInt16(uint column, uint row, ulong value)
        => spvc_constant_set_scalar_u64(constant, column, row, value);

    public void SetScalarInt64(uint column, uint row, long value)
        => spvc_constant_set_scalar_i64(constant, column, row, value);
}