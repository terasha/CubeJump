// Simplified Alpha Blended Particle shader. Differences from regular Alpha Blended Particle one:
// - no Tint color
// - no Smooth particle support
// - no AlphaTest
// - no ColorMask

Shader "Mobile/Particles/Alpha Blended TintColor" {
	Properties {
		_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_AlphaTex ("Alpha Texture", 2D) = "white" {}
	}
	
	SubShader
	{
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		
	    Pass
	    {
			Blend SrcAlpha OneMinusSrcAlpha
			AlphaTest Off
			Cull Off Lighting Off ZWrite Off Fog { Mode Off }
	    
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			
			sampler2D _MainTex;
			sampler2D _AlphaTex;
			half4 _MainTex_ST;
			fixed4 _TintColor;
			
			struct appdata_t
			{
				half4 vertex : POSITION;
				fixed4 color : COLOR;
				half2 texcoord : TEXCOORD0;
			};
			
			struct v2f {
			    half4  vertex : SV_POSITION;
			    fixed4 color : COLOR;
			    half2  texcoord : TEXCOORD0;
			};
			
			v2f vert (appdata_t v)
			{
			    v2f o;
			    o.vertex = mul (UNITY_MATRIX_MVP, v.vertex);
			    o.color = v.color;
			    o.texcoord = TRANSFORM_TEX (v.texcoord, _MainTex);
			    return o;
			}
			
			fixed4 frag (v2f i) : COLOR
			{
			    fixed4 texcol = tex2D (_MainTex, i.texcoord);
			    fixed4 alpha = tex2D (_AlphaTex, i.texcoord);
				texcol.rgb *= _TintColor.rgb * i.color.rgb;
			    texcol.a = alpha.r * _TintColor.a * i.color.a;
			    return texcol;
			}
			ENDCG
	    }
	}
}
