using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageDisplay : MonoBehaviour
{
    public Stage Stage;

    void Start()
    {
        var image = GetComponent<Image>();

        image.sprite = Stage?.Type.Icon;
    }
}
