using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGadget : StateMachineBehaviour
{
    public SaveSO currentSave;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Deploying " + currentSave.SelectedLoadout.Gadget.Name);
        Granade g = Instantiate(currentSave.SelectedLoadout.Gadget.Prefab).GetComponent<Granade>();
        var pos = PlayerController.instance.transform.position;
        pos.y = 1;
        g.transform.position = pos;
        g.SetDir(new Vector3(PlayerController.instance.Facing.x, 0, PlayerController.instance.Facing.y));
    }
}
