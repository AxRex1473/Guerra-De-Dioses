Shader "Custom/WaterShader" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Color ("Main Color", Color) = (1,1,1,1)
        _WaveSpeed ("Wave Speed", Range(0.1, 2.0)) = 1.0
        _WaveScale ("Wave Scale", Range(0.01, 0.1)) = 0.02
    }
    SubShader {
        Tags { "RenderType"="Transparent" }
        LOD 200
        
        CGPROGRAM
        #pragma surface surf Lambert alpha:fade

        sampler2D _MainTex;
        fixed4 _Color;
        half _WaveSpeed;
        half _WaveScale;

        struct Input {
            float2 uv_MainTex;
            float3 worldPos;
        };

        void surf (Input IN, inout SurfaceOutput o) {
            float wave = sin((IN.worldPos.x + _Time.y * _WaveSpeed) * 10.0) * _WaveScale;
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex + wave);
            o.Albedo = c.rgb * _Color.rgb;
            o.Alpha = c.a * _Color.a;
        }
        ENDCG
    } 
    FallBack "Transparent/Diffuse"
}
