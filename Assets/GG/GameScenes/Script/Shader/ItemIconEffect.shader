Shader "Universal Render Pipeline/UI/ItemIconEffect"
//{
//    Properties
//    {
//        _MainTex ("Texture", 2D) = "white" {}
//    }
//    
//        CGINCLUDE            
//    #include "UnityCG.cginc"
//
//    sampler2D _MainTex;
//    sampler2D _CoolTimeMaskingTex;
//    float g_fLerpRatio;
//    float g_fHighlightingRatio;
//    float4 g_vOriginColor;
//    float4 g_vColor;
//
//            struct appdata
//            {
//                float4 vertex : POSITION;
//                float2 uv : TEXCOORD0;
//            };
//
//            struct v2f
//            {
//                float2 uv : TEXCOORD0;
//                float4 vertex : SV_POSITION;
//            };
//
//            v2f vert (appdata v)
//            {
//                v2f o;
//                o.vertex = UnityObjectToClipPos(v.vertex);
//                o.uv = v.uv;
//                return o;
//            }
//
//            
//
//            fixed4 frag (v2f i) : SV_Target
//            {
//                fixed4 col = tex2D(_MainTex, i.uv);
//                half CoolTimeMasking = tex2D(_CoolTimeMaskingTex, i.uv).r;
//                
//                col.rgb *= (CoolTimeMasking > g_fLerpRatio ? 0.5f : 1.f);
//                col.rgb = col.rgb + 0.5f * g_fHighlightingRatio;
//
//                return col;
//            }
//
//                ENDCG
//
//    SubShader
//    {
//
//        blend SrcAlpha OneMinusSrcAlpha
//        Cull Off ZWrite Off ZTest Always
//       
//            Pass
//        {
//            CGPROGRAM
//            #pragma vertex vert
//            #pragma fragment frag
//
//            ENDCG
//        }
//    }
//}
{
    // The properties block of the Unity shader. In this example this block is empty
    // because the output color is predefined in the fragment shader code.
    Properties
    {
         _MainTex("Texture", 2D) = "white" {}
    }

        // The SubShader block containing the Shader code. 
        SubShader
    {
        // SubShader Tags define when and under which conditions a SubShader block or
        // a pass is executed.
        Tags { "RenderType" = "Transparent" "RenderPipeline" = "UniversalRenderPipeline" }

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off 
        ZWrite Off 
        ZTest Always

        Pass
        {
            // The HLSL code block. Unity SRP uses the HLSL language.
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

    sampler2D _MainTex;
    sampler2D _CoolTimeMaskingTex;
    float g_fLerpRatio;
    float g_fHighlightingRatio;
    float4 g_vOriginColor;
    float4 g_vColor;
   
    v2f vert(Attributes IN)
    {
        // Declaring the output object (OUT) with the Varyings struct.
        v2f OUT;
        // The TransformObjectToHClip function transforms vertex positions
        // from object space to homogenous space
        OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
        // Returning the output.

        OUT.uv = IN.uv;

        return OUT;
    }

    // The fragment shader definition.            
    half4 frag(v2f i) : SV_Target
    {
        // Defining the color variable and returning it.
        half4 col = tex2D(_MainTex, i.uv);
        half CoolTimeMasking = tex2D(_CoolTimeMaskingTex, i.uv).r;
        
        col.rgb *= (CoolTimeMasking > g_fLerpRatio ? 0.5f : 1.f);
        col.rgb = col.rgb + 0.5f * g_fHighlightingRatio;
    
        return col;
    }
    ENDHLSL
}
    }
}

