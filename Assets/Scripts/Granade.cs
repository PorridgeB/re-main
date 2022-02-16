using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour
{
    private Vector3 dir;
    private float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void SetDir(Vector3 direction)
    {
        Debug.Log(direction);
        dir = direction;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += dir * speed * Time.deltaTime;
    }
}
