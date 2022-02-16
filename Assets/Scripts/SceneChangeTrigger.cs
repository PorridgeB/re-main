using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeTrigger : MonoBehaviour
{
    [SerializeField]
    private GameEvent startRun;
    private bool entered;

    private void OnTriggerEnter(Collider other)
    {
        if (!entered)
        {
            Debug.Log("starting run");
            entered = true;
            startRun.Raise();
        }
        
    }
}