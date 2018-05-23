using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vanisher;

[RequireComponent(typeof(CanvasGroup))]
public class PauseMenuToggle : MonoBehaviour {
	private CanvasGroup canvasGroup;

	void Awake() {
		canvasGroup = GetComponent<CanvasGroup> ();
		if (canvasGroup == null) {
			Debug.LogError("canvasGroup not found!");
		}
	}
	// Update is called once per frame
	void Update () {
        if (GameManager.gameover == true || GameManager.youwin == true)  // if game is stopped, pause menu is disabled
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0f;
        }
        else
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                Debug.Log("toggle pause menu");
                if (canvasGroup.interactable)
                {
                    canvasGroup.interactable = false;
                    canvasGroup.blocksRaycasts = false;
                    canvasGroup.alpha = 0f;
                    Time.timeScale = 1f;
                }
                else
                {
                    canvasGroup.interactable = true;
                    canvasGroup.blocksRaycasts = true;
                    canvasGroup.alpha = 1f;
                    Time.timeScale = 0f;
                }
            }
        }
	}
}
