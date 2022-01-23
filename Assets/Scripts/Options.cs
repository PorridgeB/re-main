using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown dropdown;
    [SerializeField]
    private Slider soundEffects;
    [SerializeField]
    private Slider music;

    private void Start()
    {
        dropdown.value = PlayerPrefs.GetInt("Difficulty", 1);
        soundEffects.value = PlayerPrefs.GetFloat("SoundEffects", 1);
        music.value = PlayerPrefs.GetFloat("Music", 1);
    }

    public void Save()
    {
        PlayerPrefs.SetInt("Difficulty", dropdown.value);
        PlayerPrefs.SetFloat("SoundEffects", soundEffects.value);
        PlayerPrefs.SetFloat("Music", music.value);
        PlayerPrefs.Save();
    }
}
