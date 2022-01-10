using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Passage : MonoBehaviour
{
    public ConnectionSide side;

    public Vector3 Offset {
        get{
            Vector3 t = transform.localPosition;
            switch(side){
                case ConnectionSide.Top:
                case ConnectionSide.Bottom:
                    t.x = 0;
                    break;
                case ConnectionSide.Left:
                case ConnectionSide.Right:
                    t.z = 0;
                    break;
            }
            return t;
        } 
    }
}
