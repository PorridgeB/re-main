using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : StateMachineBehaviour
{
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private float speed;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Projectile p = Instantiate(projectile).GetComponent<Projectile>();
        p.GetComponent<DamageSource>().SetValue(PlayerController.instance.GetComponent<PlayerStats>().ReadAttribute("Ranged Attack Damage"));
        p.Shoot(animator.transform.position, PlayerController.instance.GetFacing(), speed);
    }
}
