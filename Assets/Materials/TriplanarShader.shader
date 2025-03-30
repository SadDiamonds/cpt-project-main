Shader "Custom/ObjectSpaceUV_Triplanar_CorrectScale"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Scale ("Texture Scale", float) = 1
        _BlendSharpness ("Blend Sharpness", float) = 5.0
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        sampler2D _MainTex;
        float _Scale;
        float _BlendSharpness;

        struct Input
        {
            float3 worldPos;
            float3 worldNormal;
            INTERNAL_DATA
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float3 objectPos = mul(unity_WorldToObject, float4(IN.worldPos, 1.0)).xyz;

            // Get the scale of the object
            float3 scale;
            scale.x = length(unity_ObjectToWorld._m00_m10_m20);
            scale.y = length(unity_ObjectToWorld._m01_m11_m21);
            scale.z = length(unity_ObjectToWorld._m02_m12_m22);

            objectPos *= scale; // Multiply instead of divide

            float3 objectNormal = mul((float3x3)unity_WorldToObject, IN.worldNormal);

            float3 blendWeights = pow(abs(objectNormal), _BlendSharpness);
            blendWeights /= (blendWeights.x + blendWeights.y + blendWeights.z);

            float2 xUV = objectPos.zy * _Scale;
            float2 yUV = objectPos.xz * _Scale;
            float2 zUV = objectPos.xy * _Scale;

            fixed4 xColor = tex2D(_MainTex, xUV);
            fixed4 yColor = tex2D(_MainTex, yUV);
            fixed4 zColor = tex2D(_MainTex, zUV);

            fixed4 blendedColor = xColor * blendWeights.x + yColor * blendWeights.y + zColor * blendWeights.z;

            o.Albedo = blendedColor.rgb;
            o.Smoothness = 0.0;
            o.Metallic = 0.0;
        }
        ENDCG
    }
    FallBack "Diffuse"
}


