using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private SpriteRenderer render;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        ChangeVisibility(false);
    }

    public void ChangeVisibility(bool value)
    {
        render.enabled = value;
    }

    public void Interact()
    {
        if (transform.parent.CompareTag("Module"))
        {
            ModuleItem m = transform.parent.GetComponent<ModuleItem>();
            m.Module.count++;
            foreach (Bonus b in m.Module.bonuses)
            {
                b.attribute.AddModuleBonus(b);
            }
            Destroy(m.gameObject);
        }
        else if (transform.parent.CompareTag("Character"))
        {
            PlayerController.instance.StartDialogue();
        }
        else if (transform.parent.CompareTag("Artifact"))
        {
            PlayerController.instance.OpenArtifact();
        }
    }
}
