using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.InputSystem;
using TMPro;

public class ArtifactReaderUI : MonoBehaviour
{
    [SerializeField]
    private TextAsset ink;
    private Story story;
    private PlayerInput inputs;
    private InputAction continueAction;
    private InputAction backAction;

    private string text;

    [SerializeField]
    private GameObject artifactUI;
    [SerializeField]
    private TMP_Text title;
    [SerializeField]
    private TMP_Text content;

    // Start is called before the first frame update
    void Start()
    {
        LoadNewInk(ink);
        inputs = PlayerController.instance.GetComponent<PlayerInput>();
        continueAction = inputs.actions["Continue"];
        backAction = inputs.actions["Back"];
    }

    // Update is called once per frame
    void Update()
    {
        artifactUI.SetActive(inputs.currentActionMap.name == "ArtifactControl");
        title.text = story.state.currentPathString;
        content.text = text;
        if (continueAction.triggered)
        {
            if (story.canContinue)
            {
                story.Continue();
            }
            else
            {
                inputs.SwitchCurrentActionMap("CharacterControl");
            }
            if (backAction.triggered)
            {
                inputs.SwitchCurrentActionMap("CharacterControl");
            }
        }
        
    }
    public void LoadNewInk(TextAsset newFile)
    {
        story = new Story(newFile.text);
        while (story.canContinue)
        {
            text += story.currentText;
            story.Continue();
        }
    }
}
