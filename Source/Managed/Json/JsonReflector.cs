using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET.Json;

public unsafe partial class JsonReflector : Reflector
{
    internal JsonReflector(Context context, Native.Compiler* compiler) : base(context, compiler) { }
}