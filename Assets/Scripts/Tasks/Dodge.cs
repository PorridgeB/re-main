using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

public class Dodge : Action
{
	public SharedFloat Distance;
	public SharedFloat Speed;
	public SharedFloat Duration;

	private Vector3 dodgeDirection;
	private float startTime = 0;
	private NavMeshAgent agent;

	public override void OnStart()
	{
		agent = GetComponent<NavMeshAgent>();

		startTime = -1;

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

			if (!gameObject.CompareTag(phaserProjectile.Target))
            {
				continue;
            }

			var directionToProjectile = (projectile.transform.position - transform.position).normalized;

			// Projectile is heading towards us
			if (Vector3.Dot(phaserProjectile.Direction, directionToProjectile) < 0)
            {
				var perpDirectionToProj = new Vector3(-directionToProjectile.z, 0, directionToProjectile.x);
				dodgeDirection = perpDirectionToProj * (Vector3.Dot(phaserProjectile.Direction, perpDirectionToProj) < 0 ? 1 : -1);
				startTime = Time.time;
				break;
			}
		}
	}

	public override TaskStatus OnUpdate()
	{
		if (startTime < 0)
        {
			return TaskStatus.Failure;
        }

		var timePercentage = (Time.time - startTime) / Duration.Value;
		var speedFactor = Mathf.Pow(1 - timePercentage, 2);

		agent.Move(dodgeDirection * speedFactor * Speed.Value * Time.deltaTime);

		return Time.time - startTime > Duration.Value ? TaskStatus.Success : TaskStatus.Running;
	}
}