using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatScrollMenuItem : MonoBehaviour
{
    [SerializeField]
    private TMP_Text name;
    [SerializeField]
    private TMP_Text value;

    public void SetInformation(string _name, string _value)
    {
        name.text = _name;
        value.text = _value;
    }
}
