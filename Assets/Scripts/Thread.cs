using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public enum ThreadPriority
{
    LOW,
    STANDARD,
    HIGH,
    ESSENTIAL 
}
[System.Serializable]
public class Thread : MonoBehaviour
{
    [SerializeField]
    private TextAsset asset;
    private Story story;
    public ThreadPriority priority;
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
        if (story.globalTags != null)
        {
            foreach (string s in story.globalTags)
            {
                if (s.Contains("LOCKED:"))
                {
                    locked = true;
                }

            }
        }
        Debug.Log((int)story.variablesState["priority"]);
        priority = (ThreadPriority)(int)story.variablesState["priority"];
        story.ObserveVariable("priority", (string str, object newValue) => { UpdatePriority((int)newValue); });
    }

    private void UpdatePriority(int newValue)
    {
        priority = (ThreadPriority)newValue;
    }

    private void LoadNewInk(TextAsset newFile)
    {
        story = new Story(newFile.text);
    }

    public void UnlockThread()
    {
        locked = false;
        GetComponentInParent<Character>().AddToPriority(this);
    }

    public void Progress()
    {
        if (!story.canContinue)
        {
            if (story.TagsForContentAtPath(currentKnot).Count > 0)
            {
                currentKnot = story.TagsForContentAtPath(currentKnot)[0].Split(':')[1].Trim();
            }
        }
    }

    public bool Complete
    {
        get
        {
            foreach (string s in story.currentTags)
            {
                Debug.Log(s);
            }
            return !story.canContinue && story.currentTags.Count == 0;
        }
        
    }

    public bool Waiting
    {
        get
        {
            return !story.canContinue && story.currentTags.Count > 0;
        }
    }

    public List<string> GetThreadTags()
    {
        return story.globalTags != null ? story.globalTags : new List<string>() { "Empty" };
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
