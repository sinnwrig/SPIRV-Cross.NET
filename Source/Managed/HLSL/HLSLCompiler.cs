using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET.HLSL;

using static Native.HLSLCompiler;


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

    internal unsafe HLSLCrossCompiler(Context context, Native.Compiler* compiler) : base(context, compiler) { }

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
        Native.VertexAttributeRemap* remapsPtr = stackalloc Native.VertexAttributeRemap[remaps.Length];

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

    public VariableID RemapNumWorkgroupsBuiltin()
        => spvc_compiler_hlsl_remap_num_workgroups_builtin(compiler);

    public void SetResourceBindingFlags(BindingFlags flags)
        => context.Throw(spvc_compiler_hlsl_set_resource_binding_flags(compiler, flags));

    public void AddResourceBinding(in ResourceBinding binding)
        => context.Throw(spvc_compiler_hlsl_add_resource_binding(compiler, in binding));

    public bool IsResourceUsed(ExecutionModel model, uint set, uint binding)
        => spvc_compiler_hlsl_is_resource_used(compiler, model, set, binding);
}