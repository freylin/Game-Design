using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vanisher;

public class YouWinHandler : MonoBehaviour {
    //public Image youwin;

    //void Awake()
    //{
    //    if (youwin == null)
    //    {
    //        Debug.Log("No youwin");
    //    }
    //}

    //void Start()
    //{
    //    youwin.enabled = false;
    //}

    //void OnTriggerEnter(Collider c)
    //{
    //    if (c.CompareTag("Player"))
    //    {
    //        youwin.enabled = true;
    //        youwin.color = new Vector4(1, 1, 1, 1);

    //        Time.timeScale = 0f;
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.YouWin();
        }
    }
}
