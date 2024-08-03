public static class ShaderCode
{
    public const string HlslCode = """
#include "FileA.hlsl"

#pragma vertex vertex
#pragma fragment pixel

struct VertexInput
{
    float4 Position : POSITION;
    float4 Color : COLOR0;
};

struct VertexOutput
{
    float4 Position : SV_POSITION;
    float4 Color : COLOR0;
};

struct ExtraStruct
{
    float4 someBS;
    float4 moreBS;
    float4 extraBS;
};

cbuffer MyUniformBuffer
{
    float4x4 modelMatrix;
    float4x4 viewMatrix;
    float4x4 projectionMatrix;
    ExtraStruct internalStruct;
    float4 lightPosition;
    float4 lightColor;
};

float4 someGlobalValue;

VertexOutput vertex(VertexInput input)
{
    VertexOutput output;
    output.Position = input.Position;
    output.Color = input.Color;
    return output;
}

#define DO_SOMETHING(x) x * 10 + 4 - 8 + sqrt(x) / abs(x)

float4 pixel(VertexOutput input) : SV_Target
{
    float value = DO_SOMETHING(input.Color.r);

    float value2 = DO_SOMETHING(value);

    float value3 = DO_SOMETHING(value2);

    input.Color *= 10;

    input.Color /= 43.55;

    input.Color.g = value2;
    input.Color.b = value;
    input.Color.a = value3;

    input.Position = mul(modelMatrix, input.Position);
    
    input.Position = lightPosition;

    input.Color *= lightColor * someGlobalValue * internalStruct.someBS * internalStruct.moreBS * internalStruct.extraBS;

    return input.Color;
}
""";
}