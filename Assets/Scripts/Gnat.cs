using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gnat : MonoBehaviour
{
    public GameObject ExplosionPrefab;

    public void Detonate()
    {
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
