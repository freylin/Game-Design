using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl playerController;

    public PoliceManager[] policeManagers;
    public Transform[] trajectories;
    public Image gameover;
    private void Awake()
    {
        if(trajectories.Length == 0 || trajectories.Length != policeManagers.Length)
        {
            Debug.Log("No trajectories or wrong number of trajectories");
        }
    }

    void Start()
    {
        gameover.enabled = false;
        //foreach (PoliceManager police in policeManagers)
        //{
        //    police.SetupAI(enemyWaypoints);
        //}
        for(int i = 0; i < policeManagers.Length; ++i)
        {
            policeManagers[i].SetupAI(GetWaypointList(trajectories[i]));
        }
    }

    List<Transform> GetWaypointList(Transform trajectory)
    {
        List<Transform> waypointList = new List<Transform>();
        for(int i = 0; i < trajectory.childCount; ++i)
        {
            waypointList.Add(trajectory.GetChild(i));
        }
        return waypointList;
    }

    public void GameOver()
    {
        playerController.Controllable = false;
        //Time.timeScale = 0f;
        foreach (var mngr in policeManagers)
        {
            mngr.StopAI();
        }
        gameover.enabled = true;
        gameover.color = Color.white;
    }
}
