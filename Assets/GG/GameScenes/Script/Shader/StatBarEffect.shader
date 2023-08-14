Shader "UI/StatBarEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }


        CGINCLUDE
    #include "UnityCG.cginc"

    float g_fLerpRatio;
    float4 g_vOriginColor;
    float4 g_vColor;

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

    fixed4 frag(v2f i) : SV_Target
    {
        fixed4 col = tex2D(_MainTex, i.uv);

        col *= g_vColor;
        if (i.uv.x > g_fLerpRatio)
            discard;
        

        return col;
    }

    ENDCG
    
        SubShader
    {
        blend SrcAlpha OneMinusSrcAlpha
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            ENDCG
        }
    }
}
