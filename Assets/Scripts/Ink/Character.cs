using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class Character : MonoBehaviour
{
    [SerializeField]
    private DialogueController dialogueController;
    [SerializeField]
    private List<Thread> threads = new List<Thread>();
    [SerializeField]
    private List<Thread> available = new List<Thread>();

    private Queue<Thread> threadQueue = new Queue<Thread>();

    private void Start()
    {
        foreach (Thread t in GetComponentsInChildren<Thread>())
        {
            threads.Add(t);
            if (t.GetThreadTags()[0] == "FIRST")
            {
                available.Add(t);
            }
        }
    }

    public void Update()
    {
        foreach (Thread t in threads)
        {
            if (!t.locked)
            {
                if (!threadQueue.Contains(t))
                {
                    if (t.GetCurrentStory().canContinue)
                    {
                        threadQueue.Enqueue(t);
                    }
                    
                }
            }
        }
    }

    public void GetStory()
    {
        if (threadQueue.Count > 0)
        {
            dialogueController.SetStory(threadQueue.Dequeue().GetCurrentStory());
        }
        else
        {
            dialogueController.SetStory(available[0].GetCurrentStory());
        }
    }
}
