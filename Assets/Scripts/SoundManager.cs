using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private static bool spawned = false;
    private AudioSource sound;
    public static AudioSource music;
    public Song currentSong;
    public static Song nextSong;

    public

    // Start is called before the first frame update
    void Awake()
    {
        if (!spawned)
        {
            spawned = true;
            DontDestroyOnLoad(gameObject);
            nextSong = currentSong;

            music = GetComponent<AudioSource>();
            music.clip = currentSong.clip;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void SceneUpdate(Scene current)
    {
        Debug.Log(current.name);
        switch (current.name)
        {
            case "The Overgrowth":
                Debug.Log("Staring music");
                music.Play();
                break;
            case "0_start":
                music.Stop();
                break;

        }

    }


    // Update is called once per frame
    void Update()
    {
        try
        {
            sound = GameObject.Find("Sound").GetComponent<AudioSource>();
            if (!sound.isPlaying)
            {
                Destroy(sound.gameObject);
            }
        }
        catch
        {

        }
        if (currentSong.bar)
        {
            if (currentSong != nextSong)
            {
                currentSong = nextSong;
                music.clip = currentSong.clip;
                music.Play();
            }
        }
    }

    public static void ChangeMusic(Song song, bool changeImmediate)
    {
        if (changeImmediate)
        {
            music.clip = song.clip;
        }
        nextSong = song;
    }

    public void StopMusic()
    {
    }

    public static void PlaySound(AudioClip clip)
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(clip);
    }

    public static void PlaySound(AudioClip clip, float volume)
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.PlayOneShot(clip);
    }
}
