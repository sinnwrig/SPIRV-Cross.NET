using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET;

/// <summary>
/// A typed wrapper around an unsigned, 32-bit integer to use as a reference and identifier for SPIRV-Cross objects.  
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct ID
{
	internal uint internalValue;

	/// <summary>
	/// Explicit conversion from uint to ID.
	/// </summary>
	public static explicit operator ID(uint value) 
		=> Unsafe.As<uint, ID>(ref value);

	/// <summary>
	/// Explicit conversion from ID to uint.
	/// </summary>
	public static explicit operator uint(ID value) 
		=> Unsafe.As<ID, uint>(ref value);
	
	/// <summary>
	/// Converts the numeric value of this instance to its equivalent string representation.
	/// </summary>
	public override readonly string ToString() 
		=> internalValue.ToString();

	/// <summary>
	/// Converts the numeric value of this instance to its equivalent string representation using the specified format.
	/// </summary>
	public readonly string ToString([StringSyntax("NumericFormat")] string? format) 
		=> internalValue.ToString(format);

	/// <summary>
	/// Converts the numeric value of this instance to its equivalent string representation using the specified culture-specific format information.
	/// </summary>
	public readonly string ToString(IFormatProvider? formatProvider) 
		=> internalValue.ToString(formatProvider);

	/// <summary>
	/// Converts the numeric value of this instance to its equivalent string representation using the specified format and culture-specific format information.
	/// </summary>
	public readonly string ToString([StringSyntax("NumericFormat")] string? format, IFormatProvider? formatProvider) 
		=> internalValue.ToString(format, formatProvider);

	/// <summary>
	/// Returns the hash code for this instance
	/// </summary>
    public override int GetHashCode() => internalValue.GetHashCode();

	/// <summary>
	/// Returns a value indicating whether this instance is equal to a specified object.
	/// </summary>
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is not ID other)
			return false;

		return Equals(other);
    }

	/// <summary>
	/// Returns a value indicating whether this instance is equal to a specified ID.
	/// </summary>
	public bool Equals(ID other) => internalValue == other.internalValue;
}

/// <summary>
/// A typed wrapper around an unsigned, 32-bit integer to use as a reference and identifier for SPIRV-Cross types.  
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct TypeID
{
	internal uint internalValue;

	/// <summary>
	/// Explicit conversion from uint to TypeID.
	/// </summary>
	public static explicit operator TypeID(uint value) 
		=> Unsafe.As<uint, TypeID>(ref value);

	/// <summary>
	/// Explicit conversion from TypeID to uint.
	/// </summary>
	public static explicit operator uint(TypeID value) 
		=> Unsafe.As<TypeID, uint>(ref value);

	/// <summary>
	/// Implicit conversion from TypeID to ID.
	/// </summary>
	public static implicit operator ID(TypeID value) 
		=> Unsafe.As<TypeID, ID>(ref value);
	
	/// <summary>
	/// Converts the numeric value of this instance to its equivalent string representation.
	/// </summary>
	public override readonly string ToString() 
		=> internalValue.ToString();

	/// <summary>
	/// Converts the numeric value of this instance to its equivalent string representation using the specified format.
	/// </summary>
	public readonly string ToString([StringSyntax("NumericFormat")] string? format) 
		=> internalValue.ToString(format);

	/// <summary>
	/// Converts the numeric value of this instance to its equivalent string representation using the specified culture-specific format information.
	/// </summary>
	public readonly string ToString(IFormatProvider? formatProvider) 
		=> internalValue.ToString(formatProvider);

	/// <summary>
	/// Converts the numeric value of this instance to its equivalent string representation using the specified format and culture-specific format information.
	/// </summary>
	public readonly string ToString([StringSyntax("NumericFormat")] string? format, IFormatProvider? formatProvider) 
		=> internalValue.ToString(format, formatProvider);

	/// <summary>
	/// Returns the hash code for this instance
	/// </summary>
    public override int GetHashCode() => internalValue.GetHashCode();

	/// <summary>
	/// Returns a value indicating whether this instance is equal to a specified object.
	/// </summary>
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is not TypeID other)
			return false;

		return Equals(other);
    }

	/// <summary>
	/// Returns a value indicating whether this instance is equal to a specified TypeID.
	/// </summary>
	public bool Equals(TypeID other) => internalValue == other.internalValue;
}

/// <summary>
/// A typed wrapper around an unsigned, 32-bit integer to use as a reference and identifier for SPIRV-Cross variables.  
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct VariableID
{
	internal uint internalValue;

	/// <summary>
	/// Explicit conversion from uint to VariableID.
	/// </summary>
	public static explicit operator VariableID(uint value) 
		=> Unsafe.As<uint, VariableID>(ref value);

	/// <summary>
	/// Explicit conversion from VariableID to uint.
	/// </summary>
	public static explicit operator uint(VariableID value) 
		=> Unsafe.As<VariableID, uint>(ref value);

	/// <summary>
	/// Implicit conversion from VariableID to ID.
	/// </summary>
	public static implicit operator ID(VariableID value) 
		=> Unsafe.As<VariableID, ID>(ref value);

    /// <summary>
	/// Converts the numeric value of this instance to its equivalent string representation.
	/// </summary>
	public override readonly string ToString() 
		=> internalValue.ToString();

	/// <summary>
	/// Converts the numeric value of this instance to its equivalent string representation using the specified format.
	/// </summary>
	public readonly string ToString([StringSyntax("NumericFormat")] string? format) 
		=> internalValue.ToString(format);

	/// <summary>
	/// Converts the numeric value of this instance to its equivalent string representation using the specified culture-specific format information.
	/// </summary>
	public readonly string ToString(IFormatProvider? formatProvider) 
		=> internalValue.ToString(formatProvider);

	/// <summary>
	/// Converts the numeric value of this instance to its equivalent string representation using the specified format and culture-specific format information.
	/// </summary>
	public readonly string ToString([StringSyntax("NumericFormat")] string? format, IFormatProvider? formatProvider) 
		=> internalValue.ToString(format, formatProvider);

	/// <summary>
	/// Returns the hash code for this instance
	/// </summary>
    public override int GetHashCode() => internalValue.GetHashCode();

	/// <summary>
	/// Returns a value indicating whether this instance is equal to a specified object.
	/// </summary>
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is not VariableID other)
			return false;

		return Equals(other);
    }

	/// <summary>
	/// Returns a value indicating whether this instance is equal to a specified VariableID.
	/// </summary>
	public bool Equals(VariableID other) => internalValue == other.internalValue;
}

/// <summary>
/// A typed wrapper around an unsigned, 32-bit integer to use as a reference and identifier for SPIRV-Cross constants.  
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct ConstantID
{
	internal uint internalValue;

	/// <summary>
	/// Explicit conversion from uint to ConstantID.
	/// </summary>
	public static explicit operator ConstantID(uint value) 
		=> Unsafe.As<uint, ConstantID>(ref value);

	/// <summary>
	/// Explicit conversion from ConstantID to uint.
	/// </summary>
	public static explicit operator uint(ConstantID value) 
		=> Unsafe.As<ConstantID, uint>(ref value);

	/// <summary>
	/// Implicit conversion from ConstantID to ID.
	/// </summary>
	public static implicit operator ID(ConstantID value) 
		=> Unsafe.As<ConstantID, ID>(ref value);

	/// <summary>
	/// Converts the numeric value of this instance to its equivalent string representation.
	/// </summary>
	public override readonly string ToString() 
		=> internalValue.ToString();

	/// <summary>
	/// Converts the numeric value of this instance to its equivalent string representation using the specified format.
	/// </summary>
	public readonly string ToString([StringSyntax("NumericFormat")] string? format) 
		=> internalValue.ToString(format);

	/// <summary>
	/// Converts the numeric value of this instance to its equivalent string representation using the specified culture-specific format information.
	/// </summary>
	public readonly string ToString(IFormatProvider? formatProvider) 
		=> internalValue.ToString(formatProvider);

	/// <summary>
	/// Converts the numeric value of this instance to its equivalent string representation using the specified format and culture-specific format information.
	/// </summary>
	public readonly string ToString([StringSyntax("NumericFormat")] string? format, IFormatProvider? formatProvider) 
		=> internalValue.ToString(format, formatProvider);

	/// <summary>
	/// Returns the hash code for this instance
	/// </summary>
    public override int GetHashCode() => internalValue.GetHashCode();

	/// <summary>
	/// Returns a value indicating whether this instance is equal to a specified object.
	/// </summary>
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is not ConstantID other)
			return false;

		return Equals(other);
    }

	/// <summary>
	/// Returns a value indicating whether this instance is equal to a specified ConstantID.
	/// </summary>
	public bool Equals(ConstantID other) => internalValue == other.internalValue;
}


/// <summary>
/// A typed wrapper to represent a C98-compatible unsigned byte-based boolean.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct CBool
{
	const byte False = 0;
	const byte True = 1;

	internal byte internalValue;

	/// <summary>
	/// Implicit conversion from bool to CBool
	/// </summary>
	public static implicit operator CBool(bool value) 
		=> Unsafe.As<bool, CBool>(ref value);

	/// <summary>
	/// Implicit conversion from CBool to bool.
	/// </summary>
	public static implicit operator bool(CBool value) 
		=> Unsafe.As<CBool, bool>(ref value);

	/// <summary>
	/// Converts the value of this instance to its equivalent string representation (either
    /// "True" or "False").
	/// </summary>
	public override readonly string ToString() 
		=> (internalValue != 0).ToString();

	/// <summary>
	/// Converts the value of this instance to its equivalent string representation (either
    /// "True" or "False").
	/// </summary>
	public readonly string ToString(IFormatProvider? formatProvider) 
		=> (internalValue != 0).ToString(formatProvider);

	/// <summary>
	/// Returns the hash code for this instance.
	/// </summary>
	public override int GetHashCode() => internalValue.GetHashCode();

	/// <inheritdoc/>
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is not CBool other)
			return false;

		return Equals(other);
    }

	/// <summary>
	/// Returns a value indicating whether this instance is equal to a specified CBool object.
	/// </summary>
	public bool Equals(CBool other) => internalValue == other.internalValue;
}

/// <summary>
/// Represents a SPIR-V resource with metadata including its identifier, type, base type, and name.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct ReflectedResource
{
	/// <summary>
	/// Resources are identified with their SPIR-V ID.
	/// This is the ID of the OpVariable.
	/// </summary>
	public ID id;
	
	/// <summary>
	/// The type ID of the variable which includes arrays and all type modifications.
	/// This type ID is not suitable for parsing OpMemberDecoration of a struct and other decorations in general
	/// since these modifications typically happen on the base_type_id.
	/// </summary>
	public TypeID type_id;

	/// <summary>
	/// <para>The base type of the declared resource.</para>
	/// This type is the base type which ignores pointers and arrays of the type_id.
	/// This is mostly useful to parse decorations of the underlying type.
	/// base_type_id can also be obtained with GetType(GetType(type_id).self).
	/// </summary>
	public TypeID base_type_id;

	/// <summary>
	/// <para>The declared name (OpName) of the resource.
	/// For Buffer blocks, the name actually reflects the externally
	/// visible Block name.</para>
	///
	/// <para>This name can be retrieved again by using either
	/// GetName(id) or GetName(base_type_id) depending if it's a buffer block or not.</para>
	///
	/// This name can be an empty string in which case GetFallbackName(id) can be
	/// used which obtains a suitable fallback identifier for an ID.
	/// </summary>
	public string name;
}


/// <summary>
/// Represents a SPIR-V builtin resource with metadata including the builtin type, value type ID, and base resource information.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct ReflectedBuiltinResource
{
	/// <summary>
	/// This is mostly here to support reflection of builtins such as Position/PointSize/CullDistance/ClipDistance.
	/// This needs to be different from Resource since we can collect builtins from blocks.
	/// <para>A builtin present here does not necessarily mean it's considered an active builtin,
	/// since variable ID "activeness" is only tracked on OpVariable level, not Block members.
	/// For that, UpdateActiveBuiltins() -> HasActiveBuiltin() can be used to further refine the reflection.</para>
	/// </summary>
	public BuiltIn builtin;
	
	/// <summary>
	/// <para>This is the actual value type of the builtin.</para>
	/// Typically float4, float, array(float, N) for the gl_PerVertex builtins.
	/// If the builtin is a control point, the control point array type will be stripped away here as appropriate.
	/// </summary>
	public TypeID value_type_id;

	/// <summary>
	/// <para>This refers to the base resource which contains the builtin.
	/// If resource is a Block, it can hold multiple builtins, or it might not be a block.</para>
	/// For advanced reflection scenarios, all information in builtin/value_type_id can be deduced,
	/// it's just more convenient this way.
	/// </summary>
	public ReflectedResource resource;
}

/// <summary>
/// Represents a SPIR-V shader entry point.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct EntryPoint
{
	/// <summary>
	/// The entrypoint's execution model.
	/// </summary>
	public ExecutionModel executionModel;

	/// <summary>
	/// The name of the entrypoint.
	/// </summary>
	public string name;
}

/// <summary>
/// Represents the associated IDs of a combined image sampler created by SPIRV-Cross for backends which do not support seperate images/samplers. 
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct CombinedImageSampler
{
	/// <summary>
	/// The ID of the sampler2D variable.
	/// </summary>
	public VariableID combined_id;

	/// <summary>
	/// The ID of the texture2D variable.
	/// </summary>
	public VariableID image_id;

	/// <summary>
	/// The ID of the sampler variable.
	/// </summary>
	public VariableID sampler_id;
}

/// <summary>
/// Represents a SPIR-V specialization constant.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SpecializationConstant
{
	/// <summary>
	/// The ID of the specialization constant.
	/// </summary>
	public ConstantID id;

	/// <summary>
	/// The constant ID of the constant, used in Vulkan during pipeline creation.
	/// </summary>
	public uint constant_id;
}

/// <summary>
/// Represents the range of shader buffer objects.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct BufferRange
{
	/// <summary>
	/// Buffer index. 
	/// </summary>
	public uint index;

	/// <summary>
	/// Buffer offset.
	/// </summary>
	public nuint offset;

	/// <summary>
	/// Buffer range.
	/// </summary>
	public nuint range;
}