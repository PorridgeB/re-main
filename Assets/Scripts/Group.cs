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

    public void Add(GameObject gameObject)
    {
        members.Add(gameObject);
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