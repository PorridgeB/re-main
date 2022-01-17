using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Group
{
    public int Size => members.Count;

    [SerializeField]
    private List<GameObject> members = new List<GameObject>();

    public void Add(GameObject gameObject)
    {
        members.Add(gameObject);
    }
}