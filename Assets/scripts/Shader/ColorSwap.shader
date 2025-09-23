Shader "Custom/ExactColorSwap"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _OriginalColorSKIN1("SKIN1 Original", Color) = (1,1,1,1)
        _OriginalColorSKIN2("SKIN2 Original", Color) = (1,1,1,1)
        _OriginalColorSKIN3("SKIN3 Original", Color) = (1,1,1,1)
        _OriginalColorSKIN4("SKIN4 Original", Color) = (1,1,1,1)
        _OriginalColorCLOTHES1("CLOTHES1 Original", Color) = (1,1,1,1)
        _OriginalColorCLOTHES2("CLOTHES2 Original", Color) = (1,1,1,1)
        _OriginalColorCLOTHES3("CLOTHES3 Original", Color) = (1,1,1,1)
        _OriginalColorCLOTHES4("CLOTHES4 Original", Color) = (1,1,1,1)
        _OriginalColorWPN1("wpn1 Original", Color) = (1,1,1,1)
        _OriginalColorWPN2("wpn2 Original", Color) = (1,1,1,1)
        _OriginalColorWPN3("wpn3 Original", Color) = (1,1,1,1)
        _OriginalColorHAIR1("HAIR1 Original", Color) = (1,1,1,1)
        _OriginalColorHAIR2("HAIR2 Original", Color) = (1,1,1,1)
        _OriginalColorHAIR3("HAIR3 Original", Color) = (1,1,1,1)
        _TargetColorSKIN1("SKIN1 Target", Color) = (1,1,1,1)
        _TargetColorSKIN2("SKIN2 Target", Color) = (1,1,1,1)
        _TargetColorSKIN3("SKIN3 Target", Color) = (1,1,1,1)
        _TargetColorSKIN4("SKIN4 Target", Color) = (1,1,1,1)
        _TargetColorCLOTHES1("CLOTHES1 Target", Color) = (1,1,1,1)
        _TargetColorCLOTHES2("CLOTHES2 Target", Color) = (1,1,1,1)
        _TargetColorCLOTHES3("CLOTHES3 Target", Color) = (1,1,1,1)
        _TargetColorCLOTHES4("CLOTHES4 Target", Color) = (1,1,1,1)
        _TargetColorWPN1("wpn1 Target", Color) = (1,1,1,1)
        _TargetColorWPN2("wpn2 Target", Color) = (1,1,1,1)
        _TargetColorWPN3("wpn3 Target", Color) = (1,1,1,1)
        _TargetColorHAIR1("HAIR1 Target", Color) = (1,1,1,1)
        _TargetColorHAIR2("HAIR2 Target", Color) = (1,1,1,1)
        _TargetColorHAIR3("HAIR3 Target", Color) = (1,1,1,1)
        _Tolerance("Tolerance", Range(0, 0.01)) = 0.001  
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
            float4 _OriginalColorCLOTHES1;
            float4 _OriginalColorCLOTHES2;
            float4 _OriginalColorCLOTHES3;
            float4 _OriginalColorCLOTHES4;
            float4 _OriginalColorWPN1;
            float4 _OriginalColorWPN2;
            float4 _OriginalColorWPN3;
            float4 _OriginalColorHAIR1;
            float4 _OriginalColorHAIR2;
            float4 _OriginalColorHAIR3;
            float4 _TargetColorSKIN1;
            float4 _TargetColorSKIN2;
            float4 _TargetColorSKIN3;
            float4 _TargetColorSKIN4;
            float4 _TargetColorCLOTHES1;
            float4 _TargetColorCLOTHES2;
            float4 _TargetColorCLOTHES3;
            float4 _TargetColorCLOTHES4;
            float4 _TargetColorWPN1;
            float4 _TargetColorWPN2;
            float4 _TargetColorWPN3;
            float4 _TargetColorHAIR1;
            float4 _TargetColorHAIR2;
            float4 _TargetColorHAIR3;
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
 

                if (length(col - _OriginalColorWPN1) < _Tolerance)
                {
                    return half4(_TargetColorWPN1.rgb, col.a);
                }
                if (length(col - _OriginalColorWPN2) < _Tolerance)
                {
                    return half4(_TargetColorWPN2.rgb, col.a);
                }if (length(col - _OriginalColorWPN3) < _Tolerance)
                {
                    return half4(_TargetColorWPN3.rgb, col.a);
                }



                if (length(col - _OriginalColorHAIR1) < _Tolerance)
                {
                    return half4(_TargetColorHAIR1.rgb, col.a);
                }
                if (length(col - _OriginalColorHAIR2) < _Tolerance)
                {
                    return half4(_TargetColorHAIR2.rgb, col.a);
                }if (length(col - _OriginalColorHAIR3) < _Tolerance)
                {
                    return half4(_TargetColorHAIR3.rgb, col.a);
                }
                return col;
            }
            ENDCG
        }
 
 
    }
}