using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Ink.Runtime;

public class Dialogue : MonoBehaviour
{
    [SerializeField]
    private DialogueHolder currentDialogue;
    public float revealSpeed = 14; // characters per second

    private string CurrentText => currentDialogue.thread.story.currentText;
    private Thread Thread => currentDialogue.thread;

    [SerializeField]
    private TextMeshProUGUI dialogue;
    [SerializeField]
    private Image portrait;
    [SerializeField]
    private AudioSource soundEffects;
    [SerializeField]
    private AudioClip speak;
    private int visibleCharacters = 0;
    private float revealTimer = 0;

    private PlayerInput inputs;
    private InputAction continueAction;
    private InputAction backAction;
    private InputAction choiceA;
    private InputAction choiceB;
    private InputAction choiceC;
    private InputAction choiceD;

    [SerializeField]
    private Transform buttonContainer;

    [SerializeField]
    private List<GameObject> buttons;
    [SerializeField]
    private GameObject buttonPrefab;

    private bool FinishedRevealing => visibleCharacters >= CurrentText.Length;
    private void Start()
    {
        inputs = PlayerController.instance.GetComponent<PlayerInput>();
        continueAction = inputs.actions["Continue"];
        backAction = inputs.actions["Back"];
        choiceA = inputs.actions["ChoiceA"];
        choiceB = inputs.actions["ChoiceB"];
        choiceC = inputs.actions["ChoiceC"];
        choiceD = inputs.actions["ChoiceD"];

        visibleCharacters = 0;
        revealTimer = 0;
    }

    private void OnEnable()
    {
        visibleCharacters = 0;
        if (Thread.story.canContinue)
        {
            Continue();
        }
        
        if (Thread.story.currentChoices.Count > 0)
        {
            GenerateChoices();
        }
    }
    private void Update()
    {
        if (Thread != null)
        {
            revealTimer += Time.deltaTime;

            if (revealTimer > (1 / revealSpeed))
            {
                RevealCharacter();
                revealTimer = 0;
            }

            if (Thread.story.canContinue)
            {
                if (continueAction.triggered)
                {
                    if (!FinishedRevealing)
                    {
                        RevealAll();
                    }
                    else
                    {
                        Continue();
                        //Thread.CheckTags();
                        if (Thread.story.currentChoices.Count > 0)
                        {
                            GenerateChoices();
                        }
                    }
                    
                }
            }
            else
            {
                if (Thread.story.currentChoices.Count <= 0)
                {
                    if (continueAction.triggered)
                    {
                        if (!FinishedRevealing)
                        {
                            RevealAll();
                        }
                        else
                        {
                            //end dialogue.
                            Thread.CheckTags();

                            gameObject.SetActive(false);

                        }
                    }
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
        }
    }

    public void GenerateChoices()
    {
        Debug.Log("generating choices");
        List<Choice> choices = Thread.story.currentChoices;
        Debug.Log(choices.Count);
        foreach (Choice c in choices)
        {
            GameObject button = Instantiate(buttonPrefab);
            buttons.Add(button);
            button.transform.SetParent(buttonContainer);
            button.transform.localScale = new Vector3(1, 1, 1);
            button.GetComponentInChildren<TMP_Text>().text = c.text;
        }
    }

    public void ChoiceMadeByButton(GameObject g)
    {
        foreach (GameObject gameObject in buttons)
        {
            if (gameObject == g)
            {
                MakeChoice(buttons.IndexOf(gameObject));
            }
        }
    }

    public void DestoryChoices()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            Destroy(buttons[i]);
        }
    }

    private void Continue()
    {
        Thread.story.Continue();
        visibleCharacters = 0;
    }

    public void MakeChoice(int index)
    {
        Thread.story.ChooseChoiceIndex(index);
        Continue();
        //Thread.CheckTags();
        DestoryChoices();

    }

    private void RevealAll()
    {
        visibleCharacters = CurrentText.Length;
        dialogue.text = CurrentText;
    }

    private void RevealCharacter()
    {
        if (FinishedRevealing)
        {
            return;
        }

        dialogue.text = CurrentText.Substring(0, ++visibleCharacters);

        if (dialogue.text[dialogue.text.Length - 1] != ' ')
        {
            soundEffects.pitch = 0.3f + Random.Range(0.95f, 1.05f);
            soundEffects.PlayOneShot(speak);
        }
    }
}
