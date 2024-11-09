Shader"Shader Graphs/Value Bar Shader"
{
    Properties
    {
        _Hue("Hue", Float) = 1
        _Saturation("Saturation", Float) = 1
        [HideInInspector]_BUILTIN_QueueOffset("Float", Float) = 0
        [HideInInspector]_BUILTIN_QueueControl("Float", Float) = -1
    }
    SubShader
    {
        Tags
        {
            // RenderPipeline: <None>
            "RenderType"="Transparent"
            "BuiltInMaterialType" = "Unlit"
            "Queue"="Transparent"
            // DisableBatching: <None>
            "ShaderGraphShader"="true"
            "ShaderGraphTargetId"="BuiltInUnlitSubTarget"
        }
        Pass
        {
Name"Pass"
            Tags
{
                "LightMode" = "ForwardBase"
}
        
        // Render State
Cull Back

Blend SrcAlpha OneMinusSrcAlpha,
One OneMinusSrcAlpha

ZTest LEqual

ZWrite Off

ColorMask RGB
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 3.0
        #pragma multi_compile_instancing
        #pragma multi_compile_fog
        #pragma multi_compile_fwdbase
        #pragma vertex vert
        #pragma fragment frag
        
        // Keywords
        // PassKeywords: <None>
        // GraphKeywords: <None>
        
        // Defines
#define ATTRIBUTES_NEED_NORMAL
#define ATTRIBUTES_NEED_TANGENT
#define ATTRIBUTES_NEED_TEXCOORD0
#define VARYINGS_NEED_TEXCOORD0
#define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
#define SHADERPASS SHADERPASS_UNLIT
#define BUILTIN_TARGET_API 1
#define _BUILTIN_SURFACE_TYPE_TRANSPARENT 1
#ifdef _BUILTIN_SURFACE_TYPE_TRANSPARENT
#define _SURFACE_TYPE_TRANSPARENT _BUILTIN_SURFACE_TYPE_TRANSPARENT
#endif
#ifdef _BUILTIN_ALPHATEST_ON
#define _ALPHATEST_ON _BUILTIN_ALPHATEST_ON
#endif
#ifdef _BUILTIN_AlphaClip
#define _AlphaClip _BUILTIN_AlphaClip
#endif
#ifdef _BUILTIN_ALPHAPREMULTIPLY_ON
#define _ALPHAPREMULTIPLY_ON _BUILTIN_ALPHAPREMULTIPLY_ON
#endif
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
#include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Shim/Shims.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
#include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Core.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
#include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/Lighting.hlsl"
#include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/LegacySurfaceVertex.hlsl"
#include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/ShaderLibrary/ShaderGraphFunctions.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
struct Attributes
{
    float3 positionOS : POSITION;
    float3 normalOS : NORMAL;
    float4 tangentOS : TANGENT;
    float4 uv0 : TEXCOORD0;
#if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
#endif
};
struct Varyings
{
    float4 positionCS : SV_POSITION;
    float4 texCoord0;
#if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
#endif
#if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
#endif
#if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
#endif
#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
#endif
};
struct SurfaceDescriptionInputs
{
    float4 uv0;
};
struct VertexDescriptionInputs
{
    float3 ObjectSpaceNormal;
    float3 ObjectSpaceTangent;
    float3 ObjectSpacePosition;
};
struct PackedVaryings
{
    float4 positionCS : SV_POSITION;
    float4 texCoord0 : INTERP0;
#if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
#endif
#if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
#endif
#if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
#endif
#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
#endif
};
        
PackedVaryings PackVaryings(Varyings input)
{
    PackedVaryings output;
    ZERO_INITIALIZE(PackedVaryings, output);
    output.positionCS = input.positionCS;
    output.texCoord0.xyzw = input.texCoord0;
#if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
#endif
#if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
#endif
#if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
#endif
#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
#endif
    return output;
}
        
Varyings UnpackVaryings(PackedVaryings input)
{
    Varyings output;
    output.positionCS = input.positionCS;
    output.texCoord0 = input.texCoord0.xyzw;
#if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
#endif
#if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
#endif
#if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
#endif
#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
#endif
    return output;
}
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
float _Hue;
float _Saturation;
        CBUFFER_END
        
        
        // Object and Global properties
        
        // -- Property used by ScenePickingPass
#ifdef SCENEPICKINGPASS
        float4 _SelectionID;
#endif
        
        // -- Properties used by SceneSelectionPass
#ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
#endif
        
        // Graph Includes
        // GraphIncludes: <None>
        
        // Graph Functions
        
void Unity_SampleGradientV1_float(Gradient Gradient, float Time, out float4 Out)
{
    float3 color = Gradient.colors[0].rgb;
            [unroll]
    for (int c = 1; c < Gradient.colorsLength; c++)
    {
        float colorPos = saturate((Time - Gradient.colors[c - 1].w) / (Gradient.colors[c].w - Gradient.colors[c - 1].w)) * step(c, Gradient.colorsLength - 1);
        color = lerp(color, Gradient.colors[c].rgb, lerp(colorPos, step(0.01, colorPos), Gradient.type));
    }
#ifdef UNITY_COLORSPACE_GAMMA
            color = LinearToSRGB(color);
#endif
    float alpha = Gradient.alphas[0].x;
            [unroll]
    for (int a = 1; a < Gradient.alphasLength; a++)
    {
        float alphaPos = saturate((Time - Gradient.alphas[a - 1].y) / (Gradient.alphas[a].y - Gradient.alphas[a - 1].y)) * step(a, Gradient.alphasLength - 1);
        alpha = lerp(alpha, Gradient.alphas[a].x, lerp(alphaPos, step(0.01, alphaPos), Gradient.type));
    }
    Out = float4(color, alpha);
}
        
void Unity_ColorspaceConversion_RGB_HSV_float(float3 In, out float3 Out)
{
    float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
    float4 P = lerp(float4(In.bg, K.wz), float4(In.gb, K.xy), step(In.b, In.g));
    float4 Q = lerp(float4(P.xyw, In.r), float4(In.r, P.yzx), step(P.x, In.r));
    float D = Q.x - min(Q.w, Q.y);
    float E = 1e-10;
    float V = (D == 0) ? Q.x : (Q.x + E);
    Out = float3(abs(Q.z + (Q.w - Q.y) / (6.0 * D + E)), D / (Q.x + E), V);
}
        
void Unity_Multiply_float_float(float A, float B, out float Out)
{
    Out = A * B;
}
        
void Unity_Combine_float(float R, float G, float B, float A, out float4 RGBA, out float3 RGB, out float2 RG)
{
    RGBA = float4(R, G, B, A);
    RGB = float3(R, G, B);
    RG = float2(R, G);
}
        
void Unity_ColorspaceConversion_HSV_RGB_float(float3 In, out float3 Out)
{
    float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
    float3 P = abs(frac(In.xxx + K.xyz) * 6.0 - K.www);
    Out = In.z * lerp(K.xxx, saturate(P - K.xxx), In.y);
}
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
struct VertexDescription
{
    float3 Position;
    float3 Normal;
    float3 Tangent;
};
        
VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
{
    VertexDescription description = (VertexDescription) 0;
    description.Position = IN.ObjectSpacePosition;
    description.Normal = IN.ObjectSpaceNormal;
    description.Tangent = IN.ObjectSpaceTangent;
    return description;
}
        
        // Custom interpolators, pre surface
#ifdef FEATURES_GRAPH_VERTEX
Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
{
    return output;
}
#define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
#endif
        
        // Graph Pixel
struct SurfaceDescription
{
    float3 BaseColor;
    float Alpha;
};
        
SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
{
    SurfaceDescription surface = (SurfaceDescription) 0;
    float _Property_cf04fe43e40449b9ac9a716105f59449_Out_0_Float = _Hue;
    float _Property_7f1202c2ab434592922464af8dde3547_Out_0_Float = _Saturation;
    Gradient _Gradient_e01f64b7043141018b63664de3cad0e0_Out_0_Gradient = NewGradient(0, 2, 2, float4(0, 0, 0, 0), float4(1, 0, 0, 1), float4(0, 0, 0, 0), float4(0, 0, 0, 0), float4(0, 0, 0, 0), float4(0, 0, 0, 0), float4(0, 0, 0, 0), float4(0, 0, 0, 0), float2(1, 0), float2(1, 1), float2(0, 0), float2(0, 0), float2(0, 0), float2(0, 0), float2(0, 0), float2(0, 0));
    float4 _UV_24b34cfa8f674cd3b9b903bbd5490f3b_Out_0_Vector4 = IN.uv0;
    float _Split_adb72921c89646a49e8b9b4b091659bd_R_1_Float = _UV_24b34cfa8f674cd3b9b903bbd5490f3b_Out_0_Vector4[0];
    float _Split_adb72921c89646a49e8b9b4b091659bd_G_2_Float = _UV_24b34cfa8f674cd3b9b903bbd5490f3b_Out_0_Vector4[1];
    float _Split_adb72921c89646a49e8b9b4b091659bd_B_3_Float = _UV_24b34cfa8f674cd3b9b903bbd5490f3b_Out_0_Vector4[2];
    float _Split_adb72921c89646a49e8b9b4b091659bd_A_4_Float = _UV_24b34cfa8f674cd3b9b903bbd5490f3b_Out_0_Vector4[3];
    float4 _SampleGradient_fc0c5e8ea73a48729e56243a5354f213_Out_2_Vector4;
    Unity_SampleGradientV1_float(_Gradient_e01f64b7043141018b63664de3cad0e0_Out_0_Gradient, _Split_adb72921c89646a49e8b9b4b091659bd_R_1_Float, _SampleGradient_fc0c5e8ea73a48729e56243a5354f213_Out_2_Vector4);
    float3 _ColorspaceConversion_2762aa241a5f4ec8bd96cfc45990c438_Out_1_Vector3;
    Unity_ColorspaceConversion_RGB_HSV_float((_SampleGradient_fc0c5e8ea73a48729e56243a5354f213_Out_2_Vector4.xyz), _ColorspaceConversion_2762aa241a5f4ec8bd96cfc45990c438_Out_1_Vector3);
    float _Split_073ff2d5c714471480ac30e066a180ff_R_1_Float = _ColorspaceConversion_2762aa241a5f4ec8bd96cfc45990c438_Out_1_Vector3[0];
    float _Split_073ff2d5c714471480ac30e066a180ff_G_2_Float = _ColorspaceConversion_2762aa241a5f4ec8bd96cfc45990c438_Out_1_Vector3[1];
    float _Split_073ff2d5c714471480ac30e066a180ff_B_3_Float = _ColorspaceConversion_2762aa241a5f4ec8bd96cfc45990c438_Out_1_Vector3[2];
    float _Split_073ff2d5c714471480ac30e066a180ff_A_4_Float = 0;
    float _Multiply_b67aba4221fd45a09ad4083df7c71ce4_Out_2_Float;
    Unity_Multiply_float_float(_Property_7f1202c2ab434592922464af8dde3547_Out_0_Float, _Split_073ff2d5c714471480ac30e066a180ff_G_2_Float, _Multiply_b67aba4221fd45a09ad4083df7c71ce4_Out_2_Float);
    float4 _Combine_8e2d2fbe932147f5a303d66dbf0d214e_RGBA_4_Vector4;
    float3 _Combine_8e2d2fbe932147f5a303d66dbf0d214e_RGB_5_Vector3;
    float2 _Combine_8e2d2fbe932147f5a303d66dbf0d214e_RG_6_Vector2;
    Unity_Combine_float(_Property_cf04fe43e40449b9ac9a716105f59449_Out_0_Float, _Multiply_b67aba4221fd45a09ad4083df7c71ce4_Out_2_Float, _Split_073ff2d5c714471480ac30e066a180ff_B_3_Float, 0, _Combine_8e2d2fbe932147f5a303d66dbf0d214e_RGBA_4_Vector4, _Combine_8e2d2fbe932147f5a303d66dbf0d214e_RGB_5_Vector3, _Combine_8e2d2fbe932147f5a303d66dbf0d214e_RG_6_Vector2);
    float3 _ColorspaceConversion_07c1996dcccf4cc9a2b3207e4a2e44bd_Out_1_Vector3;
    Unity_ColorspaceConversion_HSV_RGB_float(_Combine_8e2d2fbe932147f5a303d66dbf0d214e_RGB_5_Vector3, _ColorspaceConversion_07c1996dcccf4cc9a2b3207e4a2e44bd_Out_1_Vector3);
    surface.BaseColor = _ColorspaceConversion_07c1996dcccf4cc9a2b3207e4a2e44bd_Out_1_Vector3;
    surface.Alpha = 1;
    return surface;
}
        
        // --------------------------------------------------
        // Build Graph Inputs
        
VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
{
    VertexDescriptionInputs output;
    ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
    output.ObjectSpaceNormal = input.normalOS;
    output.ObjectSpaceTangent = input.tangentOS.xyz;
    output.ObjectSpacePosition = input.positionOS;
        
    return output;
}
SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
{
    SurfaceDescriptionInputs output;
    ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
            
        
        
        
        
        
        
#if UNITY_UV_STARTS_AT_TOP
#else
#endif
        
        
    output.uv0 = input.texCoord0;
#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
#define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
#else
#define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
#endif
#undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
    return output;
}
        
void BuildAppDataFull(Attributes attributes, VertexDescription vertexDescription, inout appdata_full result)
{
    result.vertex = float4(attributes.positionOS, 1);
    result.tangent = attributes.tangentOS;
    result.normal = attributes.normalOS;
    result.texcoord = attributes.uv0;
    result.vertex = float4(vertexDescription.Position, 1);
    result.normal = vertexDescription.Normal;
    result.tangent = float4(vertexDescription.Tangent, 0);
#if UNITY_ANY_INSTANCING_ENABLED
#endif
}
        
void VaryingsToSurfaceVertex(Varyings varyings, inout v2f_surf result)
{
    result.pos = varyings.positionCS;
            // World Tangent isn't an available input on v2f_surf
        
        
#if UNITY_ANY_INSTANCING_ENABLED
#endif
#if UNITY_SHOULD_SAMPLE_SH
#if !defined(LIGHTMAP_ON)
#endif
#endif
#if defined(LIGHTMAP_ON)
#endif
#ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
                result.fogCoord = varyings.fogFactorAndVertexLight.x;
                COPY_TO_LIGHT_COORDS(result, varyings.fogFactorAndVertexLight.yzw);
#endif
        
    DEFAULT_UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(varyings, result);
}
        
void SurfaceVertexToVaryings(v2f_surf surfVertex, inout Varyings result)
{
    result.positionCS = surfVertex.pos;
            // viewDirectionWS is never filled out in the legacy pass' function. Always use the value computed by SRP
            // World Tangent isn't an available input on v2f_surf
        
#if UNITY_ANY_INSTANCING_ENABLED
#endif
#if UNITY_SHOULD_SAMPLE_SH
#if !defined(LIGHTMAP_ON)
#endif
#endif
#if defined(LIGHTMAP_ON)
#endif
#ifdef VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
                result.fogFactorAndVertexLight.x = surfVertex.fogCoord;
                COPY_FROM_LIGHT_COORDS(result.fogFactorAndVertexLight.yzw, surfVertex);
#endif
        
    DEFAULT_UNITY_TRANSFER_VERTEX_OUTPUT_STEREO(surfVertex, result);
}
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.shadergraph/Editor/Generation/Targets/BuiltIn/Editor/ShaderGraph/Includes/UnlitPass.hlsl"
        
        ENDHLSL
}
}
}