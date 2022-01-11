using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Passage : MonoBehaviour
{
    public ConnectionSide side;
    public Room connection;

    public bool Connected{
        get{
            return connection != null;
        }
    }
    public Vector3 Offset {
        get{
            Vector3 t = transform.localPosition;
            return t;
        } 
    }
}
