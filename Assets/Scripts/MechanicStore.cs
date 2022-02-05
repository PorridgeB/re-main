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
        
    }

    public void Close()
    {
        Destroy(gameObject);
    }
}
