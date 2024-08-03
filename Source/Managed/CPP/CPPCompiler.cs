using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET.CPP;

public unsafe partial class CPPCrossCompiler : Compiler
{
    internal CPPCrossCompiler(Context context, Native.Compiler* compiler) : base(context, compiler) { }
}