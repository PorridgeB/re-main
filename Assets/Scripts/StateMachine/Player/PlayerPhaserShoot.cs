using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhaserShoot : StateMachineBehaviour
{
    public float Speed = 15f;
    public float Damage = 10f;

    [SerializeField]
    private GameObject projectilePrefab;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var projectile = Instantiate(projectilePrefab).GetComponent<Projectile>();

        var damageSource = projectile.GetComponent<DamageSource>();
        damageSource.source = PlayerController.instance.gameObject;
        damageSource.AddInstance(new DamageInstance { value = Damage, source = PlayerController.instance.gameObject });

        var player = PlayerController.instance;

        projectile.transform.position = new Vector3(player.transform.position.x, 0f, player.transform.position.z);
        projectile.Direction = player.Facing;
        projectile.Speed = Speed;
    }
}
