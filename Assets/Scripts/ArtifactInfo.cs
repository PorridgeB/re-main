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

    [SerializeField]
    private GameObject backButton;
    [SerializeField]
    private GameObject forwardButton;

    private void OnEnable()
    {
        UpdateNavigationButtons();
    }

    private void UpdateNavigationButtons()
    {
        forwardButton.SetActive(true);
        backButton.SetActive(true);
        if (description.pageToDisplay == 1)
        {
            backButton.SetActive(false);
        }
        else if (description.pageToDisplay == description.textInfo.pageCount)
        {
            forwardButton.SetActive(false);
        }
    }

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

    public void ChangePageNumber(int direction)
    {
        description.pageToDisplay += direction;
        UpdateNavigationButtons();
    }
}
