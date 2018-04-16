using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Pause")]
public class PauseAction : Action
{
    public override void Act(StateController controller)
    {
        controller.navMeshAgent.isStopped = true;
    }
}