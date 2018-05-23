using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Update Agent Move Speed")]
public class UpdateAgentMoveSpeedAction : Action
{
    public float speed;

    public override void Act(StateController controller)
    {
        controller.navMeshAgent.speed = speed;
    }
}