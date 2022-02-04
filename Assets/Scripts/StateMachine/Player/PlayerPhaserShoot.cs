using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhaserShoot : StateMachineBehaviour
{
    public float Speed = 15f;
    public float Damage = 10f;
    public float Distance = 0.25f;

    [SerializeField]
    private AudioClip phaserShoot;


    [SerializeField]
    private GameObject projectilePrefab;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SoundManager.PlaySound(phaserShoot, Random.Range(0.8f, 0.9f));

        var projectile = Instantiate(projectilePrefab).GetComponent<PhaserProjectile>();

        var player = PlayerController.instance;

        projectile.transform.position = player.transform.position + new Vector3(player.Facing.x, 0, player.Facing.y) * Distance;
        projectile.Direction = new Vector3(player.Facing.x, 0, player.Facing.y);
        projectile.Speed = Speed;
        projectile.Source = player.gameObject;
    }
}
