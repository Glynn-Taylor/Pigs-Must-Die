Shader "Foam" {
	Properties {
		_Offset ("Offset", Range (0.00,1.00)) = 1.000
		_Color ("Main Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Cutout ("Mask (A)", 2D) = "white" {}
		_Cutoff ("Alpha cutoff", Range (0,1)) = 0.5
	}
		//ZTest Less | Greater | LEqual | GEqual | Equal | NotEqual | Always
	SubShader {
		 Ztest Less

    Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}

    LOD 20
		
		ZWrite on
		Offset -1, -1
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask RGB
		 
		Pass {
			ZWrite Off
			AlphaTest Greater [_Cutoff]
			Color [_Color]
			SetTexture [_MainTex] {constantColor[_Color] combine texture * primary double, texture * constant double}
			SetTexture [_Cutout] { combine previous, previous * texture }	
		}
	}
	
	Fallback "VertexLit"
}
