using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Group : IEnumerable<GameObject>
{
    public int Size => members.Count;

    [SerializeField]
    private List<GameObject> members = new List<GameObject>();

    public void AddMember(GameObject gameObject)
    {
        members.Add(gameObject);
    }

    public void BroadcastMessage(string methodName, object value)
    {
        foreach (var member in members)
        {
            member?.SendMessage(methodName, value, SendMessageOptions.DontRequireReceiver);
        }
    }

    public IEnumerator<GameObject> GetEnumerator()
    {
        return ((IEnumerable<GameObject>)members).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)members).GetEnumerator();
    }
}