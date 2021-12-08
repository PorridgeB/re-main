using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent Event;
    public UnityEvent Response;
    public UnityEvent<GameObject> ResponseGO;
    public UnityEvent<DamageSource> ResponseDS;

    public void OnEnable()
    {
        Event?.RegisterListener(this);
    }

    public void OnDisable()
    {
        Event?.UnRegisterListener(this);
    }

    public void OnEventRaised()
    {
        Response.Invoke();
    }

    public void OnEventRaised(GameObject gameObject)
    {
        ResponseGO.Invoke(gameObject);
    }

    public void OnEventRaised(DamageSource source)
    {
        ResponseDS.Invoke(source);
    }
}
