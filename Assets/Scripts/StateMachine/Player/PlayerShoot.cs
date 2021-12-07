using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : StateMachineBehaviour
{
    [SerializeField]
    private AttackEvent nextAttack;
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private float speed;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Projectile p = Instantiate(projectile).GetComponent<Projectile>();
        p.Shoot(new Vector3(animator.transform.position.x, 0.5f, animator.transform.position.z), PlayerController.instance.GetFacing(), speed, nextAttack.damageInstances);
        nextAttack.damageInstances.Clear();
    }
}
