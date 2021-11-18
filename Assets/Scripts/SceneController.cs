using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum ScenePhase
{
    NewGameMenu,
    Loading,
    Open,
    Game,
    Paused,
    Cinematic,
    Dialogue,
    Close,
    Save
}


public class SceneController : MonoBehaviour
{
    public delegate void SceneDelegate(ScenePhase current, ScenePhase next);
    public static event SceneDelegate ScenePhaseChanged;

    public delegate void SceneChange(Scene current);
    public static event SceneChange SceneUpdate;

    public static SceneController instance;

    public static Scene currentScene;
    public static ScenePhase phase;
    private bool paused;


    [Header("DebugTools")]
    public bool disableIntro;


    [Header("Scene Objects")]
    public PlayerController player;
    public CameraController playerCamera;

    public bool awaitingResponse;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }

    }

    private void UpdateScene(Scene next, LoadSceneMode what)
    {
        SceneUpdate?.Invoke(next);

        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 1)
        {
        }
        else
        {
            Debug.Log("Scene Updating");

            player = GameObject.Find("Player").GetComponent<PlayerController>();
            playerCamera = GameObject.Find("Player Camera").GetComponent<CameraController>();

            currentScene = next;
            ChangeScenePhase(ScenePhase.Loading);
        }

    }

    private void Start()
    {
        SceneManager.sceneLoaded += UpdateScene;

        currentScene = SceneManager.GetActiveScene();
        UpdateScene(currentScene, new LoadSceneMode());
    }

    private void Update()
    {
        switch (phase)
        {
            case ScenePhase.NewGameMenu:
                break;
            case ScenePhase.Loading:
                //initialize the scene
                ChangeScenePhase(ScenePhase.Open);
                break;
            case ScenePhase.Open:
                //reveal scene and show opening sequence
                ChangeScenePhase(ScenePhase.Game);
                break;
            case ScenePhase.Game:
                //the game
                break;
            case ScenePhase.Close:
                //show closing squence
                break;
            case ScenePhase.Cinematic:
                break;
            case ScenePhase.Dialogue:
                break;
            case ScenePhase.Paused:
                break;
            case ScenePhase.Save:
                break;
        }
    }

    public void Pause()
    {
        if (paused)
        {
            Time.timeScale = 1;
            paused = false;
        }
        else
        {
            paused = true;
            Time.timeScale = 0;
        }
    }

    public IEnumerator LoadScene(string scene, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(scene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ChangeScene(string name)
    {
        ChangeScenePhase(ScenePhase.Close);
        StartCoroutine(LoadScene(name, 0));
    }

    public static void ChangeScenePhase(ScenePhase scenePhase)
    {
        ScenePhaseChanged?.Invoke(phase, scenePhase);
        phase = scenePhase;
    }
}

