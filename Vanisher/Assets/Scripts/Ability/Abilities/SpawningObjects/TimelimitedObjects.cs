using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelimitedObjects : MonoBehaviour {

    public float timeLimit;

    private float timer;

	void Start () {
        timer = Time.time;
		
	}
	
	void Update () {
        if (Time.time - timer > timeLimit)
            Destroy(this.gameObject);
	}
}
