using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Stay In Sight")]
public class StayInSightDecision : Decision
{
    private const float deltaAngle = 1f;

    public override bool Decide(StateController controller)
    {
        Vector3 lineOfViewpoint = controller.viewpoint.forward.normalized * controller.enemyStats.lookRange;
        Vector3 leftLineOfSight = Quaternion.Euler(0, -controller.enemyStats.lookAngle / 2, 0) * lineOfViewpoint;
        Vector3 rightLineOfSight = Quaternion.Euler(0, controller.enemyStats.lookAngle / 2, 0) * lineOfViewpoint;

        Debug.DrawRay(controller.viewpoint.position, leftLineOfSight, controller.currentState.sceneGizmoColor);
        Debug.DrawRay(controller.viewpoint.position, rightLineOfSight, controller.currentState.sceneGizmoColor);
        Omega.Debug.DrawArc(controller.viewpoint.position, controller.viewpoint.up,
            controller.viewpoint.position + leftLineOfSight, controller.enemyStats.lookAngle, controller.currentState.sceneGizmoColor);

        Vector3 lineOfTarget = controller.target.Find("TargetPos").position - controller.viewpoint.position;
        Debug.DrawRay(controller.viewpoint.position, lineOfTarget, Color.grey);

        float currentAngle = Vector3.Angle(lineOfViewpoint, lineOfTarget);

        RaycastHit hit;
        bool hitted = Physics.SphereCast(controller.viewpoint.position, controller.enemyStats.lookSphereCastRadius, lineOfTarget.normalized, out hit, controller.enemyStats.lookRange);
        //if (hitted)
        //{
        //    Debug.Log("hit target name " + hit.transform.gameObject.name);
        //    Debug.DrawRay(hit.point, hit.normal * 5, Color.magenta, 0.5f);
        //}

        if (!controller.isBlind &&
            (currentAngle < controller.enemyStats.lookAngle / 2 || lineOfTarget.magnitude < controller.enemyStats.minAlertRadius) &&
            hitted && hit.collider.CompareTag("Player") && !hit.collider.GetComponent<CharacterStats>().Invisible)
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