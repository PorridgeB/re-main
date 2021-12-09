using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class Thread : MonoBehaviour
{
    [SerializeField]
    private TextAsset asset;
    private Story story;
    private string currentKnot = "main";

    public bool locked;

    private int progress = -1;

    private void Awake()
    {
        InitializeStory();
    }

    private void InitializeStory()
    {
        LoadNewInk(asset);
        if (story.globalTags[0].Contains("LOCKED:"))
        {
            locked = true;
        }
    }

    private void LoadNewInk(TextAsset newFile)
    {
        story = new Story(newFile.text);
    }

    public void Unlock()
    {
        locked = false;
    }

    public void WriteVariables()
    {
    }

    public List<string> GetThreadTags()
    {
        return story.globalTags;
    }

    public void Progress()
    {
        progress++;
    }

    public Story GetCurrentStory()
    {
        if (!story.canContinue)
        {
            foreach(string s in story.currentTags)
            {
                if (s.Contains("RESUME"))
                {

                    var name  = s.Replace("RESUME:", "").Trim();
                    story.ChoosePathString(currentKnot + "." + name);
                }
            }
        }
        return story;
    }
}
