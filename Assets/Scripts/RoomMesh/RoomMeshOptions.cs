using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RoomMeshOptions
{
    [Tooltip("Add the hidden back and side faces to the walls")]
    public bool AddHiddenWallFaces = true;
    [Tooltip("Bake fake ambient occlusion into the mesh vertex colors")]
    public bool BakeAmbientOcclusion = true;
    [Tooltip("The tint of the ambient occlusion")]
    public Color AmbientOcclusionColor = new Color(0.4f, 0.4f, 0.5f);
}
