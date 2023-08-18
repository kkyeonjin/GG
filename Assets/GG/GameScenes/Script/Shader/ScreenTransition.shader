Shader "UI/ScreenTransition"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MaskTex("MaskTexture",2D) = "white" {}
    }

        CGINCLUDE


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

    v2f vert(appdata v)
    {
        v2f o;
        o.vertex = UnityObjectToClipPos(v.vertex);
        o.uv = v.uv;
        return o;
    }

    sampler2D _MainTex;
    Texture2D _MaskTex;
    SamplerState my_point_mirror_sampler;

    half g_fRatio;
    half4 g_vOriginColor;
    half4 g_vColor;

    fixed4 frag(v2f i) : SV_Target
    {
        float2 TexUV = i.uv;

        //TexUV.y *= 10.f;

        fixed4 col = tex2D(_MainTex, i.uv);
        half masking = _MaskTex.Sample(my_point_mirror_sampler, TexUV).x;
        
        if (masking > g_fRatio)
            discard;

        col.a = (g_fRatio - masking) / 0.2f;
        col.rgb = lerp(g_vColor.rgb, g_vOriginColor.rgb,col.a);//끝에 색 넣고 싶을 때


        return col;
    }
        ENDCG

    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always
        blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            ENDCG
        }
    }
}
