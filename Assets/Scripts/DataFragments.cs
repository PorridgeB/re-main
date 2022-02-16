using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataFragments : MonoBehaviour
{

    [SerializeField]
    private int value;
    private bool timerStarted = false;
    private float timer = 0;

    public ClimbingSFX dataSound;
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

    private void OnParticleCollision(GameObject other)
    {
        SoundManager.AddDataSound();
    }
}
