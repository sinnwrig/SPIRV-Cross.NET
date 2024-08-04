using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET;

using static Native.Compiler;

/// <summary>
/// Wraps SPIRV-Cross reflection functionality provided by <see cref="Native.Compiler"/> into a type-safe and memory-safe object.
/// </summary>
/// <remarks>
/// An instance of this class can be obtained through <see cref="Context.CreateReflector(ParsedIR)"/>
/// </remarks>
public unsafe class Reflector : ContextChild
{
    private Native.Compiler* _compiler;
    internal Native.Compiler* compiler
    {
        get 
        {
            Validate();
            return _compiler;
        }
    }

    internal Reflector(Context context, Native.Compiler* compiler) : base(context)
    {
        this._compiler = compiler;
    }

    // CreateConstant and CreateType ensure that no duplicate managed objects are made for existing IDs.
    // SPIRV-Cross internally keeps a lookup table of constant/type object instances, 
    // but in C#-land we have to make our own lookup to ensure we aren't creating a bunch of managed objects for a single ID.
    private Dictionary<ConstantID, Constant> _constantMap = new();

    internal Constant CreateConstant(Context context, ConstantID id, Native.Constant* constant)
    {
        if (!_constantMap.TryGetValue(id, out Constant? value))
        {
            value = new Constant(context, constant);
            _constantMap.Add(id, value);
        }

        return value;
    }

    private Dictionary<TypeID, Type> _typeMap = new();

    internal Type CreateType(Context context, TypeID id, Native.Type* type)
    {
        if (!_typeMap.TryGetValue(id, out Type? value))
        {
            value = new Type(context, type);
            _typeMap.Add(id, value);
        }

        return value;
    }

    /// <summary>
    /// API for querying which specialization constants exist.
	/// To modify a specialization constant before compile(), use get_constant(constant.id),
	/// then update constants directly in the SPIRConstant data structure.
	/// For composite types, the subconstants can be iterated over and modified.
	/// constant_type is the SPIRType for the specialization constant,
	/// which can be queried to determine which fields in the unions should be poked at.
    /// </summary>
    public uint GetCurrentIDBound()
        => spvc_compiler_get_current_id_bound(compiler); 

    /// <summary>
    /// Adds an extension which is required to run this shader, e.g.
	/// require_extension("GL_KHR_my_extension");
    /// </summary>
    public void RequireExtension(string extension) 
        => context.Throw(spvc_compiler_require_extension(compiler, extension));

    /// <summary>
    /// Gets the number of extensions in the internal SPIR-V extension list 
    /// </summary>
    public nuint GetNumRequiredExtensions() 
        => spvc_compiler_get_num_required_extensions(compiler);

    /// <summary>
    /// Indexes the internal SPIR-V extension list  
    /// </summary>
    public string GetRequiredExtension(nuint index) 
        => Marshal.PtrToStringUTF8((IntPtr)spvc_compiler_get_required_extension(compiler, index)) ?? "";

    /// <summary>
    /// <para>
    /// After compilation, query if a variable ID was used as a depth resource.
    /// </para>
    /// <para>
	/// This is meaningful for MSL since descriptor types depend on this knowledge.
	/// </para>
    /// <para>Cases which return true:</para>
	/// <para>- Images which are declared with depth = 1 image type.</para>
	/// <para>- Samplers which are statically used at least once with Dref opcodes.</para>
	/// - Images which are statically used at least once with Dref opcodes.
    /// </summary>
    public bool VariableIsDepthOrCompare(VariableID id) 
        => spvc_compiler_variable_is_depth_or_compare(compiler, id);

    /// <summary>
    /// Gets the identifier (OpName) of an ID. If not defined, an empty string will be returned.
    /// </summary>
    public string GetName(ID id) 
        => Marshal.PtrToStringUTF8((IntPtr)spvc_compiler_get_name(compiler, id)) ?? "";

    /// <summary>
    /// Overrides the identifier OpName of an ID.
	/// Identifiers beginning with underscores or identifiers which contain double underscores
	/// are reserved by the implementation.
    /// </summary>
    public void SetName(ID id, string argument) 
        => spvc_compiler_set_name(compiler, id, argument);

    /*
     * Decorations.
     * Maps to C++ API.
     */

    /// <summary>
    /// Returns whether the decoration has been applied to the ID.
    /// </summary>
    public bool HasDecoration(ID id, Decoration decoration) 
        => spvc_compiler_has_decoration(compiler, id, decoration);

    /// <summary>
    /// Returns whether the decoration has been applied to a member of a struct.
    /// </summary>
    public bool HasMemberDecoration(TypeID id, uint memberIndex, Decoration decoration) 
        => spvc_compiler_has_member_decoration(compiler, id, memberIndex, decoration);

    /// <summary>
    /// Applies a decoration to an ID. Effectively injects OpDecorate.
    /// </summary>
    public void SetDecoration(ID id, Decoration decoration, uint argument) 
        => spvc_compiler_set_decoration(compiler, id, decoration, argument);

    /// <summary>
    /// Applies a decoration to an ID. Effectively injects OpDecorate.
    /// </summary>
    public void SetDecorationString(ID id, Decoration decoration, string argument) 
        => spvc_compiler_set_decoration_string(compiler, id, decoration, argument);

    /// <summary>
    /// Similar to set_decoration, but for struct members.
    /// </summary>
    public void SetMemberDecoration(TypeID id, uint memberIndex, Decoration decoration, uint argument) 
        => spvc_compiler_set_member_decoration(compiler, id, memberIndex, decoration, argument);

    /// <summary>
    /// Similar to set_decoration, but for struct members.
    /// </summary>
    public void SetMemberDecorationString(TypeID id, uint memberIndex, Decoration decoration, string argument) 
        => spvc_compiler_set_member_decoration_string(compiler, id, memberIndex, decoration, argument);

    /// <summary>
    /// Sets the member identifier for OpTypeStruct ID, member number "index".
    /// </summary>
    public void SetMemberName(TypeID id, uint memberIndex, string argument) 
        => spvc_compiler_set_member_name(compiler, id, memberIndex, argument);

    /// <summary>
    /// Removes the decoration for an ID.
    /// </summary>
    public void UnsetDecoration(ID id, Decoration decoration) 
        => spvc_compiler_unset_decoration(compiler, id, decoration);

    /// <summary>
    /// Unsets a member decoration, similar to UnsetDecoration.
    /// </summary>
    public void UnsetMemberDecoration(TypeID id, uint memberIndex, Decoration decoration)
        => spvc_compiler_unset_member_decoration(compiler, id, memberIndex, decoration);

    /// <summary>
    /// <para>Renames an entry point from old_name to new_name.</para>
	/// <para>If old_name is currently selected as the current entry point, it will continue to be the current entry point,
	/// albeit with a new name.</para>
	/// GetEntryPoints() is essentially invalidated at this point.
    /// </summary>
    public void RenameEntryPoint(string oldName, string newName, ExecutionModel model) 
        => context.Throw(spvc_compiler_rename_entry_point(compiler, oldName, newName, model));

    /// <summary>
    /// <para>Gets the value for decorations which take arguments.</para>
	/// <para>If the decoration is a boolean (i.e. spv::DecorationNonWritable),
	/// 1 will be returned.</para>
	/// If decoration doesn't exist or decoration is not recognized,
	/// 0 will be returned.
    /// </summary>
    public uint GetDecoration(ID id, Decoration decoration) 
        => spvc_compiler_get_decoration(compiler, id, decoration);

    /// <summary>
    /// <para>Gets the value for decorations which take arguments.</para>
	/// <para>If the decoration is a boolean (i.e. spv::DecorationNonWritable),
	/// 1 will be returned.</para>
	/// If decoration doesn't exist or decoration is not recognized,
	/// 0 will be returned.
    /// </summary>
    public string GetDecorationString(ID id, Decoration decoration)  
        => Marshal.PtrToStringUTF8((IntPtr)spvc_compiler_get_decoration_string(compiler, id, decoration)) ?? "";

    /// <summary>
    /// Given an OpTypeStruct in ID, obtain the OpMemberDecoration for member number "index".
    /// </summary>
    public uint GetMemberDecoration(TypeID id, uint memberIndex, Decoration decoration) 
        => spvc_compiler_get_member_decoration(compiler, id, memberIndex, decoration);

    /// <summary>
    /// Given an OpTypeStruct in ID, obtain the OpMemberDecoration for member number "index".
    /// </summary>
    public string GetMemberDecorationString(TypeID id, uint memberIndex, Decoration decoration) 
        => Marshal.PtrToStringUTF8((IntPtr)spvc_compiler_get_member_decoration_string(compiler, id, memberIndex, decoration)) ?? "";

    /// <summary>
    /// Given an OpTypeStruct in ID, obtain the identifier for member number "index".
	/// This may be an empty string.
    /// </summary>
    public string GetMemberName(TypeID id, uint memberIndex)
        => Marshal.PtrToStringUTF8((IntPtr)spvc_compiler_get_member_name(compiler, id, memberIndex)) ?? "";

    /*
     * Entry points.
     * Maps to C++ API.
     */

    /// <summary>
    /// Gets the entry points of the current SPIR-V module.
    /// </summary>
    public ReadOnlySpan<EntryPoint> GetEntryPoints() 
    {
        Native.EntryPoint* entryPointsPtr = null;
        context.Throw(spvc_compiler_get_entry_points(compiler, &entryPointsPtr, out nuint numEntryPoints));
        Span<EntryPoint> entryPointsSpan = new EntryPoint[numEntryPoints];

        for (int i = 0; i < (int)numEntryPoints; i++)
        {
            EntryPoint entryPoint;
            Native.EntryPoint nativeEntryPoint = entryPointsPtr[i];

            entryPoint.executionModel = nativeEntryPoint.executionModel;
            entryPoint.name = Marshal.PtrToStringUTF8((IntPtr)nativeEntryPoint.name) ?? "";

            entryPointsSpan[i] = entryPoint;
        }

        return entryPointsSpan;
    }
    
    /// <summary>
    /// <para>Sets the active entry point.</para>
    /// <para>All operations work on the current entry point.</para>
	/// <para>Entry points can be swapped out with set_entry_point().</para>
	/// <para>Entry points should be set right after the constructor completes as some reflection functions traverse the graph from the entry point.</para>
	/// <para>Resource reflection also depends on the entry point.</para>
	/// By default, the current entry point is set to the first OpEntryPoint which appears in the SPIR-V module.
    /// </summary>
    public void SetEntryPoint(EntryPoint entryPoint)
        => context.Throw(spvc_compiler_set_entry_point(compiler, entryPoint.name, entryPoint.executionModel));

    /// <summary>
    /// Gets a cleansed version of the entry point name.
    /// </summary>
    public string GetCleansedEntryPointName(EntryPoint entryPoint) 
        => Marshal.PtrToStringUTF8((IntPtr)spvc_compiler_get_cleansed_entry_point_name(compiler, entryPoint.name, entryPoint.executionModel)) ?? "";

    /// <summary>
    /// Set the execution mode of the active entry point.
    /// </summary>
    public void SetExecutionMode(ExecutionMode mode) 
        => spvc_compiler_set_execution_mode(compiler, mode);

    /// <summary>
    /// Unset the execution mode of the active entry point.
    /// </summary>
    public void UnsetExecutionMode(ExecutionMode mode)  
        => spvc_compiler_unset_execution_mode(compiler, mode); 

    /// <summary>
    /// Set the execution mode of the active entry point with arguments.
    /// </summary>
    public void SetExecutionModeWithArguments(ExecutionMode mode, uint arg0, uint arg1, uint arg2) 
        => spvc_compiler_set_execution_mode_with_arguments(compiler, mode, arg0, arg1, arg2);

    /// <summary>
    /// Get the execution modes of the active SPIR-V module.
    /// </summary>
    public ReadOnlySpan<ExecutionMode> GetExecutionModes()
    {
        ExecutionMode* modesPtr = null;
        context.Throw(spvc_compiler_get_execution_modes(compiler, &modesPtr, out nuint numModes));
        return new ReadOnlySpan<ExecutionMode>(modesPtr, (int)numModes);
    }

    /// <summary>
    /// <para>Gets argument for an execution mode (LocalSize, Invocations, OutputVertices).</para>
	/// <para>For LocalSize or LocalSizeId, the index argument is used to select the dimension (X = 0, Y = 1, Z = 2).</para>
	/// <para>For execution modes which do not have arguments, 0 is returned.</para>
	/// <para>LocalSizeId query returns an ID. If LocalSizeId execution mode is not used, it returns 0.</para>
	/// LocalSize always returns a literal. If execution mode is LocalSizeId,
	/// the literal (spec constant or not) is still returned.
    /// </summary>
    public uint GetExecutionModeArgument(ExecutionMode mode) 
        => spvc_compiler_get_execution_mode_argument(compiler, mode);

    /// <summary>
    /// Get the arguments of the given execution mode by index.
    /// </summary>
    public uint GetExecutionModeArgumentByIndex(ExecutionMode mode, uint index) 
        => spvc_compiler_get_execution_mode_argument_by_index(compiler, mode, index);

    /// <summary>
    /// Get the execution model for the active SPIR-V module. 
    /// </summary>
    public ExecutionModel GetExecutionModel() 
        => spvc_compiler_get_execution_model(compiler);

    /// <summary>
    /// Traverses all reachable opcodes and sets active_builtins to a bitmask of all builtin variables which are accessed in the shader.
    /// </summary>
    public void UpdateActiveBuiltins() 
        => spvc_compiler_update_active_builtins(compiler);

    /// <summary>
    /// Returns whether or not an active builtin with a given storage class is present.
    /// </summary>
    public bool HasActiveBuiltin(BuiltIn builtin, StorageClass storage) 
        => spvc_compiler_has_active_builtin(compiler, builtin, storage);

    /*
     * Type query interface.
     * Maps to C++ API, except it's read-only.
     */

    /// <summary>
    /// Gets a handle to a SpirvCrossType instance for a SpirvTypeID.
    /// </summary>
    public Type GetTypeHandle(TypeID id) 
        => CreateType(context, id, spvc_compiler_get_type_handle(compiler, id));

    /*
     * Buffer layout query.
     * Maps to C++ API.
     */

    /// <summary>
    /// Returns the effective size of a buffer block.
    /// </summary>
    public nuint GetDeclaredStructSize(Type structType) 
    {
        context.Throw(spvc_compiler_get_declared_struct_size(compiler, structType.type, out nuint size));
        return size;
    } 
    
    /// <summary>
    /// <para>Returns the effective size of a buffer block, with a given array size
	/// for a runtime array.</para>
	/// <para>SSBOs are typically declared as runtime arrays. get_declared_struct_size() will return 0 for the size.</para>
	/// <para>This is not very helpful for applications which might need to know the array stride of its last member.</para>
	/// <para>This can be done through the API, but it is not very intuitive how to accomplish this, so here we provide a helper function
	/// to query the size of the buffer, assuming that the last member has a certain size.</para>
	/// <para>If the buffer does not contain a runtime array, array_size is ignored, and the function will behave as
	/// get_declared_struct_size().</para>
	/// To get the array stride of the last member, something like:
	/// get_declared_struct_size_runtime_array(type, 1) - get_declared_struct_size_runtime_array(type, 0) will work.
    /// </summary>
    public nuint GetDeclaredStructSizeRuntimeArray(Type structType, nuint arraySize)  
    {
        context.Throw(spvc_compiler_get_declared_struct_size_runtime_array(compiler, structType.type, arraySize, out nuint size));
        return size;
    }

    /// <summary>
    /// Returns the effective size of a buffer block struct member.
    /// </summary>
    public nuint GetDeclaredStructMemberSize(Type type, uint index)
    {
        context.Throw(spvc_compiler_get_declared_struct_member_size(compiler, type.type, index, out nuint size));
        return size;
    }

    /// <summary>
    /// <para>API for querying buffer objects.</para>
	/// <para>The type passed in here should be the base type of a resource, i.e.
	/// get_type(resource.base_type_id)
	/// as decorations are set in the basic Block type.</para>
	/// <para>The type passed in here must have these decorations set, or an exception is raised.</para>
	/// Only UBOs and SSBOs or sub-structs which are part of these buffer types will have these decorations set.
    /// </summary>
    public uint StructMemberOffset(Type type, uint index)
    {
        context.Throw(spvc_compiler_type_struct_member_offset(compiler, type.type, index, out uint offset));
        return offset;
    }

    /// <summary>
    /// <para>API for querying buffer objects.</para>
	/// <para>The type passed in here should be the base type of a resource, i.e.
	/// get_type(resource.base_type_id)
	/// as decorations are set in the basic Block type.</para>
	/// <para>The type passed in here must have these decorations set, or an exception is raised.</para>
	/// Only UBOs and SSBOs or sub-structs which are part of these buffer types will have these decorations set.
    /// </summary>
    public uint StructMemberArrayStride(Type type, uint index)
    {
        context.Throw(spvc_compiler_type_struct_member_array_stride(compiler, type.type, index, out uint stride));
        return stride;
    }

    /// <summary>
    /// <para>API for querying buffer objects.</para>
	/// <para>The type passed in here should be the base type of a resource, i.e.
	/// get_type(resource.base_type_id)
	/// as decorations are set in the basic Block type.</para>
	/// <para>The type passed in here must have these decorations set, or an exception is raised.</para>
	/// Only UBOs and SSBOs or sub-structs which are part of these buffer types will have these decorations set.
    /// </summary>
    public uint StructMemberMatrixStride(Type type, uint index)
    {
        context.Throw(spvc_compiler_type_struct_member_matrix_stride(compiler, type.type, index, out uint stride));
        return stride;
    }

    /*
     * Workaround helper functions.
     * Maps to C++ API.
     */

    /*
     * Constants
     * Maps to C++ API.
     */

    /// <summary>
    /// <para>API for querying which specialization constants exist.</para>
	/// <para>To modify a specialization constant before compile(), use get_constant(constant.id),
	/// then update constants directly in the SPIRConstant data structure.</para>
	/// <para>For composite types, the subconstants can be iterated over and modified.</para>
	/// constant_type is the SPIRType for the specialization constant,
	/// which can be queried to determine which fields in the unions should be poked at.
    /// </summary>
    public ReadOnlySpan<SpecializationConstant> GetSpecializationConstants()
    {
        SpecializationConstant* constantsPtr = null;
        context.Throw(spvc_compiler_get_specialization_constants(compiler, &constantsPtr, out nuint numConstants));
        return new ReadOnlySpan<SpecializationConstant>(constantsPtr, (int)numConstants);
    }

    /// <summary>
    /// Gets a handle to a SpirvCrossConstant instance for a ConstantID.
    /// </summary>
    public Constant GetConstantHandle(ConstantID id)
        => CreateConstant(context, id, spvc_compiler_get_constant_handle(compiler, id));

    /// <summary>
    /// <para>In SPIR-V, the compute work group size can be represented by a constant vector, in which case
	/// the LocalSize execution mode is ignored.</para>
	///
	/// <para>This constant vector can be a constant vector, specialization constant vector, or partly specialized constant vector.</para>
	/// <para>To modify and query work group dimensions which are specialization constants, SPIRConstant values must be modified
	/// directly via get_constant() rather than using LocalSize directly. This function will return which constants should be modified.</para>
	///
	/// <para>To modify dimensions which are *not* specialization constants, set_execution_mode should be used directly.</para>
	/// <para>Arguments to set_execution_mode which are specialization constants are effectively ignored during compilation.</para>
	/// <para>NOTE: This is somewhat different from how SPIR-V works. In SPIR-V, the constant vector will completely replace LocalSize,
	/// while in this interface, LocalSize is only ignored for specialization constants.</para>
	///
	/// <para>The specialization constant will be written to x, y and z arguments.</para>
	/// <para>If the component is not a specialization constant, a zeroed out struct will be written.</para>
	/// <para>The return value is the constant ID of the builtin WorkGroupSize, but this is not expected to be useful
	/// for most use cases.</para>
	/// If LocalSizeId is used, there is no uvec3 value representing the workgroup size, so the return value is 0,
	/// but x, y and z are written as normal if the components are specialization constants.
    /// </summary>
    public ConstantID GetWorkGroupSizeSpecificationConstants(out SpecializationConstant x, out SpecializationConstant y, out SpecializationConstant z) 
        => spvc_compiler_get_work_group_size_specialization_constants(compiler, out x, out y, out z);

    /*
     * Buffer ranges
     * Maps to C++ API.
     */

    /// <summary>
    /// <para>Returns a vector of which members of a struct are potentially in use by a
	/// SPIR-V shader. The granularity of this analysis is per-member of a struct.</para>
	/// <para>This can be used for Buffer (UBO), BufferBlock/StorageBuffer (SSBO) and PushConstant blocks.</para>
	/// ID is the Resource::id obtained from get_shader_resources().
    /// </summary>
    public ReadOnlySpan<BufferRange> GetActiveBufferRanges(VariableID id)
    {
        BufferRange* rangesPtr = null;
        context.Throw(spvc_compiler_get_active_buffer_ranges(compiler, id, &rangesPtr, out nuint numRanges));
        return new ReadOnlySpan<BufferRange>(rangesPtr, (int)numRanges);
    }

    /*
     * Misc reflection
     * Maps to C++ API.
     */

    /// <summary>
    /// <para>Gets the offset in SPIR-V words (uint32_t) for a decoration which was originally declared in the SPIR-V binary.</para>
	/// <para>The offset will point to one or more uint32_t literals which can be modified in-place before using the SPIR-V binary.</para>
	/// <para>Note that adding or removing decorations using the reflection API will not change the behavior of this function.</para>
	/// <para>If the decoration was declared, sets the word_offset to an offset into the provided SPIR-V binary buffer and returns true,
	/// otherwise, returns false.</para>
	/// If the decoration does not have any value attached to it (e.g. DecorationRelaxedPrecision), this function will also return false.
    /// </summary>
    public bool GetBinaryOffsetForDeclaration(VariableID id, Decoration decoration, out uint wordOffset)
        => spvc_compiler_get_binary_offset_for_decoration(compiler, id, decoration, out wordOffset);

    /// <summary>
    /// Gets the list of all SPIR-V Capabilities which were declared in the SPIR-V module.
    /// </summary>
    public ReadOnlySpan<Capability> GetDeclaredCapabilities()
    {
        Capability* capabilitiesPtr = null;
        context.Throw(spvc_compiler_get_declared_capabilities(compiler, &capabilitiesPtr, out nuint numCapabilities));
        return new ReadOnlySpan<Capability>(capabilitiesPtr, (int)numCapabilities);
    }

    /// <summary>
    /// Gets the list of all SPIR-V extensions which were declared in the SPIR-V module.
    /// </summary>
    public string[] GetDeclaredExtensions() 
    {
        byte** extensionsPtr = null;
        context.Throw(spvc_compiler_get_declared_extensions(compiler, &extensionsPtr, out nuint numExtensions));
        
        string[] extensions = new string[numExtensions];

        for (int i = 0; i < (int)numExtensions; i++)
            extensions[i] = Marshal.PtrToStringUTF8((IntPtr)extensionsPtr[i]) ?? "";

        return extensions;
    }

    /// <summary>
    /// <para>When declaring buffer blocks in GLSL, the name declared in the GLSL source
	/// might not be the same as the name declared in the SPIR-V module due to naming conflicts.</para>
	/// <para>In this case, SPIRV-Cross needs to find a fallback-name, and it might only
	/// be possible to know this name after compiling to GLSL.</para>
	/// <para>This is particularly important for HLSL input and UAVs which tends to reuse the same block type
	/// for multiple distinct blocks. For these cases it is not possible to modify the name of the type itself
	/// because it might be unique. Instead, you can use this interface to check after compilation which
	/// name was actually used if your input SPIR-V tends to have this problem.</para>
	/// <para>For other names like remapped names for variables, etc, it's generally enough to query the name of the variables
	/// after compiling, block names are an exception to this rule.</para>
	/// <para>ID is the name of a variable as returned by Resource::id, and must be a variable with a Block-like type.</para>
	///
	/// This also applies to HLSL cbuffers.
    /// </summary>
    public string GetRemappedDeclaredBlockName(VariableID id) 
        => Marshal.PtrToStringUTF8((IntPtr)spvc_compiler_get_remapped_declared_block_name(compiler, id)) ?? "";

    /// <summary>
    /// <para>For buffer block variables, get the decorations for that variable.</para>
	/// <para>Sometimes, decorations for buffer blocks are found in member decorations instead
	/// of direct decorations on the variable itself.</para>
	/// The most common use here is to check if a buffer is readonly or writeonly.
    /// </summary>
    public ReadOnlySpan<Decoration> GetBufferBlockDecorations(VariableID id)
    {
        Decoration* decorationsPtr = null;
        context.Throw(spvc_compiler_get_buffer_block_decorations(compiler, id, &decorationsPtr, out nuint numDecorations));
        return new ReadOnlySpan<Decoration>(decorationsPtr, (int)numDecorations);
    }

    /// <summary>
    /// <para>Returns a set of all global variables which are statically accessed
	/// by the control flow graph from the current entry point.</para>
	/// <para>Only variables which change the interface for a shader are returned, that is,
	/// variables with storage class of Input, Output, Uniform, UniformConstant, PushConstant and AtomicCounter
	/// storage classes are returned.</para>
	///
	/// To use the returned set as the filter for which variables are used during compilation,
	/// this set can be moved to set_enabled_interface_variables().
    /// </summary>
    public Set GetActiveInterfaceVariables()
    {
        Native.Set* setPtr = null;
        context.Throw(spvc_compiler_get_active_interface_variables(compiler, &setPtr));
        return new Set(context, setPtr);
    }

    /// <summary>
    /// Query shader resources, use ids with reflection interface to modify or query binding points, etc.
    /// </summary>
    public Resources CreateShaderResources()
    {
        Native.Resources* resourcePtr = null;
        context.Throw(spvc_compiler_create_shader_resources(compiler, &resourcePtr));
        return new Resources(context, resourcePtr);
    }

    /// <summary>
    /// <para>Query shader resources, but only return the variables which are part of active_variables.</para>
	/// E.g.: get_shader_resources(get_active_variables()) to only return the variables which are statically
	/// accessed.
    /// </summary>
    public Resources CreateShaderResourcesForActiveVariables(in Set active) 
    {
        Native.Resources* resourcePtr = null;
        context.Throw(spvc_compiler_create_shader_resources_for_active_variables(compiler, &resourcePtr, active.set));
        return new Resources(context, resourcePtr);
    }
}