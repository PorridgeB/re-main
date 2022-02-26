using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class Artifact : MonoBehaviour, IInteract
{
    [SerializeField]
    public GameEvent openArtifact;

    [SerializeField]
    private TextAsset asset;
    private Story story;

    public string Name;
    public string Description;

    private void Start()
    {
        story = new Story(asset.text);
        SetNameAndDescription();
    }

    private void SetNameAndDescription()
    {
        int progress = 0;
        while (story.canContinue)
        {
            Debug.Log(story.currentText);
            story.Continue();
            if (story.currentText.Trim() == "Title:" || story.currentText.Trim() == "Content:")
            {
                Debug.Log("progressing");
                progress++;
                story.Continue();
            }
            if (progress == 1)
            {
                Name += story.currentText;
            }
            else if (progress == 2)
            {
                Description += story.currentText;
            }
        }
    }

    public void Interact()
    {
        openArtifact.Raise(gameObject);
    }
}
