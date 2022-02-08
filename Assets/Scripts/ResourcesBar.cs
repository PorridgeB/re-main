using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesBar : MonoBehaviour
{
    [SerializeField]
    private Counter dataFragmentsCounter;
    [SerializeField]
    private Counter scrapCounter;
    [SerializeField]
    private RunInfoHistory runInfoHistory;

    private void Start()
    {
        dataFragmentsCounter.SetValue(0);
        scrapCounter.SetValue(0);

        //var rectTrans = GetComponent<RectTransform>();
        //LeanTween.moveY(rectTrans, rectTrans.rect.height, 1).setEaseInOutExpo().setLoopPingPong();
    }
    
    private void Update()
    {
        if (runInfoHistory.Current == null)
        {
            return;
        }

        dataFragmentsCounter.SetValue(runInfoHistory.Current.dataFragments);
        scrapCounter.SetValue(runInfoHistory.Current.scrap);
    }
}
