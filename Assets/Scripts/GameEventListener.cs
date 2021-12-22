using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour, IListenGameEvent
{
    public GameEvent Event;
    public UnityEvent<GameEvent> Response;
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

    public void OnEventRaised(GameEvent gameEvent)
    {
        Response.Invoke(gameEvent);
    }

    public void OnEventRaised(GameEvent gameEvent, GameObject gameObject)
    {
        ResponseGO.Invoke(gameObject);
    }

    public void OnEventRaised(GameEvent gameEvent, DamageSource source)
    {
        ResponseDS.Invoke(source);
    }
}
