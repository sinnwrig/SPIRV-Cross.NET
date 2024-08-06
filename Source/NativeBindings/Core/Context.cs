using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET.NativeBindings;

#pragma warning disable 1591

public partial struct Context 
{ 
    const string LibName = NativeLibrary.LibraryName;

    /*
     * Context is the highest-level API construct.
     * The context owns all memory allocations made by its child object hierarchy, including various non-opaque structs and strings.
     * This means that the API user only has to care about one "destroy" call ever when using the C API.
     * All pointers handed out by the APIs are only valid as long as the context
     * is alive and spvc_context_release_allocations has not been called.
     */
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_context_create(Context** context);

    /* Frees all memory allocations and objects associated with the context and its child objects. */
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial void spvc_context_destroy(Context* context);

    /* Frees all memory allocations and objects associated with the context and its child objects, but keeps the context alive. */
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial void spvc_context_release_allocations(Context* context);

    /* Get the string for the last error which was logged. */
    [LibraryImport(LibName, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial byte* spvc_context_get_last_error_string(Context* context);

    /*
    // Get notified in a callback when an error triggers. Useful for debugging.
    public unsafe delegate void ErrorCallback(void* userdata, byte* error);

    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial void spvc_context_set_error_callback(SpirvCrossContext* context, ErrorCallback cb, void* userdata);
    */

    /* SPIR-V parsing interface. Maps to Parser which then creates a ParsedIR, and that IR is extracted into the handle. */
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_context_parse_spirv(Context* context, uint* spirv, nuint word_count, ParsedIR** parsed_ir);

    /*
     * Create a compiler backend. Capture mode controls if we construct by copy or move semantics.
     * It is always recommended to use SPVC_CAPTURE_MODE_TAKE_OWNERSHIP if you only intend to cross-compile the IR once.
     */
    [LibraryImport(LibName)]
    [UnmanagedCallConv(CallConvs = [ typeof(CallConvCdecl) ] )]
    public static unsafe partial Result spvc_context_create_compiler(Context* context, Backend backend, ParsedIR* parsed_ir, CaptureMode mode, Compiler** compiler);
}