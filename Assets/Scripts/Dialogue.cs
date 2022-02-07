using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public string text = "Hello world!";
    public float revealSpeed = 14; // characters per second

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

    private void Start()
    {
        visibleCharacters = 0;
        revealTimer = 0;
    }

    private void Update()
    {
        revealTimer += Time.deltaTime;

        if (revealTimer > (1 / revealSpeed))
        {
            RevealCharacter();
            revealTimer = 0;
        }
    }

    private void RevealCharacter()
    {
        if (visibleCharacters >= text.Length)
        {
            return;
        }

        dialogue.text = text.Substring(0, ++visibleCharacters);

        if (dialogue.text[dialogue.text.Length - 1] != ' ')
        {
            soundEffects.pitch = 0.3f + Random.Range(0.95f, 1.05f);
            soundEffects.PlayOneShot(speak);
        }
    }
}
