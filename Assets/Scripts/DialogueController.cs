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
    private Story story;

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
        if (story != null)
        {
            dialogueUI.SetActive(inputs.currentActionMap.name == "DialogueControl");
            response.text = "";
            dialogue.text = story.currentText;
            if (story.canContinue)
            {
                if (continueAction.triggered)
                {
                    story.Continue();
                    foreach (string s in story.currentTags)
                    {
                        Debug.Log(s);
                    }
                    foreach (string s in story.variablesState)
                    {
                        Debug.Log(s);
                    }
                }
            }
            else
            {
                if (story.currentChoices.Count <= 0)
                {
                    if (continueAction.triggered)
                    {
                        //end dialogue.
                        inputs.SwitchCurrentActionMap("CharacterControl");
                    }
                }
                int index = 0;
                foreach (Choice choice in story.currentChoices)
                {
                    index++;
                    response.text += index + ". " + choice.text + "\n";
                }
                if (choiceA.triggered)
                {
                    MakeChoice(0);
                }
                else if (choiceB.triggered)
                {
                    MakeChoice(1);
                }
                else if (choiceC.triggered)
                {
                    MakeChoice(2);
                }
                else if (choiceD.triggered)
                {
                    MakeChoice(3);
                }
            }


            playerPortrait.SetActive(!story.canContinue);
            characterPortrait.SetActive(story.canContinue);
        }
        
    }

    public void MakeChoice(int index)
    {
        story.ChooseChoiceIndex(index);
        story.Continue();
    }

    public void SetStory(Story s)
    {
        story = s;
        story.Continue();
    }
}
