Shader "Custom/FlipHorizontal"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // Add SPI related pragmas
            #pragma multi_compile_instancing
            #pragma target 3.0
            
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID  // Add instance ID input
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                UNITY_VERTEX_OUTPUT_STEREO  // Add stereo target eye output
            };

            v2f vert (appdata v)
            {
                v2f o;
                // Setup instance ID
                UNITY_SETUP_INSTANCE_ID(v);
                // Set stereo target eye
                UNITY_INITIALIZE_OUTPUT(v2f, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                
                o.vertex = UnityObjectToClipPos(v.vertex);
                // Flip the UV horizontally
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv.x = 1.0 - o.uv.x;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Set stereo target eye for fragment shader
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}