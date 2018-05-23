using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/IsAlive")]
public class IsAliveDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return controller.target.gameObject.activeSelf;
    }
}