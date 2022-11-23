Shader "GI_Shaders/skeleton"
{
    Properties   
    {  
        _MainTex ("Texture", 2D) = "white" {}  
        _BumpMap ("Bumpmap", 2D) = "bump" {}  
        _RimColor ("Rim Color", Color) = (0.17,0.36,0.81,0.0)  
        _RimPower ("Rim Power", Range(0.6,9.0)) = 1.0  
        _IlluminCol ("auto-lightingEmission(RGB)", Color) = (1,1,1,1)
    }  
  
    SubShader   
    {  
        Tags { "RenderType" = "Opaque" }  
      
  
        CGPROGRAM  
        // use for the configuration for lambert rim color 
        #pragma surface surf Lambert  
          
        struct Input   
        {  
            float2 uv_MainTex;
            float2 uv_BumpMap;  
            float3 viewDir;  
        };  
  
        sampler2D _MainTex;  
        sampler2D _BumpMap;  
        float4 _RimColor;  
        float _RimPower;  
  
        void surf (Input IN, inout SurfaceOutput o)  
        {  
            o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;  
            o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));  
            half rim = 1.0 - saturate(dot (normalize(IN.viewDir), o.Normal));  
            o.Emission = _RimColor.rgb * pow (rim, _RimPower);  
        }  
  
        ENDCG  
    }   
  
    Fallback "Diffuse"  
}
