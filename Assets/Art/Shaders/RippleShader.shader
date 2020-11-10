Shader "Custom/RippleShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _DispTex("Displacement", 2D) = "grey" {}
		_Transparency("Transparency", float) = 0.25
		_DisplaceAmount("DisplaceAmount", float) = 0.25
    }
    Subshader
    {
        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
        }
 
        Cull off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
 
        Pass
        {
    CGPROGRAM
            #pragma vertex vert alpha
            #pragma fragment frag alpha
            #include "UnityCG.cginc"
 
            sampler2D _MainTex;
            sampler2D _DispTex;
 
            float4 _MainTex_ST;
            float4 _DispTex_ST;
			float _Transparency;
			float _DisplaceAmount;
 
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };
 



            v2f vert(float4 vertex:POSITION, float2 uv : TEXCOORD0)
            {
                v2f i;
                i.vertex = UnityObjectToClipPos(vertex);
                i.uv = TRANSFORM_TEX(uv, _MainTex);
                return i;
            }
 
            float4 frag(v2f IN) : COLOR
            {
                float2 uv = IN.uv.xy;
                float2 uvDisp = float2(0, uv.y);
                half4 disp_c = tex2D(_DispTex, uvDisp);
                uv.x += disp_c.r*_DisplaceAmount;
                float4 c;
				c.rgb = tex2D(_MainTex, uv).rgb;
				c.a = tex2D(_MainTex, uv).a * _Transparency;
                return c;
            }
    ENDCG
        }
    }
    FallBack "Diffuse"
}
 
