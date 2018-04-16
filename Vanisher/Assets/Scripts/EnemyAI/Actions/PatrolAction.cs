using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Patrol")]
public class PatrolAction : Action
{
    public override void Act(StateController controller)
    {
        //controller.characterControl.SetTarget(controller.waypoints[controller.nextWaypoint]);
        //controller.navMeshAgent.SetDestination(controller.waypoints[controller.nextWaypoint].position);
        //controller.navMeshAgent.Resume();
        //controller.navMeshAgent.isStopped = false;  // TODO: check this
        //controller.animator.SetTrigger("patrol");
        //controller.animator.ResetTrigger("patrol");

        if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending)
        {
            controller.nextWaypoint = (controller.nextWaypoint + 1) % controller.waypoints.Count;
            controller.characterControl.SetTarget(controller.waypoints[controller.nextWaypoint]);
        }
    }

}