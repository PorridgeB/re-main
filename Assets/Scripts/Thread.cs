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

[CreateAssetMenu]
public class Thread : ScriptableObject,  IListenGameEvent
{
    [SerializeField]
    private TextAsset asset;
    public Story story;
    public ThreadPriority priority;
    private string currentKnot = "main";

    public bool locked;
    public bool lockUntilEndOfScene;

    [SerializeField]
    private int progress = 0;

    [SerializeField]
    private List<GameEvent> listenTo = new List<GameEvent>();
    [SerializeField]
    private List<GameEvent> invoke = new List<GameEvent>();

    public bool Initialize()
    {
        if (story == null)
        {
            InitializeStory();
            OnEnable();
            return true;
        }
        return false;
        
    }
    public void OnEventRaised(GameEvent gameEvent)
    {
        Debug.Log(name + " : " + gameEvent.name);
        if (listenTo.Count <= progress) return;
        if (gameEvent == listenTo[progress])
        {
            if (locked)
            {
                if (locked)
                {
                    Debug.Log("unlocking " + name);
                    locked = false;
                    return;
                }
            }
            else
            {
                Progress();
            }
            
        }
    }
    public void OnEventRaised(GameEvent gameEvent, GameObject gameObject)
    {
        OnEventRaised(gameEvent);
    }
    public void OnEventRaised(GameEvent gameEvent, DamageSource damageSource)
    {
        OnEventRaised(gameEvent);
    }
    //used for debugging
    private void EditorReset()
    {
        progress = 0;
        currentKnot = "main";
    }

    private void InitializeStory()
    {
        LoadNewInk(asset);
        //EditorReset();
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
        priority = (ThreadPriority)(int)story.variablesState["priority"];
        story.ObserveVariable("priority", (string str, object newValue) => { UpdatePriority((int)newValue); });
        
    }

    private void OnEnable()
    {
        foreach (GameEvent g in listenTo)
        {
            g.RegisterListener(this);
        }
    }

    private void UpdatePriority(int newValue)
    {
        priority = (ThreadPriority)newValue;
    }

    private void Invoke(string name)
    {
        foreach (GameEvent g in invoke)
        {
            if (g.name == name)
            {
                Debug.Log("Invoking event " + name);
                g.Raise();
            }
        }
    }

    private void LoadNewInk(TextAsset newFile)
    {
        story = new Story(newFile.text);
    }

    public void CheckTags()
    {
        foreach (string t in story.currentTags)
        {
            Debug.Log(t);
            if (t.Contains("EVENT"))
            {
                string s = t.Replace("EVENT:", "").Trim();
                Invoke(s);
            }
            else if (t.Contains("NEXT"))
            {
                string s = t.Replace("NEXT:", "").Trim();
                currentKnot = s;
                Debug.Log(currentKnot);
                story.ChoosePathString(currentKnot);
                lockUntilEndOfScene = true;
            }
        }
    }

    public void Progress()
    {
        Debug.Log(story);
        List<string> tags = story.TagsForContentAtPath(currentKnot);
        if (tags[0].Contains("LOCKED")) tags.RemoveAt(0);
        if (tags[0].Contains("FIRST")) tags.RemoveAt(0);
        Debug.Log(tags.Count);
        if (tags.Count > 0)
        {
            Debug.Log("progressing " + name);
            currentKnot = story.TagsForContentAtPath(currentKnot)[0].Split(':')[1].Trim();

            Debug.Log(currentKnot);
            story.ChoosePathString(currentKnot);
                
            progress++;
        }
    }

    public bool Complete
    {
        get
        {
            return !story.canContinue && story.currentTags.Count == 0;
        }
        
    }

    public string Name
    {
        get
        {
            return asset.name;
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
                    Debug.Log(currentKnot + "." + name);
                    story.ChoosePathString(currentKnot + "." + name);
                }
            }
        }
        return story;
    }
}
