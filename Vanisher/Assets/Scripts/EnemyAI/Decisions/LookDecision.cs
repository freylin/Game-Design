using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Look")]
public class LookDecision : Decision
{

    public override bool Decide(StateController controller)
    {
        bool targetVisible = Look(controller);
        return targetVisible;
    }

    private bool Look(StateController controller)
    {
        RaycastHit hit;

        Debug.DrawRay(controller.viewpoint.position, controller.viewpoint.forward.normalized * controller.enemyStats.lookRange, Color.green);

        if (Physics.SphereCast(controller.viewpoint.position, controller.enemyStats.lookSphereCastRadius, controller.viewpoint.forward, out hit, controller.enemyStats.lookRange)
            && hit.collider.CompareTag("Player"))
        {
            controller.target = hit.transform;
            controller.lastSeenPosition = hit.transform.position;
            return true;
        }
        else
        {
            return false;
        }
    }
}