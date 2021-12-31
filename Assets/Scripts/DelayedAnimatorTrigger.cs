using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedAnimatorTrigger : MonoBehaviour
{
    public string TriggerName;
    public float Delay = 0f;

    void Start()
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(Delay);

        var animator = GetComponent<Animator>();
        animator.SetTrigger(TriggerName);
    }
}
