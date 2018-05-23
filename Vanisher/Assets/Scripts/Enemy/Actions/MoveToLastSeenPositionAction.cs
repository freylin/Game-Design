using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Move to Last Seen Position")]
public class MoveToLastSeenPositionAction : Action
{
    public override void Act(StateController controller)
    {
        controller.characterControl.SetTarget(null);
        controller.navMeshAgent.SetDestination(controller.lastSeenPosition);
        //if (controller.navMeshAgent.remainingDistance > controller.navMeshAgent.stoppingDistance || controller.navMeshAgent.pathPending)
        //{
        //    controller.navMeshAgent.destination = controller.lastSeenPosition;
        //    controller.navMeshAgent.isStopped = false;
        //}
    }
}