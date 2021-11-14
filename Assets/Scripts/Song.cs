using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Song : MonoBehaviour
{
    public float bpm;
    public AudioClip clip;
    public bool bar;
    public float beatTimer;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        beatTimer -= Time.deltaTime;
        if (beatTimer < 0)
        {
            bar = true;
            beatTimer = 60 / bpm * 4;
        }
    }

    private void LateUpdate()
    {

        bar = false;
    }
}

