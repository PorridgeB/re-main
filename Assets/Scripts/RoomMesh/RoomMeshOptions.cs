using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RoomMeshOptions
{
    public bool AddHiddenWallFaces = true;
    public bool AddAmbientOcclusion = true;
    public Color AmbientOcclusionColor = new Color(0.4f, 0.4f, 0.5f);
}
