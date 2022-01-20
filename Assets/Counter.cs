using System.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public int MaxDigits = 3;

    public void SetValue(int value)
    {
        var digits = Mathf.Clamp(value, 0, Mathf.Pow(10, MaxDigits) - 1).ToString();
        
        var textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        textMeshProUGUI.text = string.Join("", digits.Select(x => $"<sprite index={x}>"));
    }
}
