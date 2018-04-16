using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Hear Sound")]
public class HearSoundDecision : Decision
{
    public SoundType sound;

    public override bool Decide(StateController controller)
    {
        if (controller.HearSound == sound)
        {
            controller.HearSound = SoundType.None;
            return true;
        }
        else
        {
            return false;
        }
    }
}