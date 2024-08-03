using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET;

public sealed unsafe class ParsedIR : ChildObject<Context>
{
    internal Native.ParsedIR* nativeIR;

    internal ParsedIR(Context context, Native.ParsedIR* nativeIR) : base(context)
    {
        this.nativeIR = nativeIR;
    }
}