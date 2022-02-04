using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMeleeAttack : StateMachineBehaviour
{
    [SerializeField]
    private GameObject attackField;
    [SerializeField]
    private AudioClip swordMelee;
    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private float comboDashSpeed;
    [SerializeField]
    private AnimationCurve speedCurve;
    private float dashTimer;
    private Vector2 direction;
    private GameObject attackFieldInstance;
    private float lastMeleeTime = 0f;
    private float comboTime = 0.8f;
    private int comboCounter = 0;
    private const int maximumCombo = 3;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // If the time since the last melee attack is within the combo time,
        // then increment the combo counter. Otherwise, reset it
        if (Time.time - lastMeleeTime < comboTime)
        {
            comboCounter++;
        }
        else
        {
            comboCounter = 0;
        }

        dashTimer = 0;
        direction = PlayerController.instance.Facing;

        SoundManager.PlaySound(swordMelee, Random.Range(0.4f, 0.5f));
        
        attackFieldInstance = Instantiate(attackField, PlayerController.instance.transform);

        var damageSource = attackFieldInstance.GetComponent<DamageSource>();
        damageSource.source = PlayerController.instance.gameObject;

        float distance = 0.5f;
        Vector3 forwardDirection = new Vector3(direction.x, 0, direction.y);

        attackFieldInstance.transform.localPosition = Vector3.up + forwardDirection * distance;
        attackFieldInstance.transform.rotation = Quaternion.LookRotation(forwardDirection);
    }


    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dashTimer += Time.deltaTime;

        var speed = comboCounter == maximumCombo ? comboDashSpeed : dashSpeed;

        PlayerController.instance.Dash(direction * speed * speedCurve.Evaluate(dashTimer));
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(attackFieldInstance);
        attackFieldInstance = null;
        PlayerController.instance.Stop();

        lastMeleeTime = Time.time;

        // Reset the combo counter if it has reached the maximum combo count
        if (comboCounter == maximumCombo)
        {
            comboCounter = 0;
        }
    }
}
