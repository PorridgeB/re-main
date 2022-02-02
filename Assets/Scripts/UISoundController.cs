using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundController : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip ButtonClick;
    public AudioClip ButtonHighlight;

    public void OnButtonClick()
    {
        AudioSource.PlayOneShot(ButtonClick);
    }

    public void OnButtonHighlight()
    {
        AudioSource.PlayOneShot(ButtonHighlight);
    }
}
