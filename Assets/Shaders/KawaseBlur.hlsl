#ifndef KAWASE_BLUR_INCLUDED
#define KAWASE_BLUR_INCLUDED

void CalculateKawaseBlur_float(float Offset, UnityTexture2D Texture, float2 UV, out float3 Color)
{
    float2 res = Texture.texelSize.xy;
    
    Color = tex2D(Texture, UV);
    Color += tex2D(Texture, UV + float2(Offset, Offset) * res);
    Color += tex2D(Texture, UV + float2(Offset, -Offset) * res);
    Color += tex2D(Texture, UV + float2(-Offset, Offset) * res);
    Color += tex2D(Texture, UV + float2(-Offset, -Offset) * res);
    Color /= 5.0f;
}

#endif