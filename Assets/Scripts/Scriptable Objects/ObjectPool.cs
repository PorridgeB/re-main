using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObjectPool : ScriptableObject
{
    [SerializeField]
    private List<GameObject> objects;

    public GameObject GetRandom()
    {
        return objects[Random.Range(0, objects.Count)];
    }
}
