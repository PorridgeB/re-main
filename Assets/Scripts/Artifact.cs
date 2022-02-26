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

    public string Name => asset.name;
    public string Description
    {
        get
        {
            if (story.currentText == "")
            {
                story.Continue();
            }
            return story.currentText;
        }
    }

    private void Start()
    {
        story = new Story(asset.text);
    }
    public void Interact()
    {
        openArtifact.Raise(gameObject);
    }
}
