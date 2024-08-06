using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET.CPP;

/// <summary>
/// <inheritdoc/>
/// <para>Outputs cross-compiled C++ when calling Compile().</para>
/// </summary>
public unsafe partial class CPPCrossCompiler : Compiler
{
    internal CPPCrossCompiler(Context context, NativeBindings.Compiler* compiler) : base(context, compiler) { }
}