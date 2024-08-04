using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET;

/// <summary>
/// <para>A type-safe and memory-safe wrapper around a <see cref="Native.ParsedIR"/> pointer.</para>
/// Internally, a <see cref="ParsedIR"/>'s native object is used by SPIRV-Cross to store cross-compilation and reflection information of a SPIR-V module. 
/// </summary>
public sealed unsafe class ParsedIR : ContextChild
{
    private Native.ParsedIR* _nativeIR;
    internal Native.ParsedIR* nativeIR
    {
        get 
        {
            Validate();
            return _nativeIR;
        }
    }

    internal ParsedIR(Context context, Native.ParsedIR* nativeIR) : base(context)
    {
        this._nativeIR = nativeIR;
    }
}