using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator), typeof(NavMeshAgent))]
public class PoliceManager : MonoBehaviour
{

    private StateController stateController;
    //private NavMeshAgent navMeshAgent;
    //private Transform viewpoint;
    //private GameObject player;

    void Awake()
    {
        //navMeshAgent = GetComponent<NavMeshAgent>();
        //if (navMeshAgent == null)
        //{
        //    Debug.LogError("PoliceManager:ERR: cannot find NavMeshAgent component.");
        //}

        //viewpoint = transform.Find("swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:Neck/swat:Neck1/swat:Head/Viewpoint");
        //if (viewpoint == null)
        //{
        //    Debug.LogError("PoliceManager:ERR: cannot find child game object Viewpoint.");
        //}

        stateController = GetComponent<StateController>();
        if (stateController == null)
        {
            Debug.LogError("PoliceManager:ERR: cannot find StateController component.");
        }

        //player = GameObject.FindGameObjectWithTag("Player");
        //if (player == null)
        //{
        //    Debug.LogError("PoliceManager:ERR: cannot find player game object.");
        //}
    }

    public void SetupAI(List<Transform> waypoints)
    {
        if(stateController == null)
        {
            Debug.LogError("state controller is null");
        }
        stateController.SetupAI(true, waypoints);
    }

    public void StopAI()
    {
        stateController.TransitionToState(stateController.idleState);
    }
}