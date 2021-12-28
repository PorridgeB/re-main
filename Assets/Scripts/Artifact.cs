using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class Artifact : MonoBehaviour, IInteract
{
    [SerializeField]
    private TextAsset asset;
    private Story story;

    private void Start()
    {
        story = new Story(asset.text);
    }
    public void Interact()
    {
        //set story to this story
        //event to open UI

        PlayerController.instance.OpenArtifact();
    }
}
