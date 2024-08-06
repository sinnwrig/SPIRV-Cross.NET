using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET.GLSL;

/// <summary>
/// GLSL precision level.
/// </summary>
public enum Precision
{
	/// <summary>
	/// Medium (16-bit minimum) floating-point precision.
	/// </summary>
	Mediump = 0,

	/// <summary>
	/// High (24-bit minimum) floating-point precision.
	/// </summary>
	Highp = 1,
}