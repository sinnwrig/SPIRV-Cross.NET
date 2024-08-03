using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET.HLSL;

using static Native.HLSLCompiler;

public unsafe partial class HLSLCrossCompiler : Compiler
{
    public HLSLCompilerOptions hlslOptions = new();

    internal unsafe HLSLCrossCompiler(Context context, Native.Compiler* compiler) : base(context, compiler) { }

    /* Compile IR into a string. *source is owned by the context, and caller must not free it themselves. */
    public override string Compile()
    {
        hlslOptions.Apply(parent, optionsPtr);
        return base.Compile();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void HLSLSetRootConstantsLayout(RootConstants[] constantInfo)
        => parent.Throw(spvc_compiler_hlsl_set_root_constants_layout(compiler, constantInfo, (nuint)constantInfo.Length));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void HLSLAddVertexAttributeRemap(VertexAttributeRemap[] remaps)
        => parent.Throw(spvc_compiler_hlsl_add_vertex_attribute_remap(compiler, remaps, (nuint)remaps.Length));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VariableID HLSLRemapNumWorkgroupsBuiltin()
        => spvc_compiler_hlsl_remap_num_workgroups_builtin(compiler);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void HLSLSetResourceBindingFlags(BindingFlags flags)
        => parent.Throw(spvc_compiler_hlsl_set_resource_binding_flags(compiler, flags));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void HLSLAddResourceBinding(in ResourceBinding binding)
        => parent.Throw(spvc_compiler_hlsl_add_resource_binding(compiler, in binding));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HLSLIsResourceUsed(ExecutionModel model, uint set, uint binding)
        => spvc_compiler_hlsl_is_resource_used(compiler, model, set, binding);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsCompilerBufferHLSLCounterBuffer(VariableID id)
        => spvc_compiler_buffer_is_hlsl_counter_buffer(compiler, id);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool CompilerBufferGetHLSLCounterBuffer(VariableID id, out VariableID counter_id)
        => spvc_compiler_buffer_get_hlsl_counter_buffer(compiler, id, out counter_id);
}