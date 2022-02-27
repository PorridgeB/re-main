using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

[CreateAssetMenu]
public class Character : ScriptableObject
{
    [SerializeField]
    private GameEvent enterDialogue;
    [SerializeField]
    private CurrentCharacter currentCharacter;
    [SerializeField]
    private Thread shopDialogue;

    [SerializeField]
    private List<Thread> threads = new List<Thread>();
    [SerializeField]
    private List<Thread> available = new List<Thread>();
    [SerializeField]
    private List<Thread> priority = new List<Thread>();
    private Thread currentThread;

    public Thread CurrentThread => currentThread;
    public void Initialize()
    {
        foreach (Thread thread in threads)
        {
            if (thread.Initialize())
            {
                if (thread.GetThreadTags()[0] == "FIRST")
                {
                    priority.Add(thread);
                    currentThread = thread;
                }
                if (!thread.locked)
                {
                    available.Add(thread);
                }
            }
        }
    }

    public void GetStory()
    {
        if (currentThread != null)
        {
            available.Remove(currentThread);

            if (!currentThread.Complete && !currentThread.lockUntilEndOfScene)
            {
                available.Insert(available.Count, currentThread);
            }
        }

        if (available.Count <= 0) return;
        
        if (priority.Count > 0)
        {
            currentThread = priority[0];
            priority.RemoveAt(0);
        }
        else
        {
            currentThread = available[0];
            foreach (Thread t in available)
            {
                if (t.priority > currentThread.priority)
                {
                    currentThread = t;
                }
            }
            
        }
        Debug.Log(currentThread);
        currentCharacter.character = this;
        enterDialogue.Raise();
    }
}
