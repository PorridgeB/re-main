using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesBar : MonoBehaviour
{
    public Counter DataFragmentsCounter;
    public Counter ScrapCounter;

    private void Start()
    {
        DataFragmentsCounter.SetValue(0);
        ScrapCounter.SetValue(0);

        //var rectTrans = GetComponent<RectTransform>();
        //LeanTween.moveY(rectTrans, rectTrans.rect.height, 1).setEaseInOutExpo().setLoopPingPong();
    }
    
    private void Update()
    {
    }
}
