using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveProfile : MonoBehaviour
{
    public bool Interactable
    {
        set
        {
            var button = GetComponent<Button>();
            button.interactable = value;

            contents.color = value ? new Color(0, 0.5843138f, 0.9137255f) : Color.gray;
        }
    }

    [SerializeReference]
    public Save Save;

    [SerializeField]
    private TextMeshProUGUI contents;

    private void Start()
    {
        Refresh();
    }

    private string GetText()
    {
        if (Save == null)
        {
            return "Empty";
        }

        var totalTime = TimeSpan.FromSeconds(Save.TotalTime).ToString(@"hh\:mm\:ss");
        var story = Save.StoryCompletion;
        var dataFragments = Save.DataFragments;
        var scrap = Save.Scrap;

        return $"Total Time - {totalTime}\tStory - {story}%\n{dataFragments} <sprite=0>\t{scrap} <sprite=1>";
    }

    public void Refresh()
    {
        contents.text = GetText();
    }

    public void Select()
    {
        SendMessageUpwards("OnSaveProfileSelected", this);
    }
}
