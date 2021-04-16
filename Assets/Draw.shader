Shader "Unlit/Draw"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 1000

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

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
            float4 underCol;
            float4 overCol;
            int pointsNum;
            Texture2D pointsT;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }
           
            float IsPtInsideFunc(float2 funcpt, float2 pt, float transitionHalfWidth)
            {
                return smoothstep(-funcpt.y - transitionHalfWidth, -funcpt.y + transitionHalfWidth, pt.y)
                * (1. - smoothstep(funcpt.y - transitionHalfWidth, funcpt.y + transitionHalfWidth, pt.y));
            }
            float2 GlobalToLocalPos(float2 origin, float angle, float2 globalPos)
            {
                float2 i = float2(cos(angle), sin(angle));
                float2 j = float2(-i.y, i.x);
                float2 vect = globalPos - origin;
                return float2(dot(i, vect), dot(j, vect));
            }
            float GetSignedDistanceToLine(float2 linePt, float2 lineUnitDir, float2 pt)
            {
                return dot(float2(-lineUnitDir.y, lineUnitDir.x), pt - linePt);
            }
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float2 uv = i.uv -0.5;
                
                //fixed4 col = fixed4(uv[1],uv[1],uv[1],1);
                // apply fog
                
                int index = (pointsNum-1) * i.uv[0];
                int index2 = index+1;
                if(index2 == pointsNum)
                    index2--;
                int2 id = int2(index, 0);
                int2 id2 = int2(index2, 0);
                float4 funcpt = pointsT[id];
                float4 funcpt2 = pointsT[id2];
                float currUV = ((float)index) / ((float)(pointsNum-1));
                float nextUV =  ((float)index2) / ((float)(pointsNum-1));
                float lerpFac = (i.uv[0]-currUV)/nextUV;
                //float4 c = pointsT[int2(pointsNum-1,0)];
                float2 pt = lerp(funcpt,funcpt2,1-lerpFac);//GlobalToLocalPos(funcpt,0,uv);
                
                fixed4 col = lerp(overCol, underCol, IsPtInsideFunc(pt,GlobalToLocalPos(pt,0,i.uv),0));
                //fixed4 col = c;
                    
                float2 rect = float2(.2,.1);
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
