using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET;

[StructLayout(LayoutKind.Sequential)]
public struct ID
{
	internal uint internalValue;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static explicit operator ID(uint value) 
		=> Unsafe.As<uint, ID>(ref value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static explicit operator uint(ID value) 
		=> Unsafe.As<ID, uint>(ref value);
	
	public override readonly string ToString() 
		=> internalValue.ToString();

	public readonly string ToString([StringSyntax("NumericFormat")] string? format) 
		=> internalValue.ToString(format);

	public readonly string ToString(IFormatProvider? formatProvider) 
		=> internalValue.ToString(formatProvider);

	public readonly string ToString([StringSyntax("NumericFormat")] string? format, IFormatProvider? formatProvider) 
		=> internalValue.ToString(format, formatProvider);
	
	public override int GetHashCode() => internalValue.GetHashCode();
	
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is not ID other)
			return false;

		return Equals(other);
    }

	public bool Equals(ID other) => internalValue == other.internalValue;
}

[StructLayout(LayoutKind.Sequential)]
public struct TypeID
{
	internal uint internalValue;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static explicit operator TypeID(uint value) 
		=> Unsafe.As<uint, TypeID>(ref value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static explicit operator uint(TypeID value) 
		=> Unsafe.As<TypeID, uint>(ref value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator ID(TypeID value) 
		=> Unsafe.As<TypeID, ID>(ref value);
	
	public override readonly string ToString() 
		=> internalValue.ToString();

	public readonly string ToString([StringSyntax("NumericFormat")] string? format) 
		=> internalValue.ToString(format);

	public readonly string ToString(IFormatProvider? formatProvider) 
		=> internalValue.ToString(formatProvider);

	public readonly string ToString([StringSyntax("NumericFormat")] string? format, IFormatProvider? formatProvider) 
		=> internalValue.ToString(format, formatProvider);
	
	public override int GetHashCode() => internalValue.GetHashCode();
	
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is not TypeID other)
			return false;

		return Equals(other);
    }

	public bool Equals(TypeID other) => internalValue == other.internalValue;
}

[StructLayout(LayoutKind.Sequential)]
public struct VariableID
{
	internal uint internalValue;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static explicit operator VariableID(uint value) 
		=> Unsafe.As<uint, VariableID>(ref value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static explicit operator uint(VariableID value) 
		=> Unsafe.As<VariableID, uint>(ref value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator ID(VariableID value) 
		=> Unsafe.As<VariableID, ID>(ref value);

    public override readonly string ToString() 
		=> internalValue.ToString();

	public readonly string ToString([StringSyntax("NumericFormat")] string? format) 
		=> internalValue.ToString(format);

	public readonly string ToString(IFormatProvider? formatProvider) 
		=> internalValue.ToString(formatProvider);

	public readonly string ToString([StringSyntax("NumericFormat")] string? format, IFormatProvider? formatProvider) 
		=> internalValue.ToString(format, formatProvider);
	
	public override int GetHashCode() => internalValue.GetHashCode();
	
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is not VariableID other)
			return false;

		return Equals(other);
    }

	public bool Equals(VariableID other) => internalValue == other.internalValue;
}

[StructLayout(LayoutKind.Sequential)]
public struct ConstantID
{
	internal uint internalValue;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static explicit operator ConstantID(uint value) 
		=> Unsafe.As<uint, ConstantID>(ref value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static explicit operator uint(ConstantID value) 
		=> Unsafe.As<ConstantID, uint>(ref value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator ID(ConstantID value) 
		=> Unsafe.As<ConstantID, ID>(ref value);

	public override readonly string ToString() 
		=> internalValue.ToString();

	public readonly string ToString([StringSyntax("NumericFormat")] string? format) 
		=> internalValue.ToString(format);

	public readonly string ToString(IFormatProvider? formatProvider) 
		=> internalValue.ToString(formatProvider);

	public readonly string ToString([StringSyntax("NumericFormat")] string? format, IFormatProvider? formatProvider) 
		=> internalValue.ToString(format, formatProvider);

    public override int GetHashCode() => internalValue.GetHashCode();

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is not ConstantID other)
			return false;

		return Equals(other);
    }

	public bool Equals(ConstantID other) => internalValue == other.internalValue;
}

[StructLayout(LayoutKind.Sequential)]
public struct CBool
{
	const byte False = 0;
	const byte True = 1;

	internal byte internalValue;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator CBool(bool value) 
		=> Unsafe.As<bool, CBool>(ref value);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static implicit operator bool(CBool value) 
		=> Unsafe.As<CBool, bool>(ref value);

	public override readonly string ToString() 
		=> (internalValue != 0).ToString();

	public readonly string ToString(IFormatProvider? formatProvider) 
		=> (internalValue != 0).ToString(formatProvider);

	public override int GetHashCode() => internalValue.GetHashCode();
	
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is not CBool other)
			return false;

		return Equals(other);
    }

	public bool Equals(CBool other) => internalValue == other.internalValue;
}

/* See C++ API. */
[StructLayout(LayoutKind.Sequential)]
public struct ReflectedResource
{
	public VariableID id;
	public TypeID base_type_id;
	public TypeID type_id;

	public string name;
}

[StructLayout(LayoutKind.Sequential)]
public struct ReflectedBuiltinResource
{
	public BuiltIn builtin;
	public TypeID value_type_id;
	public ReflectedResource resource;
}

/* See C++ API. */
[StructLayout(LayoutKind.Sequential)]
public struct EntryPoint
{
	public ExecutionModel executionModel;

	public string name;
}

/* See C++ API. */
[StructLayout(LayoutKind.Sequential)]
public struct CombinedImageSampler
{
	public VariableID combined_id;
	public VariableID image_id;
	public VariableID sampler_id;
}

/* See C++ API. */
[StructLayout(LayoutKind.Sequential)]
public struct SpecializationConstant
{
	public ConstantID id;
	public uint constant_id;
}

/* See C++ API. */
[StructLayout(LayoutKind.Sequential)]
public struct BufferRange
{
	public uint index;
	public nuint offset;
	public nuint range;
}