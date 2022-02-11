using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Ink.Runtime;
using TMPro;

public class DialogueController : MonoBehaviour
{
    [SerializeField]
    private TextAsset file;
    private Thread thread;

    private PlayerInput inputs;
    private InputAction continueAction;
    private InputAction backAction;
    private InputAction choiceA;
    private InputAction choiceB;
    private InputAction choiceC;
    private InputAction choiceD;

    [SerializeField]
    private GameObject dialogueUI;
    [SerializeField]
    private GameObject playerPortrait;
    [SerializeField]
    private GameObject characterPortrait;
    [SerializeField]
    private TMP_Text dialogue;
    [SerializeField]
    private TMP_Text response;

    // Start is called before the first frame update
    void Start()
    {
        inputs = PlayerController.instance.GetComponent<PlayerInput>();
        continueAction = inputs.actions["Continue"];
        backAction = inputs.actions["Back"];
        choiceA = inputs.actions["ChoiceA"];
        choiceB = inputs.actions["ChoiceB"];
        choiceC = inputs.actions["ChoiceC"];
        choiceD = inputs.actions["ChoiceD"];


    }



    // Update is called once per frame
    void Update()
    {
        if (thread != null)
        {
            dialogueUI.SetActive(inputs.currentActionMap.name == "DialogueControl");
            response.text = "";
            dialogue.text = thread.story.currentText;
            if (thread.story.canContinue)
            {
                if (continueAction.triggered)
                {
                    
                    thread.story.Continue();
                    thread.CheckTags();
                }
            }
            else
            {
                if (thread.story.currentChoices.Count <= 0)
                {
                    if (continueAction.triggered)
                    {
                        //end dialogue.
                        inputs.SwitchCurrentActionMap("CharacterControl");
                    }
                }
                int index = 0;
                foreach (Choice choice in thread.story.currentChoices)
                {
                    index++;
                    response.text += index + ". " + choice.text + "\n";
                }
                if (choiceA.triggered)
                {
                    MakeChoice(0);
                    thread.CheckTags();
                }
                else if (choiceB.triggered)
                {
                    MakeChoice(1);
                    thread.CheckTags();
                }
                else if (choiceC.triggered)
                {
                    MakeChoice(2);
                    thread.CheckTags();
                }
                else if (choiceD.triggered)
                {
                    MakeChoice(3);
                    thread.CheckTags();
                }
            }


            playerPortrait.SetActive(!thread.story.canContinue);
            characterPortrait.SetActive(thread.story.canContinue);
        }
        
    }

    public void MakeChoice(int index)
    {
        thread.story.ChooseChoiceIndex(index);
        thread.story.Continue();
    }

    public void SetThread(Thread t)
    {
        thread = t;
        if (thread.story.canContinue)
        {
            thread.story.Continue();
        }
        
    }
}
