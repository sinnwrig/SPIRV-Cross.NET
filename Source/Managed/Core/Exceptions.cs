using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET;

/// <summary>
/// Represents an error that occurs when an internal SPIRV-Cross method returns a <see cref="Native.Result.InvalidSPIRV"/> 
/// </summary>
public class InvalidSPIRVException : Exception 
{ 
    internal InvalidSPIRVException() : base() { }
    internal InvalidSPIRVException(string message) : base(message) { }
}

/// <summary>
/// Represents an error that occurs when an internal SPIRV-Cross method returns a <see cref="Native.Result.UnsupportedSPIRV"/> 
/// </summary>
public class UnsupportedSPIRVException : Exception 
{ 
    internal UnsupportedSPIRVException() : base() { }
    internal UnsupportedSPIRVException(string message) : base(message) { }
}

/// <summary>
/// Represents an error that occurs when an internal SPIRV-Cross method returns a <see cref="Native.Result.InvalidArgument"/> 
/// </summary>
public class InvalidArgumentException : Exception 
{ 
    internal InvalidArgumentException() : base() { }
    internal InvalidArgumentException(string message) : base(message) { }
}

/// <summary>
/// Represents an error that occurs when a SPIR-V cross child type no longer has an active parent context.
/// </summary>
public class MissingContextException : Exception
{
    internal MissingContextException(System.Type type) : 
        base($"Parent context of {type.Name} is no longer alive. It is not allowed to call any furtner methods with a disposed context.") { }
}