Shader "Custom/URPUnlitOverlay" {
    Properties {
        _MainTex ("Sprite", 2D) = "white" {}
        _Color ("Tint Color", Color) = (1, 1, 1, 1)
        _OverlayColor ("Overlay Color", Color) = (1, 1, 1, 1)
    }
 
    SubShader {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        LOD 100
 
        Pass {
            Cull Off
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            Lighting Off
            Stencil {
                Ref 1
                Comp Always
                Pass Replace
            }
 
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
 
            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
 
            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
 
            sampler2D _MainTex;
            float4 _Color;
            float4 _OverlayColor;
 
            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
 
            fixed4 frag (v2f i) : SV_Target {
                // Sample the sprite texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // Apply the tint color
                col.rgb *= _Color.rgb;
                // Apply the overlay color
                col.rgb += _OverlayColor.rgb;
                return col;
            }
            ENDCG
        }
    }
}