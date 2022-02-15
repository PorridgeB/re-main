using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingSFX : MonoBehaviour
{
    [SerializeField]
    private AudioClip sound;
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private float timeout;
    private float timer;

    

    [SerializeField]
    private float soundInterval;
    private float intervalTimer;

    private int triggerCount;

    [SerializeField]
    private float pitch;
    private float pitchMod;
    private float pitchMax = 1.6f;

    private void Start()
    {
        source.clip = sound;
    }

    public void Trigger()
    {
        if (triggerCount == 0)
        {
            intervalTimer = -1;
        }
        triggerCount++;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        intervalTimer -= Time.deltaTime;

        if (timer < 0)
        {
            pitchMod = 1;
        }
        if (intervalTimer < 0)
        {
            intervalTimer = soundInterval;
            if (triggerCount > 0)
            {
                triggerCount--;
                source.pitch = pitchMod * pitch;
                source.Play();
                timer = timeout;
                pitchMod = Mathf.Min(pitchMax, pitchMod + 0.05f);
            }
        }
    }
}
