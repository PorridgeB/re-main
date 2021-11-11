using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModuleScrollMenuItem : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private TMP_Text name;
    [SerializeField]
    private TMP_Text bonuses;

    public void SetInformation(string _name, string _bonuses, Sprite sprite)
    {
        image.sprite = sprite;
        name.text = _name;
        bonuses.text = _bonuses;
    }
}
