using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET.Json;

/// <summary>
/// <inheritdoc/>
/// <para>Outputs reflection info in JSON notation instead of standard format.</para>
/// </summary>
public unsafe partial class JsonReflector : Reflector
{
    internal JsonReflector(Context context, Native.Compiler* compiler) : base(context, compiler) { }
}