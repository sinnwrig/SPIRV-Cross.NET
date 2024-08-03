using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET;

using static Native.Resources;

public sealed unsafe class Resources : ChildObject<Reflector>
{
    internal Native.Resources* resources;

    internal Resources(Reflector reflector, Native.Resources* resources) : base(reflector)
    {
        this.resources = resources;
    }

    public ReadOnlySpan<ReflectedResource> UniformBuffers => 
        GetResourceListForType(ResourceType.UniformBuffer);
    
    public ReadOnlySpan<ReflectedResource> StorageBuffers => 
        GetResourceListForType(ResourceType.StorageBuffer);

    public ReadOnlySpan<ReflectedResource> StageInputs => 
        GetResourceListForType(ResourceType.StageInput);

    public ReadOnlySpan<ReflectedResource> StageOutputs => 
        GetResourceListForType(ResourceType.StageOutput);

    public ReadOnlySpan<ReflectedResource> SubpassInputs => 
        GetResourceListForType(ResourceType.SubpassInput);

    public ReadOnlySpan<ReflectedResource> StorageImages => 
        GetResourceListForType(ResourceType.StorageImage);

    public ReadOnlySpan<ReflectedResource> SampledImages => 
        GetResourceListForType(ResourceType.SampledImage);
    
    public ReadOnlySpan<ReflectedResource> AtomicCounters => 
        GetResourceListForType(ResourceType.AtomicCounter);

    public ReadOnlySpan<ReflectedResource> PushConstantBuffers => 
        GetResourceListForType(ResourceType.PushConstant);

    public ReadOnlySpan<ReflectedResource> SeparateImages => 
        GetResourceListForType(ResourceType.SeparateImage);

    public ReadOnlySpan<ReflectedResource> SeparateSamplers => 
        GetResourceListForType(ResourceType.SeparateSamplers);

    public ReadOnlySpan<ReflectedResource> AccelerationStructures => 
        GetResourceListForType(ResourceType.AccelerationStructure);
	
    public ReadOnlySpan<ReflectedResource> RecordBuffers => 
        GetResourceListForType(ResourceType.ShaderRecordBuffer);


    public ReadOnlySpan<ReflectedBuiltinResource> BuiltinInputs =>
        GetBuiltinResourceListForType(BuiltinResourceType.StageInput);
    
    public ReadOnlySpan<ReflectedBuiltinResource> BuiltinOutputs =>
        GetBuiltinResourceListForType(BuiltinResourceType.StageOutput);


    private ReadOnlySpan<ReflectedResource> GetResourceListForType(ResourceType type)
    {
        Native.ReflectedResource* resourceListPtr = null;
        parent.parent.Throw(spvc_resources_get_resource_list_for_type(resources, type, &resourceListPtr, out nuint resourceSize));
        
        Span<ReflectedResource> resourceSpan = new ReflectedResource[(int)resourceSize];

        for (int i = 0; i < resourceSpan.Length; i++)
            resourceSpan[i] = ToManagedResource(resourceListPtr[i]);
        
        return resourceSpan;
    }

    private ReadOnlySpan<ReflectedBuiltinResource> GetBuiltinResourceListForType(BuiltinResourceType type) 
    {
        Native.ReflectedBuiltinResource* resourceListPtr = null;
        parent.parent.Throw(spvc_resources_get_builtin_resource_list_for_type(resources, type, &resourceListPtr, out nuint resourceSize));
        
        Span<ReflectedBuiltinResource> resourceSpan = new ReflectedBuiltinResource[(int)resourceSize];

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