using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRotate : MonoBehaviour
{
    public float Speed = -0.3f;

    private void Update()
    {
        transform.Rotate(new Vector3(0, Speed * Time.deltaTime, 0));
    }
}
