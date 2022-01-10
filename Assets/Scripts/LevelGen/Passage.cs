using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Passage : MonoBehaviour
{
    public ConnectionSide side;
    public bool connected;

    public Vector3 Offset {
        get{
            Vector3 t = transform.localPosition;
            return t;
        } 
    }
}
