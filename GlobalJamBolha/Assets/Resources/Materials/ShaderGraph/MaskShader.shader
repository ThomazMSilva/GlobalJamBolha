Shader "Custom/MaskShader"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB) Gloss (A)", 2D) = "White"{}
    }

    Category
    {
        
        SubShader
        {
            Tags { "Queue"="Transparent+1"}
            Pass
            {
                ZWrite On
                ZTest Greater
                Lighting On
                SetTexture [_MainTex] {}
            }
        }
    }
    Fallback "Specular",1
}
