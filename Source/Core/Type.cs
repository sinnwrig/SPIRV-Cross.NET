using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET;

using static NativeBindings.Type;

/// <summary>
/// <para>Wraps SPIRV-Cross type reflection functionality provided by <see cref="NativeBindings.Compiler"/> into a type-safe and memory-safe object.</para>
/// A <see cref="Resources"/> instance provides access to shader type reflection information which can be used 
/// in conjunction with a <see cref="Reflector"/> to query or modify names, offsets, vector sizes, layouts, image information, and more. 
/// </summary>
public sealed unsafe class Type : ContextChild
{
    private NativeBindings.Type* _type;
    internal NativeBindings.Type* type
    {
        get
        {
            Validate();
            return _type;
        }
    }

    internal Type(Context context, NativeBindings.Type* type) : base(context)
    {   
        this._type = type;
    }

    /// <summary>
    /// This ID can be used to get the underlying type of an object without decorations
    /// </summary>    
    public TypeID BaseTypeID
        => spvc_type_get_base_type_id(type);

    /// <summary>
    /// Gets the underlying primitive value type of this type. 
    /// </summary>
    public BaseType BaseType
        => spvc_type_get_basetype(type);

    /// <summary>
    /// If this <see cref="Type"/> is a struct, gets the number of member types it contains. 
    /// </summary>
    public uint MemberCount
        => spvc_type_get_num_member_types(type);
    
    /// <summary>
    /// If this <see cref="Type"/> is a vector or matrix, returns the width in bits of this type.
    /// </summary>
    public uint BitWidth
        => spvc_type_get_bit_width(type);
    
    /// <summary>
    /// If this <see cref="Type"/> is a vector or matrix, returns the vector size of this type.
    /// </summary>
    public uint VectorSize
        => spvc_type_get_vector_size(type);
    
    /// <summary>
    /// If this <see cref="Type"/> is a matrix, returns the columns it contains.
    /// </summary>
    public uint Columns
        => spvc_type_get_columns(type);

    /// <summary>
    /// If this <see cref="Type"/> is an array, returns the number of dimensions that were defined.
    /// </summary>
    public uint ArrayDimensions
        => spvc_type_get_num_array_dimensions(type); 

    /// <summary>
    /// Returns storage class information about this type.
    /// </summary>
    public StorageClass StorageClass
        => spvc_type_get_storage_class(type);

     /// <summary>
    /// If this <see cref="Type"/> is an image, provides type information about the image samples. 
    /// </summary>
    public TypeID ImageSampledType
        => spvc_type_get_image_sampled_type(type);

     /// <summary>
    /// If this <see cref="Type"/> is an image, provides the number of dimensions the image has. 
    /// </summary>
    public Dimension ImageDimension
        => spvc_type_get_image_dimension(type);

    /// <summary>
    /// If this <see cref="Type"/> is an image, determines if it is a depth image. 
    /// </summary>
    public bool ImageIsDepth
        => spvc_type_get_image_is_depth(type);

    /// <summary>
    /// If this <see cref="Type"/> is an image, determines if the image is a hardware image array. 
    /// </summary>
    public bool ImageArrayed
        => spvc_type_get_image_arrayed(type);

    /// <summary>
    /// If this <see cref="Type"/> is an image, determines if it is a multisampled image. 
    /// </summary>
    public bool ImageMultisampled
        => spvc_type_get_image_multisampled(type);

    /// <summary>
    /// If this <see cref="Type"/> is an image, determines if it is a storage image. 
    /// </summary>
    public bool ImageIsStorage
        => spvc_type_get_image_is_storage(type);

    /// <summary>
    /// If this <see cref="Type"/> is a storage image, provides the storage format of the image. 
    /// </summary>
    public ImageFormat ImageStorageFormat
        => spvc_type_get_image_storage_format(type);

    /// <summary>
    /// If this <see cref="Type"/> is an image, provides the memory access qualifier of the image. 
    /// </summary>
    public AccessQualifier ImageAccessQualifier
        => spvc_type_get_image_access_qualifier(type);

    /// <summary>
    /// Array elements can be either specialization constants or specialization ops.
	/// This method can be used to determine if the dimension of an array is literal.
	/// If this method returns true, the element is a literal,
	/// otherwise, it's an expression, which must be resolved on demand.
	/// The actual size is not really known until runtime.
    /// </summary>
    public bool ArrayDimensionIsLiteral(uint dimension)
        => spvc_type_array_dimension_is_literal(type, dimension);

    /// <summary>
    /// If this <see cref="Type"/> is an array with multiple dimensions, gets the array information of a given dimension. This can be used with <see cref="ArrayDimensions"/> to iterate over the sub-arrays of this type. 
    /// </summary>
    public ID GetArrayDimension(uint dimension)
        => spvc_type_get_array_dimension(type, dimension);

    /// <summary>
    /// If this <see cref="Type"/> is a struct, gets the member type at a given index. This can be used with <see cref="MemberCount"/> to iterate over the fields of this type.  
    /// </summary>
    public TypeID GetMemberType(uint index)
        => spvc_type_get_member_type(type, index);
}