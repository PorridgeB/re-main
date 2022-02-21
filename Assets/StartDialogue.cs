using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Kinda does what character does but simple
public class StartDialogue : MonoBehaviour
{
    //[SerializeField]
    //private Thread thread;
    [SerializeField]
    private DialogueHolder currentDialogue;
    [SerializeField]
    private GameEvent enterDialogue;

    public void StartThread(Thread thread)
    {
        currentDialogue.thread = thread;
        enterDialogue.Raise();
    }
}
