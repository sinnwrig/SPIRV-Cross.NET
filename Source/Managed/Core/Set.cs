using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET;

public sealed unsafe class Set : ChildObject<Reflector>
{
    internal Native.Set* set;

    internal Set(Reflector reflector, Native.Set* set) : base(reflector)
    {
        this.set = set;
    }
}