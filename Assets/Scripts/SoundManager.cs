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

    public static ClimbingSFX dataSound;

    public Song menuMusic;
    public Song gameMusic;
    public Song summaryMusic;

    // Start is called before the first frame update
    void Awake()
    {
        if (!spawned)
        {
            spawned = true;
            DontDestroyOnLoad(gameObject);
            nextSong = currentSong;
            SceneManager.sceneLoaded += SceneUpdate;
            music = GetComponent<AudioSource>();
            dataSound = GetComponent<ClimbingSFX>();
            if (currentSong != null)
            {
                music.clip = currentSong.clip;
            }
            music.Play();
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void SceneUpdate(Scene current, LoadSceneMode mode)
    {
        //change music when scene changes
        switch (current.name)
        {
            case "Level":
                ChangeMusic(gameMusic, true);
                break;
            case "Hub":
                ChangeMusic(menuMusic, true);
                break;
            case "RunSummary":
                ChangeMusic(summaryMusic, true);
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
        if (currentSong != null)
        {
            if (currentSong.clip != nextSong.clip)
            {
                if (currentSong.bar)
                {
                    currentSong = nextSong;
                    music.clip = currentSong.clip;
                    music.Play();
                }
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

    public static GameObject PlaySound(AudioClip clip, float volume)
    {
        if (Time.timeScale == 0)
        {
            return null;
        }
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.PlayOneShot(clip);
        return audioSource.gameObject;
    }

    public static void AddDataSound()
    {
        dataSound.Trigger();
    }
}
