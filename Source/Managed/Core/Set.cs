using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET;

/// <summary>
/// <para>A type-safe and memory-safe wrapper around a <see cref="Native.Set"/> pointer.</para>
/// Internally, a <see cref="Set"/>'s native object is used by SPIRV-Cross to store a set of global variables in the SPIR-V module. 
/// </summary>
public sealed unsafe class Set : ContextChild
{
    private Native.Set* _set;
    internal Native.Set* set
    {
        get 
        {
            Validate();
            return _set;
        }
    }


    internal Set(Context context, Native.Set* set) : base(context)
    {
        this._set = set;
    }
}