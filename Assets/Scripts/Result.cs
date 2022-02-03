using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Result : MonoBehaviour
{
    [SerializeField]
    private Image icon;
    [SerializeField]
    private TMP_Text title;
    [SerializeField]
    private TMP_Text score;

    public void SetValues(string name, string value)
    {
        title.text = name;
        score.text = value;
    }
}
