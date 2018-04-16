using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter2 : MonoBehaviour {

	public void StartGame() {
		Time.timeScale = 1f;
		SceneManager.LoadScene ("Level2");
	}
}
