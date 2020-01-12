// 发光剑的光晕实现
// By XiaoZeFeng
Shader "custom/LightSword" 
{
    Properties
    {
        _MainTex("Main Texture (RGB)", 2D) = "white" {}                     //  主贴图  //
        _MainColorTimes("MainColorTimes", Range(0, 30)) = 1                 //  主图颜色增强倍数  //
        _EmissionTex("_EmissionTex", 2D) = "white" {}                       //  光晕Alpha图,取Alpha值填补Emission颜色    //
        _EmissionAlphaTimes("EmissionAlphaTimes", Range(0, 50)) = 1         //  光晕Alpha增强倍数 //
        _EmissionAlphaExponent("EmissionAlphaExponent", Range(0, 10)) = 1   //  光晕Alpha指数，用于消除黑边    //

        _Emission1("Emmisive Color1", Color) = (0,0,0,0)                    //  剑体本身的发光颜色   //
        _EmissionColorTimes1("EmissionColorTimes1", float) = 1              //  剑体本身的发光颜色倍数 //

        _Emission2("Emmisive Color2", Color) = (0,0,0,0)                    //  剑体光晕的发光颜色   //
        _EmissionColorTimes2("EmissionColorTimes2", float) = 1              //  剑体光晕的发光颜色倍数 //

        _AllAlpha("AllAlpha", Range(0, 1)) = 1                              //  整体Alpha值    //
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent+100"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
        }

        LOD 100
        Cull Off
        ZWrite Off
        AlphaTest Off
        Blend SrcAlpha OneMinusSrcAlpha
        Fog{ Mode Off }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            uniform fixed _MainColorTimes;
            sampler2D _EmissionTex;
            uniform float _EmissionAlphaTimes;
            uniform float _EmissionAlphaExponent;
            uniform fixed4 _Emission1;
            uniform fixed _EmissionColorTimes1;
            uniform fixed4 _Emission2;
            uniform fixed _EmissionColorTimes2;
            uniform fixed _AllAlpha;

            struct appdata_t
            {
                float4 vertex   : POSITION;
                fixed4 color : COLOR;
                float2 texcoord : TEXCOORD;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color : COLOR;
                half2 texcoord  : TEXCOORD;
            };

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color;

                return OUT;
            }

            fixed4 frag(v2f IN) : COLOR
            {
                fixed4 mainColor = tex2D(_MainTex, IN.texcoord);
                fixed4 emisionColor = tex2D(_EmissionTex, IN.texcoord);

                mainColor.rgb = mainColor.rgb * _MainColorTimes
                    + _Emission1 * mainColor.a * _EmissionColorTimes1
                    + _Emission2 * (1 - mainColor.a) * emisionColor.a * _EmissionColorTimes2;

                mainColor.a = max(mainColor.a, pow(emisionColor.a, _EmissionAlphaExponent)
                 * _EmissionAlphaTimes) * _AllAlpha;

                return  mainColor;
            }
            ENDCG
        }
    }
}