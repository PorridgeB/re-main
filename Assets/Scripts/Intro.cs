using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public string SceneName = "Intro";

    private PlayableDirector director;

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();

        director.stopped += delegate { SceneManager.LoadScene(SceneName); };
    }
}
