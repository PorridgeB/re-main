#ifndef VISIBILITY_INCLUDED
#define VISIBILITY_INCLUDED

void CalculateVisibilityMask_float(float3 Position, out float3 Color)
{
#ifndef SHADERGRAPH_PREVIEW
    Light light = GetAdditionalLight(0, Position, 1);
    Color = light.shadowAttenuation;
#else
    Color = 0;
#endif
}

#endif