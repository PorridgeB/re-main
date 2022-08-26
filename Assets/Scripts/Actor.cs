using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour, IInteract
{
    [SerializeField]
    private Character character;

    // Start is called before the first frame update
    void Start()
    {
        character.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        character.GetStory();
    }
}
