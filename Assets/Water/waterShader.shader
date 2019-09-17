
Shader "Unlit/waterShader"
{
    Properties
    {
        _sunColor("_sunColor", Color) = (0, 0, 0)
        _sunPosition("_sunPosition", Vector) = (0.0, 0.0, 0.0)
         _moonColor("_moonColor", Color) = (0.55, 0.76, 1, 1)
        _moonPosition("_moonPosition", Vector) = (0.0, 0.0, 0.0)
        // _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
    
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            uniform float3 _sunColor;
            uniform float3 _sunPosition;
            uniform float3 _moonColor;
            uniform float3 _moonPosition;
            
            struct vertIn
            {
                float4 vertex : POSITION;
                float4 normal : NORMAL;
                float4 color : COLOR;
            };

            struct vertOut
            {
                float4 vertex : SV_POSITION;
                float4 worldVertex : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
                float4 color : COLOR;
            };

            // Implementation of the vertex shader
            vertOut vert(vertIn v)
            {   
                float4 displacement = float4(0.0f, 0.5 * sin(v.vertex.x + _Time.y), 0.25 * sin(v.vertex.x + _Time.y), 0.0f);
                v.vertex += displacement;
                // v.color = tex2D(_MainTex, v.uv);
                
                vertOut o;

                float4 worldVertex = mul(unity_ObjectToWorld, v.vertex);
                float3 worldNormal = normalize(mul(transpose((float3x3)unity_WorldToObject), v.normal.xyz));
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = v.color;

                o.worldVertex = worldVertex;
                o.worldNormal = worldNormal;

                return o;
            }

            // Implementation of the fragment shader
            fixed4 frag(vertOut v) : SV_Target
            {
            
            // v.color = tex2D(_MainTex, v.uv);
                float3 interpNormal = normalize(v.worldNormal);

                // Ambient RGB intensities with Ka = 1
                float Ka = 1.5;
                float3 amb = v.color.rgb * UNITY_LIGHTMODEL_AMBIENT.rgb * Ka;
                
                // Diffusion reflection with fAtt = 1, kd = 0.8
                float fAtt = 1;
                float Kd_sun = 1; // sun diffusion
                float kd_moon = 0.3;
                float3 L = normalize(_sunPosition - v.worldVertex.xyz);
                float LdotN = dot(L, interpNormal);
                float3 dif_sun = fAtt * _sunColor.rgb * Kd_sun * v.color.rgb * saturate(LdotN);
                float3 dif_moon = fAtt * _moonColor.rgb * kd_moon * v.color.rgb * saturate(LdotN);

                // Specular reflection with Ks = 0.2
                float Ks_sun = 0.6;
                float Ks_moon = 0.2;
                float specN = 15; 
                float3 V = normalize(_WorldSpaceCameraPos - v.worldVertex.xyz);
                specN = 25; 
                float3 H = normalize(V + L);
                float3 spe_sun = fAtt * _sunColor.rgb * Ks_sun * pow(saturate(dot(interpNormal, H)), specN);
                float3 spe_moon = fAtt * _moonColor.rgb * Ks_moon * pow(saturate(dot(interpNormal, H)), specN);

                float4 returnColor = float4(0.0f, 0.0f, 0.0f, 0.0f);
                returnColor.rgb = amb.rgb + dif_sun.rgb + spe_sun.rgb
                    + dif_moon.rgb + spe_moon.rgb;
                returnColor.a = 0.8f;
                // returnColor = tex2D(_MainTex, v.uv);

                return returnColor;
            }
            ENDCG
        }
    }
}