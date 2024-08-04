using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET;

using static Native.Context;

/// <summary>
/// Wraps SPIRV-Cross context functionality provided by <see cref="Native.Context"/> into a type-safe and memory-safe object.
/// <para>An instance of a <see cref="Context"/> must be kept alive in order to utilize SPIR-V functionality provided by the child objects it returns.</para>
/// </summary>
public sealed unsafe class Context : IDisposable
{
    internal Native.Context* context;

    /// <summary>
    /// Has this context instance been disposed?
    /// </summary>
    public bool IsDisposed => context == null;

    /// <summary>
    /// Initializes a new SpirvCrossContext along with its native resources.
    /// </summary>
    public Context()
    {
        context = null;

        fixed (Native.Context** contextRef = &context)
        {
            Throw(spvc_context_create(contextRef));
        }
    }

    /// <summary>
    /// Parses SPIR-V bytecode into an intermediate representation that SPIRV-Cross can consume.
    /// </summary>
    public ParsedIR ParseSpirv(byte[] spirvWords)
    {
        if (!(spirvWords.Length % sizeof(uint) == 0))
            throw new Exception("SPIR-V bytes length is not a multiple of the required unsigned int word stride.");

        GCHandle pinnedWords = GCHandle.Alloc(spirvWords, GCHandleType.Pinned);

        Native.ParsedIR* parsedIR = null;
        
        Throw(spvc_context_parse_spirv(context, (uint*)pinnedWords.AddrOfPinnedObject(), (nuint)spirvWords.Length / sizeof(uint), &parsedIR));

        pinnedWords.Free();

        return new ParsedIR(this, parsedIR);
    }

    private Native.Compiler* CreateCompiler(ParsedIR parsedIR, Native.Backend backend)
    {
        Native.Compiler* compiler = null;

        Throw(spvc_context_create_compiler(context, backend, parsedIR.nativeIR, Native.CaptureMode.Copy, &compiler));

        return compiler;
    }

    private Native.Compiler* CreateCompiler(byte[] spirvWords, Native.Backend backend)
    {
        Native.Compiler* compiler = null;
        
        ParsedIR ir = ParseSpirv(spirvWords);
        
        Throw(spvc_context_create_compiler(context, backend, ir.nativeIR, Native.CaptureMode.TakeOwnership, &compiler));

        return compiler;
    }

    internal unsafe void Throw(Native.Result result)
    {
        switch (result)
        {
            case Native.Result.InvalidArgument:  throw new InvalidArgumentException(Marshal.PtrToStringUTF8((IntPtr)spvc_context_get_last_error_string(context)) ?? "");
            case Native.Result.InvalidSPIRV:     throw new InvalidSPIRVException(Marshal.PtrToStringUTF8((IntPtr)spvc_context_get_last_error_string(context)) ?? "");
            case Native.Result.UnsupportedSPIRV: throw new UnsupportedSPIRVException(Marshal.PtrToStringUTF8((IntPtr)spvc_context_get_last_error_string(context)) ?? "");
            case Native.Result.OutOfMemory:      throw new OutOfMemoryException(); 
        };
    }

// -----------
// Explicit IR
// -----------

    /// <summary>
    /// Creates an instance of a reflection-only compiler from a parsed IR instance.
    /// </summary>
    public Reflector CreateReflector(ParsedIR parsedIR)
        => new(this, CreateCompiler(parsedIR, Native.Backend.None));
    
    /// <summary>
    /// Creates an instance of a reflection-only compiler with a JSON backend from a parsed IR instance.
    /// </summary>
    public Json.JsonReflector CreateJsonReflector(ParsedIR parsedIR)
        => new(this, CreateCompiler(parsedIR, Native.Backend.Json));

    /// <summary>
    /// Creates an instance of an SPIR-V to HLSL cross-compiler from a parsed IR instance.
    /// </summary>
    public HLSL.HLSLCrossCompiler CreateHLSLCompiler(ParsedIR parsedIR)
        => new(this, CreateCompiler(parsedIR, Native.Backend.HLSL));

    /// <summary>
    /// Creates an instance of an SPIR-V to MSL cross-compiler from a parsed IR instance.
    /// </summary>
    public MSL.MSLCrossCompiler CreateMSLCompiler(ParsedIR parsedIR)
        => new(this, CreateCompiler(parsedIR, Native.Backend.MSL));

    /// <summary>
    /// Creates an instance of an SPIR-V to GLSL cross-compiler from a parsed IR instance.
    /// </summary>
    public GLSL.GLSLCrossCompiler CreateGLSLCompiler(ParsedIR parsedIR)
        => new(this, CreateCompiler(parsedIR, Native.Backend.GLSL));

    /// <summary>
    /// Creates an instance of an SPIR-V to C++ cross-compiler from a parsed IR instance.
    /// </summary>
    public CPP.CPPCrossCompiler CreateCPPCompiler(ParsedIR parsedIR)
        => new(this, CreateCompiler(parsedIR, Native.Backend.CPP));

// -----------
// Implicit IR
// -----------

    /// <summary>
    /// Creates an instance of a reflection-only compiler from SPIR-V bytecode.
    /// </summary>
    /// <remarks>
    /// Implicitly creates parsed IR from the SPIR-V bytecode. 
    /// If you intend to make multiple compilers from equivalent bytecode, it is preferable to reuse a parsed IR instance returned from <see cref="ParseSpirv"/> 
    /// </remarks>
    public Reflector CreateReflector(byte[] spirvWords)
        => new(this, CreateCompiler(spirvWords, Native.Backend.None));

    /// <summary>
    /// Creates an instance of a reflection-only compiler with a JSON backend from SPIR-V bytecode.
    /// </summary>
    /// <remarks>
    /// Implicitly creates parsed IR from the SPIR-V bytecode. 
    /// If you intend to make multiple compilers from equivalent bytecode, it is preferable to reuse a parsed IR instance returned from <see cref="ParseSpirv"/> 
    /// </remarks>
    public Json.JsonReflector CreateJsonReflector(byte[] spirvWords)
        => new(this, CreateCompiler(spirvWords, Native.Backend.Json));

    /// <summary>
    /// Creates an instance of an SPIR-V to HLSL cross-compiler from SPIR-V bytecode.
    /// </summary>
    /// <remarks>
    /// Implicitly creates parsed IR from the SPIR-V bytecode. 
    /// If you intend to make multiple compilers from equivalent bytecode, it is preferable to reuse a parsed IR instance returned from <see cref="ParseSpirv"/> 
    /// </remarks>
    public HLSL.HLSLCrossCompiler CreateHLSLCompiler(byte[] spirvWords)
        => new(this, CreateCompiler(spirvWords, Native.Backend.HLSL));

    /// <summary>
    /// Creates an instance of an SPIR-V to MSL cross-compiler from SPIR-V bytecode.
    /// </summary>
    /// <remarks>
    /// Implicitly creates parsed IR from the SPIR-V bytecode. 
    /// If you intend to make multiple compilers from equivalent bytecode, it is preferable to reuse a parsed IR instance returned from <see cref="ParseSpirv"/> 
    /// </remarks>
    public MSL.MSLCrossCompiler CreateMSLCompiler(byte[] spirvWords)
        => new(this, CreateCompiler(spirvWords, Native.Backend.MSL));

    /// <summary>
    /// Creates an instance of an SPIR-V to GLSL cross-compiler from SPIR-V bytecode.
    /// </summary>
    /// <remarks>
    /// Implicitly creates parsed IR from the SPIR-V bytecode. 
    /// If you intend to make multiple compilers from equivalent bytecode, it is preferable to reuse a parsed IR instance returned from <see cref="ParseSpirv"/> 
    /// </remarks>
    public GLSL.GLSLCrossCompiler CreateGLSLCompiler(byte[] spirvWords)
        => new(this, CreateCompiler(spirvWords, Native.Backend.GLSL));

    /// <summary>
    /// Creates an instance of an SPIR-V to C++ cross-compiler from SPIR-V bytecode.
    /// </summary>
    /// <remarks>
    /// Implicitly creates parsed IR from the SPIR-V bytecode. 
    /// If you intend to make multiple compilers from equivalent bytecode, it is preferable to reuse a parsed IR instance returned from <see cref="ParseSpirv"/> 
    /// </remarks>
    public CPP.CPPCrossCompiler CreateCPPCompiler(byte[] spirvWords)
        => new CPP.CPPCrossCompiler(this, CreateCompiler(spirvWords, Native.Backend.CPP));

    /// <summary>
    /// Disposes of the native SPIRV-Cross context and all the resources it owns.
    /// Calling any SPIRV-Cross functionality in child objects created by this instance is illegal and will throw an exception.
    /// </summary>
    public void Dispose()
    {
        spvc_context_destroy(context);
        context = null;
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// </summary>
    ~Context()
    {
        var prevColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("SpirvCrossContext was not disposed with IDisposable.Dispose(). Ensure that Dispose() is being called when the context is no longer in use.");
        Console.ForegroundColor = prevColor;
        Dispose();
    }
}


/// <summary>
/// Represents a class that relies on a Context object for its operation and performs validation to ensure that the context is still valid.
/// <para>If the parent context is disposed, attempting to use any validated methods on classes inheriting from this will throw a <see cref="MissingContextException"/>.</para>
/// </summary>
public class ContextChild
{
    internal readonly Context context;

    internal ContextChild(Context context)
    {
        this.context = context;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void Validate()
    {
        if (context.IsDisposed)
            throw new MissingContextException(GetType());
    }
}