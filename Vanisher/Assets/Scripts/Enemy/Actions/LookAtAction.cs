using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Look At")]
public class LookAtAction : Action
{
    public override void Act(StateController controller)
    {
        //controller.transform.rotation = Quaternion.LookRotation(controller.target.transform.position - controller.viewpoint.position);
        //controller.transform.LookAt(controller.target.position);
        controller.transform.LookAt(controller.lastSeenPosition);
    }
}