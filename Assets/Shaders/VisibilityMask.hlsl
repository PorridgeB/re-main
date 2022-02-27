#ifndef VISIBILITY_INCLUDED
#define VISIBILITY_INCLUDED

void CalculateVisibilityMask_float(float3 Position, out float3 Color)
{
    Color = 0;
#ifndef SHADERGRAPH_PREVIEW
    uint additionalLightsCount = GetAdditionalLightsCount();
    for (uint i = 0; i < additionalLightsCount; i++)
    {
        Light light = GetAdditionalLight(i, Position, 1);
        //if (light.layerMask == (1 << 6))
        if (true)
        {
            Color = light.shadowAttenuation;
            break;
        }
    }
#endif
}

#endif