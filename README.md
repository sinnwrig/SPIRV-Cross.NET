# SPIRV-Cross.NET: cross-platform C# wrapper for SPIRV-Cross 

A cross-platform .NET 8.0 wrapper for Microsoft's DirectX Shader Compiler, written in C#. 

[![NuGet](https://img.shields.io/nuget/v/SPIRV-Cross.NET.svg)](https://www.nuget.org/packages/SPIRV-Cross.NET)

# Usage

For a brief showcase of how SPIRV-Cross.NET works, check the _Example_ folder. At the moment, the object structure maps relatively close to SPIRV-Cross's C++ API, and the library's [documentation](https://github.com/KhronosGroup/SPIRV-Cross/wiki/Reflection-API-user-guide) can generally be used in most cases. However, if there is not a direct mapping for a given field, property, or function, please consult the source files in _Source/Managed/.._ for more info. 
 
# Native Details
 
To facilitate cross-platform releases, the native SPIRV-Cross library is [built using zig](https://github.com/sinnwrig/SPIRV-Cross-zig) instead of CMake/GN. As Zig's compiler supports cross-compilation out of the box, it allows SPIRV-Cross to build easily from most desktop platforms, for most platforms. The libraries produced by building this repository are what SPIRV-Cross.NET uses in its releases.
 
## Building Native Libraries
 
To build native libraries, run the `BuildNative.cs` file inside the _Native_ folder, specicying your target architecture [x64, arm64, all] with -A and your target platform [windows, linux, macos, all] with -P.
 
Native build requirements:
- Zig compiler version of at least 0.14.0
 
Pre-built binaries are bundled in the NuGet package for the following operating systems:
- Windows x64
- Windows arm64
- OSX x64
- OSX arm64
- Linux x64
- Linux arm64
