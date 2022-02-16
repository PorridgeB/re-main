using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRunning : StateMachineBehaviour
{
    [SerializeField]
    private float runSoundRange;
    [SerializeField]
    private float runSoundPeriod;
    [SerializeField]
    private AudioClip step;
    [SerializeField]
    private float stepInterval;
    private float timer;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerController.instance.Run();
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            SoundManager.PlaySound(step, Random.Range(0.2f,0.4f));
            timer = stepInterval;
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerController.instance.Stop();
    }
}
