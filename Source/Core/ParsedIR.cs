using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET;

/// <summary>
/// <para>A type-safe and memory-safe wrapper around a <see cref="NativeBindings.ParsedIR"/> pointer.</para>
/// Internally, a <see cref="ParsedIR"/>'s native object is used by SPIRV-Cross to store cross-compilation and reflection information of a SPIR-V module. 
/// </summary>
public sealed unsafe class ParsedIR : ContextChild
{
    private NativeBindings.ParsedIR* _nativeIR;
    internal NativeBindings.ParsedIR* nativeIR
    {
        get 
        {
            Validate();
            return _nativeIR;
        }
    }

    internal ParsedIR(Context context, NativeBindings.ParsedIR* nativeIR) : base(context)
    {
        this._nativeIR = nativeIR;
    }
}