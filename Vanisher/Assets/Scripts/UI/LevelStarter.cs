using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStarter : MonoBehaviour {
    public int level;

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level" + level.ToString());
    }
	
}
