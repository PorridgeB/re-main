using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class Thread : MonoBehaviour
{
    [SerializeField]
    private List<TextAsset> assets;
    private List<Story> stories = new List<Story>();
    private List<List<GameEventListener>> events = new List<List<GameEventListener>>();

    private int progress = -1;

    private void Awake()
    {
        InitializeStories();
    }

    private void InitializeStories()
    {
        foreach (TextAsset t in assets)
        {
            LoadNewInk(t);
        }
    }

    private void LoadNewInk(TextAsset newFile)
    {
        stories.Add(new Story(newFile.text));
    }

    public bool EndOfThread()
    {
        return progress >= stories.Count -1;
    }

    public void WriteVariables()
    {
    }

    public List<string> GetThreadTags()
    {
        return stories[0].globalTags;
    }

    public void Progress()
    {
        progress++;
    }

    public Story GetCurrentStory()
    {
        return stories[progress];
    }
}
