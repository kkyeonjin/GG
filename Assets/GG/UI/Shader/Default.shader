Shader "Universal Render Pipeline/Unlit/DefaultShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Alpha("Alpha",Float) = 1
    }

        CGINCLUDE
#include "UnityCG.cginc"

        sampler2D _MainTex;
        half _Alpha;

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



    fixed4 frag(v2f i) : SV_Target
    {
        fixed4 col = tex2D(_MainTex, i.uv);
        col.a *= _Alpha;
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
