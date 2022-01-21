using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveProfile : MonoBehaviour
{
    public TextMeshProUGUI Text;

    private void Start()
    {
        Text.text = "New Save Profile";

        var totalTime = 0;
        var story = 0;
        var dataFragments = 0;
        var scrap = 0;

        Text.text = $"Total Time - ";
    }
}
