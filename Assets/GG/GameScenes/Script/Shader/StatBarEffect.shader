Shader "Universal Render Pipeline/UI/StatBarEffect"
//{
//    Properties
//    {
//        _MainTex ("Texture", 2D) = "white" {}
//        _MaskingTex("Texture",2D) = "white" {}
//    }
//
//
//        CGINCLUDE
//    #include "UnityCG.cginc"
//
//    float g_fLerpRatio;
//    float4 g_vOriginColor;
//    float4 g_vColor;
//
//    struct appdata
//    {
//        float4 vertex : POSITION;
//        float2 uv : TEXCOORD0;
//    };
//
//    struct v2f
//    {
//        float2 uv : TEXCOORD0;
//        float4 vertex : SV_POSITION;
//    };
//
//    v2f vert(appdata v)
//    {
//        v2f o;
//        o.vertex = UnityObjectToClipPos(v.vertex);
//        o.uv = v.uv;
//        return o;
//    }
//
//    sampler2D _MainTex;
//    sampler2D _MaskingTex;
//
//    fixed4 frag(v2f i) : SV_Target
//    {
//        fixed4 col = tex2D(_MainTex, i.uv);
//
//        col = g_vColor;
//        if (i.uv.x > g_fLerpRatio)
//        {
//            col = g_vOriginColor;
//        }
//
//        return col;
//    }
//
//    ENDCG
//    
//        SubShader
//    {
//        blend SrcAlpha OneMinusSrcAlpha
//        Cull Off ZWrite Off ZTest Always
//
//        Pass
//        {
//            CGPROGRAM
//            #pragma vertex vert
//            #pragma fragment frag
//            ENDCG
//        }
//    }
//}

{
    Properties
    {
         _MainTex("Texture", 2D) = "white" {}
    }

        SubShader
    {
         Tags { "RenderType" = "Transparent" "RenderPipeline" = "UniversalRenderPipeline" }

         Blend SrcAlpha OneMinusSrcAlpha
         Cull Off 
         ZWrite Off 
         ZTest Always

        Pass
        {
            HLSLPROGRAM
            // This line defines the name of the vertex shader. 
            #pragma vertex vert
            // This line defines the name of the fragment shader. 
            #pragma fragment frag

            // The Core.hlsl file contains definitions of frequently used HLSL
            // macros and functions, and also contains #include references to other
            // HLSL files (for example, Common.hlsl, SpaceTransforms.hlsl, etc.).
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"            

            // The structure definition defines which variables it contains.
            // This example uses the Attributes structure as an input structure in
            // the vertex shader.
    struct Attributes
    {
        // The positionOS variable contains the vertex positions in object
        // space.
        float4 positionOS   : POSITION;
        float2 uv : TEXCOORD0;
    };

    struct v2f
    {
        // The positions in this struct must have the SV_POSITION semantic.
        float4 positionHCS  : SV_POSITION;
        float2 uv : TEXCOORD0;
    };

    float g_fLerpRatio;
    float4 g_vOriginColor;
    float4 g_vColor;

    sampler2D _MainTex;
    sampler2D _MaskingTex;

    // The vertex shader definition with properties defined in the Varyings 
    // structure. The type of the vert function must match the type (struct)
    // that it returns.
    v2f vert(Attributes IN)
    {
        v2f OUT;
        OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);

        OUT.uv = IN.uv;

        return OUT;
    }

    // The fragment shader definition.            
    half4 frag(v2f i) : SV_Target
    {
        half4 col = tex2D(_MainTex, i.uv);

        //col = g_vColor;
        if (i.uv.x > g_fLerpRatio)
        {
            col = g_vOriginColor;
        }

        return col;
    }ENDHLSL
}
    }
}