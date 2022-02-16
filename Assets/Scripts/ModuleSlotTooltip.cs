using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ModuleSlotTooltip : MonoBehaviour
{
    [SerializeField]
    private TMP_Text name;
    [SerializeField]
    private TMP_Text description;

    public int offsetY;

    private void OnEnable()
    {
        var offsetX = 35;
        Module m = transform.parent.GetComponent<ModuleSlot>().module;
        name.text = m.name;
        description.text = m.description;
        transform.localPosition = new Vector3(offsetX, offsetY, 0);
    }
}
