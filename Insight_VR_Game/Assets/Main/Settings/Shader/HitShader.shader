//Shader "Custom/HitShader"
//{
//    Properties
//    {
//        _Color("Main Color", Color) = (.5,.5,.5,1)
//        _MainTex("Base (RGB)", 2D) = "white" { }
//    }
//    SubShader
//    {
//        Tags {"Queue" = "Overlay" }
//
//        CGPROGRAM
//        #pragma surface surf Lambert
//
//        struct Input
//        {
//            float2 uv_MainTex;
//        };
//
//        sampler2D _MainTex;
//        fixed4 _Color;
//
//        void surf(Input IN, inout SurfaceOutput o)
//        {
//            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
//            o.Albedo = c.rgb;
//            o.Alpha = c.a;
//        }
//        ENDCG
//    }
//}
Shader "Custom/HitShaderWithShadow" {
    Properties{
        _Color("Main Color", Color) = (.5,.5,.5,1)
        _MainTex("Base (RGB)", 2D) = "white" { }
    }
        SubShader{
            Tags {"Queue" = "Geometry"}

            CGPROGRAM
            #pragma surface surf Lambert alpha:fade nolightmap

            struct Input {
                float2 uv_MainTex;
            };

            sampler2D _MainTex;
            fixed4 _Color;

            void surf(Input IN, inout SurfaceOutput o) {
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
                o.Albedo = c.rgb;
                o.Alpha = c.a;
            }
            ENDCG

                // Shadow caster pass
                Pass {
                    Tags {"LightMode" = "ShadowCaster"}
                    CGPROGRAM
                    #pragma vertex vert
                    #pragma fragment frag
                    #pragma target 3.0

                    #include "UnityCG.cginc"

                    struct appdata {
                        float4 vertex : POSITION;
                        float2 uv : TEXCOORD0;
                    };

                    v2f vert(appdata v) {
                        v2f o;
                        o.vertex = UnityObjectToClipPos(v.vertex);
                        return o;
                    }

                    float frag(v2f i) : SV_Target {
                        return 1.0;
                    }
                    ENDCG
                }
    }
        Fallback "Diffuse"
}