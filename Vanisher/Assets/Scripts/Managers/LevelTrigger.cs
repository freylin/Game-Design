using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour {
    public int levelID = 0;

    private float triggerTime = 2.0f;
    private float enterTime;
    bool isTriggered;

    // Use this for initialization
    void Start()
    {
        isTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggered)
        {
            if (Time.time - enterTime > triggerTime)
            {
                EventManager.TriggerEvent<ChangeLevelEvent, int>(levelID);
            }
        }
    }

    void OnTriggerEnter(Collider c)
    {
        enterTime = Time.time;
        isTriggered = true;
    }
}
