using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Chase")]
public class ChaseAction : Action
{
    public override void Act(StateController controller)
    {
        controller.characterControl.SetTarget(controller.target);
        //controller.navMeshAgent.destination = controller.target.position;
        //controller.navMeshAgent.isStopped = false;  // TODO: check this
        //controller.animator.SetTrigger("chase");
    }
}