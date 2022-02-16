using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomMeshOptions", menuName = "Room Mesh Options", order = 1)]
public class RoomMeshOptions : ScriptableObject
{
    [Tooltip("Add the hidden back and side faces to the walls")]
    public bool AddHiddenWallFaces = true;
    [Tooltip("Bake fake ambient occlusion into the mesh vertex colors")]
    public bool BakeAmbientOcclusion = true;
    [Tooltip("Use the ambient occlusion color when there is at least one occluder")]
    public bool SimpleAmbientOcclusion = false;
    [Tooltip("The tint of the ambient occlusion")]
    public Color AmbientOcclusionColor = new Color(0.3f, 0.3f, 0.4f);

    public Color CalculateAmbientOcclusion(params bool[] occluders)
    {
        if (SimpleAmbientOcclusion)
        {
            return occluders.Any(x => x) ? AmbientOcclusionColor : Color.white;
        }
        else
        {
            var t = Mathf.Pow((float)occluders.Average(x => x ? 1 : 0), 0.5f);
            return Color.Lerp(Color.white, AmbientOcclusionColor, t);
        }
    }
}
