using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveProfile : MonoBehaviour
{
    public TextMeshProUGUI Text;

    private void Start()
    {
        Text.text = "Empty";

        if (Random.value < 0.5f)
        {
            return;
        }

        var totalTime = "00:00:00";
        var story = 0;
        var dataFragments = 0;
        var scrap = 0;

        Text.text = $"Total Time - {totalTime}\tStory - {story}%\n{dataFragments} <sprite=0>\t{scrap} <sprite=1>";
    }
}
