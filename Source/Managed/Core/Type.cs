using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET;

using static Native.Type;

public sealed unsafe class Type : ContextChild
{
    private Native.Type* _type;
    internal Native.Type* type
    {
        get
        {
            Validate();
            return _type;
        }
    }

    internal Type(Context context, Native.Type* type) : base(context)
    {   
        this._type = type;
    }

    /// <summary>
    /// Maps to SPIRType::self. This ID can be used to get the underlying type of an object without decorations
    /// </summary>    
    public TypeID BaseType
        => spvc_type_get_base_type_id(type);

    public BaseValueType BaseValueType
        => spvc_type_get_basetype(type);

    public uint MemberCount
        => spvc_type_get_num_member_types(type);
    
    public uint BitWidth
        => spvc_type_get_bit_width(type);
    
    public uint VectorSize
        => spvc_type_get_vector_size(type);

    public uint Columns
        => spvc_type_get_columns(type);
    public uint ArrayDimensions
        => spvc_type_get_num_array_dimensions(type); 

    public StorageClass StorageClass
        => spvc_type_get_storage_class(type);

    public TypeID ImageSampledType
        => spvc_type_get_image_sampled_type(type);

    public Dimension ImageDimension
        => spvc_type_get_image_dimension(type);

    public bool ImageIsDepth
        => spvc_type_get_image_is_depth(type);

    public bool ImageArrayed
        => spvc_type_get_image_arrayed(type);

    public bool ImageMultisampled
        => spvc_type_get_image_multisampled(type);

    public bool ImageIsStorage
        => spvc_type_get_image_is_storage(type);

    public ImageFormat ImageStorageFormat
        => spvc_type_get_image_storage_format(type);

    public AccessQualifier ImageAccessQualifier
        => spvc_type_get_image_access_qualifier(type);

    public bool ArrayDimensionIsLiteral(uint dimension)
        => spvc_type_array_dimension_is_literal(type, dimension);

    public ID GetArrayDimension(uint dimension)
        => spvc_type_get_array_dimension(type, dimension);

    public TypeID GetMemberType(uint index)
        => spvc_type_get_member_type(type, index);
}