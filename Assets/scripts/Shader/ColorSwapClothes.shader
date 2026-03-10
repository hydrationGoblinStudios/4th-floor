Shader "Custom/ClothesSwap"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _TargetColorCLOTHES1("CLOTHES1 Target", Color) = (1,1,1,1)
        _TargetColorCLOTHES2("CLOTHES2 Target", Color) = (1,1,1,1)
        _TargetColorCLOTHES3("CLOTHES3 Target", Color) = (1,1,1,1)
        _TargetColorCLOTHES4("CLOTHES4 Target", Color) = (1,1,1,1)
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
            float4 _OriginalColorCLOTHES1;
            float4 _OriginalColorCLOTHES2;
            float4 _OriginalColorCLOTHES3;
            float4 _OriginalColorCLOTHES4;
            float4 _TargetColorCLOTHES1;
            float4 _TargetColorCLOTHES2;
            float4 _TargetColorCLOTHES3;
            float4 _TargetColorCLOTHES4;
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
                if (length(col - _OriginalColorCLOTHES1) < _Tolerance)
                {
                    return half4(_TargetColorCLOTHES1.rgb, col.a);
                }
                if (length(col - _OriginalColorCLOTHES2) < _Tolerance)
                {
                    return half4(_TargetColorCLOTHES2.rgb, col.a);
                }if (length(col - _OriginalColorCLOTHES3) < _Tolerance)
                {
                    return half4(_TargetColorCLOTHES3.rgb, col.a);
                }
                if (length(col - _OriginalColorCLOTHES4) < _Tolerance)
                {
                    return half4(_TargetColorCLOTHES4.rgb, col.a);
                }
                return col;
            }
            ENDCG
        }
    }
}