using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataFragments : MonoBehaviour
{

    [SerializeField]
    private int value;
    private bool timerStarted = false;
    private float timer = 0;

    public int Value => value;

    private ParticleSystem pSystem;
    void Start()
    {
        pSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (timerStarted){
            timer += Time.deltaTime;
            if (pSystem.particleCount == 0){
                Destroy(gameObject);
            }
        }
        if (timer >= (float)value/80){
            pSystem.Stop();
        }
        
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            timerStarted = true;
        }
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")){
            Vector3 dir = (other.transform.position - transform.position);
            dir.y = 0;
            dir = dir.normalized;
            transform.position += dir*timer*Time.deltaTime;
        }
    } 
}
