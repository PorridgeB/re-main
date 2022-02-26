using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactInfo : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private TextMeshProUGUI description;
    [SerializeField]
    private Image picture;

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void SetArtifact(GameObject artifact)
    {
        Artifact a = artifact.GetComponent<Artifact>();

        title.text = a.Name;
        description.text = a.Description;
        picture.sprite = a.GetComponent<SpriteRenderer>().sprite;
    }
}
