using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour {
	public string level = "";

	public void StartGame() {
        Debug.Log("start game");
		Time.timeScale = 1f;
		SceneManager.LoadScene (level);
	}
}
