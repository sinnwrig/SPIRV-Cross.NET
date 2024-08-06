using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET.HLSL;

using static NativeBindings.HLSLCompiler;


/// <summary>
/// <inheritdoc/>
/// <para>Outputs cross-compiled HLSL when calling <see cref="Compile()"/>.</para>
/// </summary>
public unsafe partial class HLSLCrossCompiler : Compiler
{
    /// <summary>
    /// HLSL-specific options to compile with.
    /// </summary>
    public HLSLCompilerOptions hlslOptions = new();

    internal unsafe HLSLCrossCompiler(Context context, NativeBindings.Compiler* compiler) : base(context, compiler) { }

    /// <inheritdoc/>
    public override string Compile()
    {
        hlslOptions.Apply(context, optionsPtr);
        return base.Compile();
    }

    /// <summary>
    /// Sets the custom root constant layout, which should be emitted
	/// when translating push constant ranges.
    /// </summary>
    public void SetRootConstantsLayout(RootConstants[] constantInfo)
    {
        Span<RootConstants> constants = constantInfo;
        
        fixed (RootConstants* constantsPtr = &constants.GetPinnableReference())
            context.Throw(spvc_compiler_hlsl_set_root_constants_layout(compiler, constantsPtr, (nuint)constantInfo.Length));
    }

    /// <summary>
    /// Adds an array of remaps to convert a vertex layout location into an HLSL semantic.  
    /// </summary>
    public void AddVertexAttributeRemap(VertexAttributeRemap[] remaps)
    {
        NativeBindings.VertexAttributeRemap* remapsPtr = stackalloc NativeBindings.VertexAttributeRemap[remaps.Length];

        for (int i = 0; i < remaps.Length; i++)
        {
            remapsPtr[i].location = remaps[i].location;

            int len = System.Text.Encoding.UTF8.GetByteCount(remaps[i].semantic);

            remapsPtr[i].semantic = (byte*)Marshal.AllocHGlobal(len);
            Span<byte> nativeSemantic = new Span<byte>(remapsPtr[i].semantic, len);

            System.Text.Encoding.UTF8.GetBytes(remaps[i].semantic, nativeSemantic);
        }

        context.Throw(spvc_compiler_hlsl_add_vertex_attribute_remap(compiler, remapsPtr, (nuint)remaps.Length));

        for (int i = 0; i < remaps.Length; i++)
            Marshal.FreeHGlobal((IntPtr)remapsPtr[i].semantic);
    }

    /// <summary>
    /// <para>This is a special HLSL workaround for the NumWorkGroups builtin.</para>
	/// <para>This does not exist in HLSL, so the calling application must create a dummy cbuffer in
	/// which the application will store this builtin.</para>
	/// <para>The cbuffer layout will be:</para>
	///     <para>cbuffer SPIRV_Cross_NumWorkgroups : register(b#, space#) { uint3 SPIRV_Cross_NumWorkgroups_count; };</para>
	/// <para>This must be called before compile().</para>
	/// The function returns 0 if NumWorkGroups builtin is not statically used in the shader from the current entry point.
	/// If non-zero, this returns the variable ID of a cbuffer which corresponds to
	/// the cbuffer declared above. 
    /// <para>By default, no binding or descriptor set decoration is set,
	/// so the calling application should declare explicit bindings on this ID before calling compile().</para>
    /// </summary>
    public VariableID RemapNumWorkgroupsBuiltin()
        => spvc_compiler_hlsl_remap_num_workgroups_builtin(compiler);

    /// <summary>
    /// Controls how resource bindings are declared in the output HLSL.
    /// </summary>
    public void SetResourceBindingFlags(BindingFlags flags)
        => context.Throw(spvc_compiler_hlsl_set_resource_binding_flags(compiler, flags));

    /// <summary>
    /// resource is a resource binding to indicate the HLSL CBV, SRV, UAV or sampler binding to use for a particular SPIR-V description set and binding. 
    /// If resource bindings are provided, IsHlslResourceBindingUsed() will return true after calling compile() if
	/// the set/binding combination was used by the HLSL code.
    /// </summary>
    public void AddResourceBinding(in ResourceBinding binding)
        => context.Throw(spvc_compiler_hlsl_add_resource_binding(compiler, in binding));

    /// <summary>
    /// </summary>
    public bool IsResourceUsed(ExecutionModel model, uint set, uint binding)
        => spvc_compiler_hlsl_is_resource_used(compiler, model, set, binding);
}