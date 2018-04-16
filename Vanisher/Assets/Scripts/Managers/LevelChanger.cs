using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour {
    private UnityAction<int> changeLevelListener;

	// Use this for initialization
	void Awake () {
        changeLevelListener = new UnityAction<int>(ChangeLevelEventHandler);
	}
	
	void OnEnable()
    {
        EventManager.StartListening<ChangeLevelEvent, int>(changeLevelListener);
    }

    void OnDisable()
    {
        EventManager.StopListening<ChangeLevelEvent, int>(changeLevelListener);
    }

    void ChangeLevelEventHandler(int i)
    {
        // change scene to the specified level
        SceneManager.LoadScene("Level" + i.ToString(), LoadSceneMode.Single);
    }
}
