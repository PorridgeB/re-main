using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Dodge : Action
{
	public SharedFloat Distance;
	public SharedFloat Speed;
	public SharedFloat Duration;

	private Vector3 dodgeDirection;

	public override void OnStart()
	{
		// Find incoming projectile
		var projectiles = GameObject.FindGameObjectsWithTag("Projectile");
		foreach (var projectile in projectiles)
        {
			if (Vector3.Distance(transform.position, projectile.transform.position) > Distance.Value)
			{
				continue;
			}

			var phaserProjectile = projectile.GetComponent<PhaserProjectile>();

			// Projectile is not a Phaser Projectile
			if (phaserProjectile == null)
            {
				continue;
            }

			var directionToProjectile = (projectile.transform.position - transform.position).normalized;

			// Projectile is heading towards us
			if (Vector3.Dot(phaserProjectile.Direction, directionToProjectile) < 0)
            {
				var perpDirectionToProj = new Vector3(-directionToProjectile.z, 0, directionToProjectile.x);
				dodgeDirection = perpDirectionToProj * (Vector3.Dot(phaserProjectile.Direction, perpDirectionToProj) > 0 ? 1 : -1);
            }
		}
	}

	public override TaskStatus OnUpdate()
	{
		return TaskStatus.Failure;
	}
}