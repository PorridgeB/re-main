using System.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEditor;

public class MechanicStore : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scrap;

    private void Start()
    {
        scrap.text = $"{SaveManager.Instance.Save.Scrap} <sprite=1>";
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
