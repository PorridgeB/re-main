using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
	[Header("Layers")]
	public LayerMask groundLayer;

	[Space]

	public bool onGround;
	public bool onWall;
	public bool onRightWall;
	public bool onLeftWall;
	public bool overLedge;
	public int wallSide;


	[Header("Collision")]
	public float collisionRadius = 0.25f;
	public Vector2 bottomOffset, rightOffset, leftOffset, boxSize, boxCenterOffset;

	void Update()
	{
		onGround = Physics2D.OverlapBox((Vector2)transform.position + boxCenterOffset, boxSize, 0, groundLayer);
		onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer)
			|| Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

		onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
		onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);
		overLedge = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset - boxCenterOffset / 2, collisionRadius, groundLayer) || Physics2D.OverlapCircle((Vector2)transform.position + rightOffset - boxCenterOffset / 2, collisionRadius, groundLayer);
		wallSide = onRightWall ? -1 : 1;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;

		var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };
		Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
		Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
		Gizmos.DrawWireCube((Vector2)transform.position + boxCenterOffset, boxSize);
		Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset - boxCenterOffset / 2, collisionRadius);
		Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset - boxCenterOffset / 2, collisionRadius);
	}
}
