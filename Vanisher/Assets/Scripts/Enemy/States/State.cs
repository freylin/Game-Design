using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/State")]
public class State : ScriptableObject
{
    public Action[] actions;
    public Action[] onEnterActions;
    public Action[] onExitActions;
    public Transition[] transitions;

    //public string animatorParameter;
    public Color sceneGizmoColor;

    public void OnEnter(StateController controller)
    {
        //controller.animator.SetBool(animatorParameter, true);
        foreach (Action a in onEnterActions)
        {
            a.Act(controller);
        }
    }

    public void OnExit(StateController controller)
    {
        //controller.animator.SetBool(animatorParameter, false);
        foreach (Action a in onExitActions)
        {
            a.Act(controller);
        }
    }

    public void UpdateState(StateController controller)
    {
        DoActions(controller);
        CheckTransitions(controller);
    }

    private void DoActions(StateController controller)
    {
        foreach (Action a in actions)
        {
            a.Act(controller);
        }
    }

    private void CheckTransitions(StateController controller)
    {
        foreach (Transition transition in transitions)
        {
            bool decisionSucceeded = transition.decision.Decide(controller);

            if (decisionSucceeded)
            {
                controller.TransitionToState(transition.trueState);
            }
            else
            {
                controller.TransitionToState(transition.falseState);
            }
        }
    }
}