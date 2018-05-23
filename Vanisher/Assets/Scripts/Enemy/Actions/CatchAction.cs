using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vanisher;

[CreateAssetMenu(menuName = "AI/Actions/Catch")]
public class CatchAction : Action
{
    public override void Act(StateController controller)
    {
        Debug.DrawRay(controller.transform.position, (controller.target.position - controller.transform.position).normalized * controller.enemyStats.catchDistance, controller.currentState.sceneGizmoColor);
        if ((controller.target.position - controller.transform.position).magnitude < controller.enemyStats.catchDistance)  // use body position, not eye position
        {
            //FindObjectOfType<GameManager>().GameOver();
            GameManager.GameOver();
        }
    }
}