using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET.GLSL;

using static Native.Compiler;

public unsafe partial class GLSLCrossCompiler : Compiler
{
    public GLSLCompilerOptions glslOptions = new();

    internal unsafe GLSLCrossCompiler(Context context, Native.Compiler* compiler) : base(context, compiler) { }

    /// <summary>
    /// <para>Legacy GLSL compatibility method.</para>
	/// <para>Takes a uniform or push constant variable and flattens it into a (i|u)vec4 array[N]; array instead.</para>
    /// <para>For this to work, all types in the block must be the same basic type, e.g. mixing vec2 and vec4 is fine, but
	/// mixing int and float is not.</para>
    /// The name of the uniform array will be the same as the interface block name.
    /// </summary>
    public void FlattenBufferBlock(VariableID id)
    {
        Validate(MissingContext);
        parent.Throw(spvc_compiler_flatten_buffer_block(compiler, id));
    }

    /// <inheritdoc/>
    public override string Compile()
    {
        glslOptions.Apply(parent, optionsPtr);
        return base.Compile();
    }
}