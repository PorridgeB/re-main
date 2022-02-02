using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public Camera Camera;
    public Transform Object;

    private void Update()
    {
        var screenPoint = Camera.WorldToScreenPoint(Object.transform.position + new Vector3(0, 0.5f, 0));

        var rectTrans = GetComponent<RectTransform>();
        rectTrans.anchoredPosition = screenPoint;
    }
}
