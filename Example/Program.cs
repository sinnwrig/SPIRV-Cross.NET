using System;
using System.Diagnostics;
using DirectXShaderCompiler.NET;
using SPIRVCross.NET;
using SPIRVCross.NET.GLSL;
using SPIRVCross.NET.HLSL;
using SPIRVCross.NET.MSL;

namespace Application;

public class Program
{       
    public static void Main()
    {
        CompilerOptions options = new CompilerOptions(ShaderType.Fragment.ToProfile(6, 0))
        {
            entryPoint = "pixel",
            generateAsSpirV = true,
        };

        // Console.WriteLine($"Compiling shader: \n\n\"\n{ShaderCode.HlslCode}\"\n");
        CompilationResult result = ShaderCompiler.Compile(ShaderCode.HlslCode, options, (x) => "");

        if (result.compilationErrors != null)
        {
            Console.WriteLine("Errors compiling shader:");
            Console.WriteLine(result.compilationErrors);
            return;
        } 
        
        using SpirvCrossContext context = new SpirvCrossContext();

        SpirvCrossParsedIR parsedIR = context.ParseSpirv(result.objectBytes);

        GLSLCrossCompiler glslCompiler = context.CreateGLSLCompiler(parsedIR);
        DoGLSLStuff(glslCompiler);

        HLSLCrossCompiler hlslCompiler = context.CreateHLSLCompiler(parsedIR);
        DoHLSLStuff(hlslCompiler);
        
        MSLCrossCompiler mslCompiler = context.CreateMSLCompiler(parsedIR);
        DoMSLStuff(mslCompiler);
    }


    static void DoGLSLStuff(GLSLCrossCompiler crossCompiler)
    {

    }


    static void DoHLSLStuff(HLSLCrossCompiler crossCompiler)
    {
        // Do some basic reflection.
        SpirvCrossResources resources = crossCompiler.CreateShaderResources();

        Console.WriteLine("\nGetting reflected data\n");

        foreach (var resource in resources.GetResourceListForType(ResourceType.UniformBuffer))
        {
            Console.WriteLine($"Uniform Buffer {resource.name}");

            SpirvCrossType type = crossCompiler.GetTypeHandle(resource.base_type_id);
            SpirvTypeID typeID = type.GetBaseTypeID();

            for (uint i = 0; i < type.GetNumMemberTypes(); i++)
            {
                SpirvTypeID typeID2 = type.GetMemberType(i);
                SpirvCrossType type2 = crossCompiler.GetTypeHandle(typeID2);

                Console.WriteLine($"\t{type2.GetBaseType()} {crossCompiler.GetMemberName(typeID, i)}. (o{crossCompiler.StructMemberOffset(type, i)}, s{crossCompiler.GetDeclaredStructMemberSize(type, i)})");

                for (uint j = 0; j < type2.GetNumMemberTypes(); j++)
                {
                    SpirvTypeID typeID3 = type2.GetMemberType(j);
                    SpirvCrossType type3 = crossCompiler.GetTypeHandle(typeID3);

                    Console.WriteLine($"\t\t{type3.GetBaseType()} {crossCompiler.GetMemberName(typeID2, j)}. (o{crossCompiler.StructMemberOffset(type2, j)}, s{crossCompiler.GetDeclaredStructMemberSize(type2, j)})");
                }
            }
        }

        string result = crossCompiler.Compile();

        // Console.WriteLine($"Cross-compiled source: \n\n\"\n{result}\n\"");
    }


    static void DoMSLStuff(MSLCrossCompiler crossCompiler)
    {
        
    }
}