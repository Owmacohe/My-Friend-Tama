Shader "Universal Render Pipeline/OccaSoftware/Stylized Outline Shader"
{

    Properties
    {
        [HDR]_OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineThickness ("Outline Thickness", Float) = 0.1
        [Toggle(USE_VERTEX_COLOR_ENABLED)] _UseVertexColors ("Use Vertex Color (R) for Outline Thickness?", Float) = 0
        [Toggle(ATTENUATE_BY_DISTANCE_ENABLED)] _AttenuateByDistance ("Attenuate Outline Thickness by Camera Distance?", Float) = 0
        [Toggle(RANDOM_OFFSETS_ENABLED)] _RandomOffset ("Randomly offset the sample position", Float) = 0
        _CompleteFalloffDistance ("Complete Falloff Distance", Float) = 30.0
        _NoiseTexture ("Noise Texture", 2D) = "white" {} 
        _NoiseFrequency ("Noise Frequency", Float) = 5.0
        _NoiseFramerate ("Noise Framerate", Float) = 12.0
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline" = "UniversalPipeline" "LightMode" = "SRPDefaultUnlit"}
        ZWrite On

        HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

        CBUFFER_START(UnityPerMaterial)
            float4 _OutlineColor;
            float _OutlineThickness;
            float _CompleteFalloffDistance;
            Texture2D _NoiseTexture;
            SAMPLER(sampler_linear_repeat);
            float _NoiseFrequency;
            float _NoiseFramerate;
        CBUFFER_END

        struct VertexInput
        {
            half4 position : POSITION;
            half3 normal : NORMAL;
            half4 color : COLOR;
        };

        struct VertexOutput
        {
            half4 position : SV_POSITION;
        };
        
        ENDHLSL

        Pass
        {
            Name "DrawOutlines"
            Cull Front
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma shader_feature_local USE_VERTEX_COLOR_ENABLED
            #pragma shader_feature_local ATTENUATE_BY_DISTANCE_ENABLED
            #pragma shader_feature_local RANDOM_OFFSETS_ENABLED

            float nrand(float2 uv)
            {
                return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
            }

            VertexOutput vert(VertexInput i)
            {
                VertexOutput o;

                _OutlineThickness = max(_OutlineThickness, 0);

                #if defined(USE_VERTEX_COLOR_ENABLED)
                _OutlineThickness *= i.color.r;
                #endif

                
                half3 normalWS = normalize(GetVertexNormalInputs(i.normal).normalWS);
                half3 positionWS = TransformObjectToWorld(i.position.xyz);

                #if defined(ATTENUATE_BY_DISTANCE_ENABLED)
                _OutlineThickness *= 1.0 - saturate(distance(_WorldSpaceCameraPos, positionWS) / _CompleteFalloffDistance);
                #endif

                half dist = length(i.position.xyz);

                half r = _Time.y * _NoiseFramerate;
                #if defined(RANDOM_OFFSETS_ENABLED)
                r = nrand(floor(r));
                #endif

                half value = _NoiseTexture.SampleLevel(sampler_linear_repeat, half2(r + (dist * _NoiseFrequency), 0), 0).r;
                _OutlineThickness *= value;
                
                half3 newPositionWS = positionWS + (normalWS * _OutlineThickness);
                o.position = TransformWorldToHClip(newPositionWS);
                return o;
            }

            half4 frag(VertexOutput i) : SV_Target
            {
                return _OutlineColor;
            }
            ENDHLSL
        }
    }
    
    CustomEditor "OccaSoftware.StylizedOutlines.Editor.StylizedOutlinesGUI"
}
