using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRailgunShoot : StateMachineBehaviour
{
    public GameObject BeamPrefab;
    public float RecoilSpeed = 1.2f;
    public float RotationSpeed = 100f;
    public AnimationCurve Power;

    private GameObject beam;

    [SerializeField]
    private AudioClip railgunShoot;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var player = PlayerController.instance;

        beam = Instantiate(BeamPrefab, player.gameObject.transform);
        SoundManager.PlaySound(railgunShoot);

        beam.transform.rotation = Quaternion.LookRotation(new Vector3(player.Facing.x, 0, player.Facing.y), Vector3.up);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var power = Power.Evaluate(stateInfo.normalizedTime);

        var railgunBeam = beam.GetComponentInChildren<RailgunBeam>();
        railgunBeam.DistanceFactor = power;
        railgunBeam.WidthFactor = power;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(beam);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
