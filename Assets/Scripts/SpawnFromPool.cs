using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFromPool : MonoBehaviour
{
    [SerializeField]
    private ObjectPool pool;
    [SerializeField]
    private Transform spawnPoint;

    private void Start()
    {
        Instantiate(pool.GetRandom(), spawnPoint.position, new Quaternion(), transform);
    }
}
