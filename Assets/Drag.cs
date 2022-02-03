using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Drag : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public SoftwareUpgrade SoftwareUpgrade;

    private Canvas parentCanvas;

    private void Start()
    {
        parentCanvas = GetComponentInParent<Canvas>();

        GetComponent<Image>().color = SoftwareUpgrade.Color;

        GetComponent<RectTransform>().sizeDelta = new Vector2(10 * SoftwareUpgrade.Lines, 10 * SoftwareUpgrade.Rings);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 movePos;

        if (parentCanvas == null)
        {
            return;
        }

        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, Mouse.current.position.ReadValue(), parentCanvas.worldCamera, out movePos);
        transform.position = parentCanvas.transform.TransformPoint(movePos);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(gameObject);
    }
}
