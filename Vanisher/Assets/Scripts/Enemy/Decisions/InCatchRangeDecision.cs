using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vanisher;

[CreateAssetMenu(menuName = "AI/Decisions/InCatchRange")]
public class InCatchRangeDecision : Decision
{

    public override bool Decide(StateController controller)
    {
        Debug.DrawRay(controller.transform.position, (controller.target.position - controller.transform.position).normalized * controller.enemyStats.catchDistance, controller.currentState.sceneGizmoColor);
        if ((controller.target.position - controller.transform.position).magnitude < controller.enemyStats.catchDistance)  // use body position, not eye position
        {
            Debug.Log("Catched player!");
            //FindObjectOfType<GameManager>().GameOver();
            GameManager.GameOver();
            return true;
        }
        else
        {
            return false;
        }
    }
}