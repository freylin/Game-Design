using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Out of Time")]
public class OutOfTimeDecision : Decision
{
    public float duration;

    public override bool Decide(StateController controller)
    {
        return controller.CheckIfCountDownElapsed(duration);
    }
}