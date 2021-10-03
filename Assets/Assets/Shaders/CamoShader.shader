Shader "Butterfly/CamoShader"
{
    Properties
    {
        _MainTex("Alpha Texture", 2D) = "white"
        _SecondaryTex("Filter Texture", 2D) = "white" {}
        _LerpValue("Transition float", Range(0,1)) = 0.5

        _enablePerlin("Enable Perlin", Range(0,1)) = 0
        _offsetX("OffsetX",Float) = 0.0
        _offsetY("OffsetY",Float) = 0.0
        _octaves("Octaves",Int) = 7
        _lacunarity("Lacunarity", Range(1.0 , 5.0)) = 1
        _gain("Gain", Range(0.0 , 1.0)) = 1
        _value("Value", Range(-2.0 , 2.0)) = -1.5
        _amplitude("Amplitude", Range(0.0 , 5.0)) = 1.3
        _frequency("Frequency", Range(0.0 , 6.0)) = 1
        _power("Power", Range(0.1 , 5.0)) = 0.1
        _scale("Scale", Float) = 1.0
        _color("Color", Color) = (1.0,1.0,1.0,1.0)
        [Toggle] _monochromatic("Monochromatic", Float) = 1
        _range("Monochromatic Range", Range(0.0 , 1.0)) = 0.006

    }
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0


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

            sampler2D _MainTex, _SecondaryTex;
            float4 _MainTex_ST, _SecondaryTex_ST, _color;
            float _LerpValue, _octaves, _lacunarity, _gain, _value, _amplitude, _frequency, _offsetX, _offsetY, _power, _scale, _monochromatic, _range, _enablePerlin;

            float fbm(float2 p)
            {
                p = p * _scale + float2(_offsetX, _offsetY);
                for (int i = 0; i < _octaves; i++)
                {
                    float2 i = floor(p * _frequency);
                    float2 f = frac(p * _frequency);
                    float2 t = f * f * f * (f * (f * 6.0 - 15.0) + 10.0);
                    float2 a = i + float2(0.0, 0.0);
                    float2 b = i + float2(1.0, 0.0);
                    float2 c = i + float2(0.0, 1.0);
                    float2 d = i + float2(1.0, 1.0);
                    a = -1.0 + 2.0 * frac(sin(float2(dot(a, float2(127.1, 311.7)), dot(a, float2(269.5, 183.3)))) * 43758.5453123);
                    b = -1.0 + 2.0 * frac(sin(float2(dot(b, float2(127.1, 311.7)), dot(b, float2(269.5, 183.3)))) * 43758.5453123);
                    c = -1.0 + 2.0 * frac(sin(float2(dot(c, float2(127.1, 311.7)), dot(c, float2(269.5, 183.3)))) * 43758.5453123);
                    d = -1.0 + 2.0 * frac(sin(float2(dot(d, float2(127.1, 311.7)), dot(d, float2(269.5, 183.3)))) * 43758.5453123);
                    float A = dot(a, f - float2(0.0, 0.0));
                    float B = dot(b, f - float2(1.0, 0.0));
                    float C = dot(c, f - float2(0.0, 1.0));
                    float D = dot(d, f - float2(1.0, 1.0));
                    float noise = (lerp(lerp(A, B, t.x), lerp(C, D, t.x), t.y));
                    _value += _amplitude * noise;
                    _frequency *= _lacunarity;
                    _amplitude *= _gain;
                }
                _value = clamp(_value, -1.0, 1.0);
                return pow(_value * 0.5 + 0.5, _power);
            }            

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = lerp(tex2D(_MainTex, i.uv), tex2D(_SecondaryTex, i.uv), _LerpValue);

                float2 uv = i.uv.xy;
                float c = fbm(uv);
                if (_monochromatic == 0.0)
                    return float4(c,c,c,c) * _color * _LerpValue;
                else
                if (c < _range)
                    return col;
                else
                    return lerp(col, _color, _enablePerlin);
            }

            ENDCG
        }
    }
}
