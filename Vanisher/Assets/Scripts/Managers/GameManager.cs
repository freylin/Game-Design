using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Vanisher // to change the 'gear' icon to 'script' icon
{
    [RequireComponent(typeof(CanvasGroup))]
    public class GameManager : MonoBehaviour
    {
        //public UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl playerController;

        [Tooltip("Police instances that are attached with police manager scripts")]
        public PoliceManager[] policeManagers;

        [Tooltip("Patrolling trajectories of police instances")]
        public Transform[] trajectories;

        [Tooltip("Game-Over window that pops up after police catch the thief")]
        public GameObject canvasGameOver;

        [Tooltip("You-Win window that pops up after the thief arrives at the destination")]
        public GameObject canvasYouWin;

        private CanvasGroup canvasGroupGameOver;
        private CanvasGroup canvasGroupYouWin;

        public static bool gameover;
        public static bool youwin;


        private static GameManager gameManager = null;
        public static GameManager instance
        {
            get
            {
                return gameManager;
            }
        }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                gameManager = this;
            }
        }

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            gameover = false;
            youwin = false;
            if (trajectories.Length == 0 || trajectories.Length != policeManagers.Length)
            {
                Debug.LogError("No patrolling trajectories or wrong number of patrolling trajectories");
            }

            canvasGroupGameOver = canvasGameOver.GetComponent<CanvasGroup>();
            if (canvasGroupGameOver == null)
            {
                Debug.LogError("canvasGroupGameOver not found!");
            }

            canvasGroupYouWin = canvasYouWin.GetComponent<CanvasGroup>();
            if(canvasGroupYouWin == null)
            {
                Debug.LogError("canvasGroupYouWin not found!");
            }

            for (int i = 0; i < policeManagers.Length; ++i)
            {
                if(policeManagers[i] == null)
                {
                    Debug.LogError("no police manager initialized");
                }
                policeManagers[i].SetupAI(GetWaypointList(trajectories[i]));
            }
        }

        private List<Transform> GetWaypointList(Transform trajectory)
        {
            List<Transform> waypointList = new List<Transform>();
            for (int i = 0; i < trajectory.childCount; ++i)
            {
                waypointList.Add(trajectory.GetChild(i));
            }
            return waypointList;
        }

        public static void GameOver()
        {
            //playerController.Controllable = false;
            //gameover = true;

            //foreach (var mngr in instance.policeManagers)
            //{
            //    mngr.StopAI();
            //}

            //if (instance.canvasGroupGameOver.interactable)
            //{
            //    instance.canvasGroupGameOver.interactable = false;
            //    instance.canvasGroupGameOver.blocksRaycasts = false;
            //    instance.canvasGroupGameOver.alpha = 0f;
            //    Time.timeScale = 1f;
            //}
            //else
            //{
            //    instance.canvasGroupGameOver.interactable = true;
            //    instance.canvasGroupGameOver.blocksRaycasts = true;
            //    instance.canvasGroupGameOver.alpha = 1f;
            //    Time.timeScale = 0f;
            //}
            GameStop(0);
        }

        public static void YouWin()
        {
            GameStop(1);
        }

        private static void GameStop(int condition)  // condition = 1 : you win, condition = 0: game over
        {
            CanvasGroup canvasGroup;
            if (condition == 0)
            {
                canvasGroup = instance.canvasGroupGameOver;
                gameover = true;
                youwin = false;
                //Debug.Log("game over");
            }
            else
            {
                canvasGroup = instance.canvasGroupYouWin;
                youwin = true;
                gameover = false;
                //Debug.Log("you win");
            }

            foreach (var mngr in instance.policeManagers)
            {
                mngr.StopAI();
            }

            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
            Time.timeScale = 0f;
        }        
    }
}
