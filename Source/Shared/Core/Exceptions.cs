using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SPIRVCross.NET;

public class InvalidSPIRVException : Exception 
{ 
    public InvalidSPIRVException() : base() { }
    public InvalidSPIRVException(string message) : base(message) { }
    public InvalidSPIRVException(string message, Exception? innerException) : base(message, innerException) { }
}

public class UnsupportedSPIRVException : Exception 
{ 
    public UnsupportedSPIRVException() : base() { }
    public UnsupportedSPIRVException(string message) : base(message) { }
    public UnsupportedSPIRVException(string message, Exception? innerException) : base(message, innerException) { }
}

public class InvalidArgumentException : Exception 
{ 
    public InvalidArgumentException() : base() { }
    public InvalidArgumentException(string message) : base(message) { }
    public InvalidArgumentException(string message, Exception? innerException) : base(message, innerException) { }
}