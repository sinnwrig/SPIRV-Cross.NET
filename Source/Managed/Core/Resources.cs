using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET;

using static Native.Resources;

/// <summary>
/// <para>Wraps SPIRV-Cross reflection functionality provided by <see cref="Native.Compiler"/> into a type-safe and memory-safe object.</para>
/// A <see cref="Resources"/> instance provides access to shader resource reflection information which can be used 
/// in conjunction with a <see cref="Reflector"/> to query or modify field names, resource types, offsets, and more.  
/// </summary>
/// <remarks>
/// If the SPIR-V IR used to generate a <see cref="Resources"/> instance has been modified, the instance will not reflect the changes and a new one nust be created.
/// </remarks>
public sealed unsafe class Resources : ContextChild
{
    private Native.Resources* _resources;
    internal Native.Resources* resources
    {
        get
        {
            Validate();
            return _resources;
        }
    }

    internal Resources(Context context, Native.Resources* resources) : base(context)
    {
        this._resources = resources;
        
        _uniformBuffers = GetResourceListForType(ResourceType.UniformBuffer);
        _storageBuffers = GetResourceListForType(ResourceType.StorageBuffer);
        _stageInputs = GetResourceListForType(ResourceType.StageInput);
        _stageOutputs = GetResourceListForType(ResourceType.StageOutput);
        _subpassInputs = GetResourceListForType(ResourceType.SubpassInput);
        _storageImages = GetResourceListForType(ResourceType.StorageImage);
        _sampledImages = GetResourceListForType(ResourceType.SampledImage);
        _atomicCounters = GetResourceListForType(ResourceType.AtomicCounter);
        _pushConstantBuffers = GetResourceListForType(ResourceType.PushConstant);
        _separateImages = GetResourceListForType(ResourceType.SeparateImage);
        _separateSamplers = GetResourceListForType(ResourceType.SeparateSamplers);
        _accelerationStructures = GetResourceListForType(ResourceType.AccelerationStructure);
        _recordBuffers = GetResourceListForType(ResourceType.ShaderRecordBuffer);
    
        _builtinInputs = GetBuiltinResourceListForType(BuiltinResourceType.StageInput);
        _builtinOutputs = GetBuiltinResourceListForType(BuiltinResourceType.StageOutput);
    }


    private ReflectedResource[] _uniformBuffers;

    /// <summary>
    /// Uniform Buffers defined in SPIR-V source.
    /// </summary>
    public ReadOnlySpan<ReflectedResource> UniformBuffers => _uniformBuffers;

    private ReflectedResource[] _storageBuffers;

    /// <summary>
    /// Storage buffers defined in SPIR-V source.
    /// </summary>
    public ReadOnlySpan<ReflectedResource> StorageBuffers => _storageBuffers;

    private ReflectedResource[] _stageInputs;

    /// <summary>
    /// Stage inputs defined in SPIR-V source.
    /// </summary>
    public ReadOnlySpan<ReflectedResource> StageInputs => _stageInputs;

    private ReflectedResource[] _stageOutputs;

    /// <summary>
    /// Stage output defined in SPIR-V source.
    /// </summary>
    public ReadOnlySpan<ReflectedResource> StageOutputs => _stageOutputs;

    private ReflectedResource[] _subpassInputs;

    /// <summary>
    /// Subpass inputs defined in SPIR-V source.
    /// </summary>
    public ReadOnlySpan<ReflectedResource> SubpassInputs => _subpassInputs;

    private ReflectedResource[] _storageImages;

    /// <summary>
    /// Storage images defined in SPIR-V source.
    /// </summary>
    public ReadOnlySpan<ReflectedResource> StorageImages => _storageImages;

    private ReflectedResource[] _sampledImages;

    /// <summary>
    /// Sampled images defined in SPIR-V source.
    /// </summary>
    public ReadOnlySpan<ReflectedResource> SampledImages => _sampledImages;

    private ReflectedResource[] _atomicCounters;

    /// <summary>
    /// Atomic counters defined in SPIR-V source.
    /// </summary>
    public ReadOnlySpan<ReflectedResource> AtomicCounters => _atomicCounters;

    private ReflectedResource[] _pushConstantBuffers;

    /// <summary>
    /// Push constant buffers defined in SPIR-V source.
    /// </summary>
    /// <remarks>
    /// There can only be one push constant block,
	/// but SPIRV-Cross keeps this field as an array in case this restriction is lifted in the future.
    /// </remarks>
    public ReadOnlySpan<ReflectedResource> PushConstantBuffers => _pushConstantBuffers;

    private ReflectedResource[] _separateImages;

    /// <summary>
    /// Images defined in SPIR-V source.
    /// </summary>
    /// <remarks>
    /// For Vulkan GLSL and HLSL source,
	/// these correspond to separate texture2D and samplers respectively.
    /// </remarks>
    public ReadOnlySpan<ReflectedResource> SeparateImages => _separateImages;

    private ReflectedResource[] _separateSamplers;

    /// <summary>
    /// Image samplers defined in SPIR-V source.
    /// </summary>
    /// <remarks>
    /// For Vulkan GLSL and HLSL source,
	/// these correspond to separate texture2D and samplers respectively.
    /// </remarks>
    public ReadOnlySpan<ReflectedResource> SeparateSamplers => _separateSamplers;

    private ReflectedResource[] _accelerationStructures;

    /// <summary>
    /// Acceleration structures defined in SPIR-V source.
    /// </summary>
    public ReadOnlySpan<ReflectedResource> AccelerationStructures => _accelerationStructures;
    
    private ReflectedResource[] _recordBuffers;

    /// <summary>
    /// Shader record buffers defined in SPIR-V source.
    /// </summary>
    public ReadOnlySpan<ReflectedResource> RecordBuffers => _recordBuffers;


    private ReflectedBuiltinResource[] _builtinInputs;

    /// <summary>
    /// Built-in inputs defined in SPIR-V source.
    /// </summary>
    public ReadOnlySpan<ReflectedBuiltinResource> BuiltinInputs => _builtinInputs;
    
    private ReflectedBuiltinResource[] _builtinOutputs;

    /// <summary>
    /// Built-in outputs defined in SPIR-V source.
    /// </summary>
    public ReadOnlySpan<ReflectedBuiltinResource> BuiltinOutputs => _builtinOutputs;


    private ReflectedResource[] GetResourceListForType(ResourceType type)
    {
        Native.ReflectedResource* resourceListPtr = null;
        context.Throw(spvc_resources_get_resource_list_for_type(resources, type, &resourceListPtr, out nuint resourceSize));
        
        ReflectedResource[] resourceSpan = new ReflectedResource[(int)resourceSize];

        for (int i = 0; i < resourceSpan.Length; i++)
            resourceSpan[i] = ToManagedResource(resourceListPtr[i]);
        
        return resourceSpan;
    }

    private ReflectedBuiltinResource[] GetBuiltinResourceListForType(BuiltinResourceType type) 
    {
        Native.ReflectedBuiltinResource* resourceListPtr = null;
        context.Throw(spvc_resources_get_builtin_resource_list_for_type(resources, type, &resourceListPtr, out nuint resourceSize));
        
        ReflectedBuiltinResource[] resourceSpan = new ReflectedBuiltinResource[(int)resourceSize];

        for (int i = 0; i < resourceSpan.Length; i++)
        {
            Native.ReflectedBuiltinResource nativeResource = resourceListPtr[i];
            ReflectedBuiltinResource managedResource;

            managedResource.builtin = nativeResource.builtin;
            managedResource.value_type_id = nativeResource.value_type_id;
            managedResource.resource = ToManagedResource(nativeResource.resource);

            resourceSpan[i] = managedResource;
        }

        return resourceSpan;
    }

    private static ReflectedResource ToManagedResource(Native.ReflectedResource resourcePtr)
    {
        ReflectedResource resource;
        resource.base_type_id = resourcePtr.base_type_id;
        resource.id = resourcePtr.id;
        resource.type_id = resourcePtr.type_id;
        resource.name = Marshal.PtrToStringUTF8((IntPtr)resourcePtr.name) ?? "";

        return resource;
    }
}