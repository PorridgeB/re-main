using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    [SerializeField]
    private Animator veil;
    private string destination;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SceneReady()
    {
        veil.SetTrigger("SceneReady");
    }

    public void LevelEndReached()
    {
        destination = "Level";
        veil.SetTrigger("SceneEnded");
    }

    public void playerDied()
    {
        destination = "Hub";
        veil.SetTrigger("SceneEnded");
    }

    public void SceneEnded()
    {
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(destination);
    }
}
