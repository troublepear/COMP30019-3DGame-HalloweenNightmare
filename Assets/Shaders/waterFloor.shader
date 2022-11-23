Shader "GI_Shaders/waterFloor" {
	Properties {
		[HDR]_ColorD ("Deep Color", Color) = (0.1,0.3,0.5,0.7)
		[HDR]_ColorS ("Steep Color", Color) = (0.7,0.9,1.0,1.0)
		_MainTex ("Main Tex", 2D) = "white" {}
		_BlendTex ("Blend Tex", range(0,1)) = 0.5
	}
	SubShader {
		Tags { 
			"Queue"="Transparent" 
			"RenderType"="Transparent" }
		LOD 400
		
		CGPROGRAM
		#pragma surface surf BlinnPhong alpha
		#pragma target 3.0

		sampler2D _MainTex;
		float4 _ColorD;
		float4 _ColorS;
		float _BlendTex;

		struct Input {
			float3 worldNormal; INTERNAL_DATA
			float3 worldPosCordinates;
			float2 uv_MainTex : TEXCOORD0;
			float3 viewDir;
		};

		// surf computatian usage of the
		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D(_MainTex, IN.uv_MainTex);
			float vDotn = dot(normalize(IN.viewDir), normalize(IN.worldNormal));
			float4 colorBlend = (_ColorD*vDotn)+(_ColorS*(1.0-vDotn));
			o.Albedo = (1-_BlendTex)*c.rgb + _BlendTex*colorBlend.rgb;
			o.Alpha = colorBlend.a;
		}
		ENDCG
	} 
	// if the cg color is not used then we can use Diffuse rendering as a fallback 
	FallBack "Diffuse"
}
