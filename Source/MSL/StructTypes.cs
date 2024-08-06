using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET.MSL;

#pragma warning disable 1591 // Missing XML comment for publicly visible type or member

/// <summary>
/// Description for a shader interface variable.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct ShaderInterfaceVar()
{
	/// <summary>
	/// Variable location.
	/// </summary>
	public uint location = 0;

	/// <summary>
	/// Variable format.
	/// </summary>
	public ShaderVariableFormat format = ShaderVariableFormat.Other;

	/// <summary>
	/// Variable builtin type.
	/// </summary>
	public BuiltIn builtin = BuiltIn.All;

	/// <summary>
	/// The variable's vector size if applicable.
	/// </summary>
	public uint vecsize = 0;

	/// <summary>
	/// The rate at which a variable changes in value.
	/// </summary>
	public ShaderVariableRate rate = ShaderVariableRate.PerVertex;
}

/// <summary>
/// Describes an MSL resource binding.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct ResourceBinding()
{
	public ExecutionModel stage = ExecutionModel.All;
	public uint desc_set;
	public uint binding;
	public uint count;
	public uint msl_buffer;
	public uint msl_texture;
	public uint msl_sampler;
}

/// <summary>
/// Defines an MSL constant-expression texture sampler.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct ConstexprSampler()
{
	public SamplerCoord coord = SamplerCoord.Normalized;
	public SamplerFilter min_filter = SamplerFilter.Nearest;
	public SamplerFilter mag_filter = SamplerFilter.Nearest;
	public SamplerMipFilter mip_filter = SamplerMipFilter.None;
	public SamplerAddress s_address = SamplerAddress.ClampToEdge;
	public SamplerAddress t_address = SamplerAddress.ClampToEdge;
	public SamplerAddress r_address = SamplerAddress.ClampToEdge;
	public SamplerCompareFunc compare_func = SamplerCompareFunc.Never;
	public SamplerBorderColor border_color = SamplerBorderColor.TransparentBlack;

	public float lod_clamp_min = 0.0f;
	public float lod_clamp_max = 1000.0f;
	public int max_anisotropy = 1;

	public CBool compare_enable = false;
	public CBool lod_clamp_enable = false;
	public CBool anisotropy_enable = false;
}

/// <summary>
/// Maps to the sampler Y'CbCr conversion-related portions of ConstexprSampler
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct SamplerYCBCRConversion
{
	public uint planes;
	public FormatResolution resolution;
	public SamplerFilter chroma_filter;
	public ChromaLocation x_chroma_offset;
	public ChromaLocation y_chroma_offset;
	
	private fixed int swizzle[4];

	public ComponentSwizzle GetSwizzle(int index)
	{
		return (ComponentSwizzle)swizzle[index];
	}

	public void SetSwizzle(int index, ComponentSwizzle swizzle)
	{
		this.swizzle[index] = (int)swizzle;
	}

	public SamplerYCBCRModelConversion ycbcr_model;
	public SamplerYCBCRRange ycbcr_range;
	public uint bpc;
}