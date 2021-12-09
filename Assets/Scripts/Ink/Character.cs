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
        foreach (Thread t in GetComponents<Thread>())
        {
            threads.Add(t);
            if (t.GetThreadTags()[0] == "FIRST")
            {
                available.Add(t);
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
            if (available[0].EndOfThread())
            {
                available.RemoveAt(0);
            }
            dialogueController.SetStory(available[0].GetCurrentStory());
        }
    }
}
