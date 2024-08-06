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
        DirectXShaderCompiler.NET.CompilerOptions options = new DirectXShaderCompiler.NET.CompilerOptions(ShaderType.Fragment.ToProfile(6, 0))
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
        
        using Context context = new Context();

        ParsedIR parsedIR = context.ParseSpirv(result.objectBytes);

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
        Resources resources = crossCompiler.CreateShaderResources();

        Console.WriteLine("\nGetting reflected data\n");

        foreach (var resource in resources.UniformBuffers)
        {
            Console.WriteLine($"Uniform Buffer {resource.name}");

            SPIRVCross.NET.Type type = crossCompiler.GetTypeHandle(resource.base_type_id);
            TypeID typeID = type.BaseTypeID;

            for (uint i = 0; i < type.MemberCount; i++)
            {
                TypeID typeID2 = type.GetMemberType(i);
                SPIRVCross.NET.Type type2 = crossCompiler.GetTypeHandle(typeID2);

                Console.WriteLine($"\t{type2.BaseType} {crossCompiler.GetMemberName(typeID, i)}. (o{crossCompiler.StructMemberOffset(type, i)}, s{crossCompiler.GetDeclaredStructMemberSize(type, i)})");

                for (uint j = 0; j < type2.MemberCount; j++)
                {
                    TypeID typeID3 = type2.GetMemberType(j);
                    SPIRVCross.NET.Type type3 = crossCompiler.GetTypeHandle(typeID3);

                    Console.WriteLine($"\t\t{type3.BaseType} {crossCompiler.GetMemberName(typeID2, j)}. (o{crossCompiler.StructMemberOffset(type2, j)}, s{crossCompiler.GetDeclaredStructMemberSize(type2, j)})");
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