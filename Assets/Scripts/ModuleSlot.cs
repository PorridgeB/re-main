using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModuleSlot : MonoBehaviour
{
    [SerializeField]
    private List<Color> rarityColors;

    [SerializeField]
    private Image icon;
    [SerializeField]
    private TextMeshProUGUI quantity;
    [SerializeField]
    private Image border;

    public Module module;
    public void SetUp(Module m)
    {
        module = m;
        icon.sprite = m.sprite;
        quantity.text = m.count.ToString();
        border.color = rarityColors[(int)m.rarity];

    }
}
