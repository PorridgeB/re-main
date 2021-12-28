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
        var p = Instantiate(projectile).GetComponent<Projectile>();

        var damageSource = p.GetComponent<DamageSource>();
        damageSource.source = PlayerController.instance.gameObject;
        damageSource.AddInstance(new DamageInstance { value = 10, source = PlayerController.instance.gameObject });

        p.transform.position = new Vector3(animator.transform.position.x, 0f, animator.transform.position.z);
        p.Direction = PlayerController.instance.GetFacing();
        p.Speed = speed;
    }
}
