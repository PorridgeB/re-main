using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class StoryText : MonoBehaviour
{
    [Range(0, 1)]
    public float PercentageRevealed = 0;
    [Range(0.1f, 2)]
    public float CaretBlinkPeriod = 1;
    public char Caret = '_';
    public AudioClip AddCharacter;
    public AudioClip RemoveCharacter;

    [SerializeField]
    private AudioSource audioSource;
    private TextMeshProUGUI textMesh;
    private string text;
    private float caretTimer;
    private int previousLength;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        text = textMesh.text + " ";
        caretTimer = Time.time;
        previousLength = 0;
    }

    private void Update()
    {
        if (Time.time - caretTimer > CaretBlinkPeriod)
        {
            caretTimer = Time.time;
        }

        var revealedLength = Mathf.RoundToInt((text.Length - 1) * PercentageRevealed);

        var newTextBuilder = new StringBuilder(text);
        newTextBuilder[revealedLength] = Caret;
        textMesh.text = newTextBuilder.ToString();

        var showCaret = (Time.time - caretTimer) / CaretBlinkPeriod > 0.5f;

        textMesh.maxVisibleCharacters = revealedLength + (showCaret ? 1 : 0);

        if (previousLength != revealedLength)
        {
            if (textMesh.text[revealedLength] != ' ')
            {
                audioSource.PlayOneShot(previousLength < revealedLength ? AddCharacter : RemoveCharacter);
            }
        }

        previousLength = revealedLength;
    }
}
