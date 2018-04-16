using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour {
    public Image gameover;
    
    void Awake()
    {
        if(gameover == null)
        {
            Debug.Log("No gameover");
        }
    }
	void Start () {
        gameover.enabled = false;
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            gameover.enabled = true;
            gameover.color = new Vector4(1, 1, 1, 1);
            //Debug.Log("hahaha");
            Time.timeScale = 0f;  // pause the game
        }
    }
}
