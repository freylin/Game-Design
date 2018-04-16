using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Look Around")]
public class LookAroundAction : Action
{

    public override void Act(StateController controller)
    {
        controller.transform.Rotate(0, controller.enemyStats.searchingTurnSpeed * Time.deltaTime, 0);
    }
}