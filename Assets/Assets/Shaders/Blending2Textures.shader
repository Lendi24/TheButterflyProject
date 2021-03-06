Shader "Butterfly/Blending2Textures"
{
    Properties
    {
		_MainTex("Alpha Texture", 2D) = "white"
		_SecondaryTex("Filter Texture", 2D) = "white" {}
		_LerpValue("Transition float", Range(0,1)) = 0.5
    }

    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert alpha
            #pragma fragment frag 
            // make fog work
            #pragma multi_compile_fog alpha

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

			sampler2D _MainTex;
			float4 _MainTex_ST;
			sampler2D _SecondaryTex;
			float4 _SecondaryTex_ST;
			float _LerpValue;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = lerp(tex2D(_MainTex, i.uv),tex2D(_SecondaryTex, i.uv),_LerpValue);
				
                return col;
            }
            ENDCG
        }
    }
}
