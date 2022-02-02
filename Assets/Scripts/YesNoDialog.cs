using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class YesNoDialog : MonoBehaviour
{
    public string Prompt
    {
        get => prompt.text;
        set => prompt.text = value;
    }

    public delegate void Callback();

    public Callback OnYes;
    public Callback OnNo;

    [SerializeField]
    private TextMeshProUGUI prompt;

    public void Yes()
    {
        OnYes?.Invoke();

        Destroy(gameObject);
    }

    public void No()
    {
        OnNo?.Invoke();

        Destroy(gameObject);
    }
}
