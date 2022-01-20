using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesBar : MonoBehaviour
{
    public int DataFragments = 31;
    public int Scrap = 22;

    private void Start()
    {
        var rectTrans = GetComponent<RectTransform>();

        LeanTween.moveY(rectTrans, rectTrans.rect.height, 1).setEaseInOutExpo().setLoopPingPong();
    }
}
