Shader "Custom/RaceSwap"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _TargetColorSKIN1("SKIN1 Target", Color) = (1,1,1,1)
        _TargetColorSKIN2("SKIN2 Target", Color) = (1,1,1,1)
        _TargetColorSKIN3("SKIN3 Target", Color) = (1,1,1,1)
        _TargetColorSKIN4("SKIN4 Target", Color) = (1,1,1,1)
    }
       SubShader
    {
        Tags { "RenderType" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
 
        Cull Off
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
 
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
 
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
 
            sampler2D _MainTex;
            float4 _MainTex_ST;
             float4 _OriginalColorSKIN1;
            float4 _OriginalColorSKIN2;
            float4 _OriginalColorSKIN3;
            float4 _OriginalColorSKIN4;
            float4 _TargetColorSKIN1;
            float4 _TargetColorSKIN2;
            float4 _TargetColorSKIN3;
            float4 _TargetColorSKIN4;
            float _Tolerance;
 
            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
 
            half4 frag(v2f i) : SV_Target
            {
                half4 col = tex2D(_MainTex, i.uv);
 
                if (col.a == 0)
                {
                    return half4(0, 0, 0, 0);
                }
                if (length(col - _OriginalColorSKIN1) < _Tolerance)
                {
                    return half4(_TargetColorSKIN1.rgb, col.a);
                }
                if (length(col - _OriginalColorSKIN2) < _Tolerance)
                {
                    return half4(_TargetColorSKIN2.rgb, col.a);
                }if (length(col - _OriginalColorSKIN3) < _Tolerance)
                {
                    return half4(_TargetColorSKIN3.rgb, col.a);
                }
                if (length(col - _OriginalColorSKIN4) < _Tolerance)
                {
                    return half4(_TargetColorSKIN4.rgb, col.a);
                }
                return col;
            }
            ENDCG
        }
    }
}