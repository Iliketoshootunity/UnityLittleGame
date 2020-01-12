﻿Shader "Custom/LogoShader" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _FlashColor ("Flash Color", Color) = (1,1,1,1)
        _Angle ("Flash Angle", Range(0, 180)) = 45
        _Width ("Flash Width", Range(0, 1)) = 0.2
        _LoopTime ("Loop Time", Float) = 1
        _Interval ("Time Interval", Float) = 3
//        _BeginTime ("Begin Time", Float) = 2
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200
        Blend SrcAlpha OneMinusSrcAlpha //指定alphaBlend
        CGPROGRAM
        #pragma surface surf Lambert alpha exclude_path:prepass noforwardadd
//      #pragma target 3.0

        sampler2D _MainTex;
        float4 _FlashColor;
        float _Angle;
        float _Width;
        float _LoopTime;
        float _Interval;
//        float _BeginTime;

        struct Input 
        {
            half2 uv_MainTex;
        };

        float inFlash(half2 uv)
        {   
            float brightness = 0;

            float angleInRad = 0.0174444 * _Angle;
            float tanInverseInRad = 1.0 / tan(angleInRad);

            float currentTime = _Time.y;
            float totalTime = _Interval + _LoopTime;
            float currentTurnStartTime = (int)((currentTime / totalTime)) * totalTime;
            float currentTurnTimePassed = currentTime - currentTurnStartTime - _Interval;

            bool onLeft = (tanInverseInRad > 0);
            float xBottomFarLeft = onLeft? 0.0 : tanInverseInRad;
            float xBottomFarRight = onLeft? (1.0 + tanInverseInRad) : 1.0;

            float percent = currentTurnTimePassed / _LoopTime;
            float xBottomRightBound = xBottomFarLeft + percent * (xBottomFarRight - xBottomFarLeft);
            float xBottomLeftBound = xBottomRightBound - _Width;

            float xProj = uv.x + uv.y * tanInverseInRad;

            if(xProj > xBottomLeftBound && xProj < xBottomRightBound)
            {
                brightness = 1.0 - abs(2.0 * xProj - (xBottomLeftBound + xBottomRightBound)) / _Width;
            }

            return brightness;
        }

        void surf (Input IN, inout SurfaceOutput o)
        {                
            half4 texCol = tex2D(_MainTex, IN.uv_MainTex);
            float brightness = inFlash(IN.uv_MainTex);

//            o.Albedo = texCol.rgb + _FlashColor.rgb * brightness;
            o.Emission = texCol.rgb + _FlashColor.rgb * brightness;//改为输出Emission，不受光影响
            o.Alpha = texCol.a;
        }

        ENDCG     
    }
        FallBack "Diffuse"
//    FallBack "Unlit/Transparent"
}