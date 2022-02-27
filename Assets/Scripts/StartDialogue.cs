using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Kinda does what character does but simple
public class StartDialogue : MonoBehaviour
{
    //[SerializeField]
    //private Thread thread;
    [SerializeField]
    private CurrentCharacter currentCharacter;
    [SerializeField]
    private GameEvent enterDialogue;

    public void StartThread(Character character)
    {
        currentCharacter.character = character;
        enterDialogue.Raise();

    }
}
