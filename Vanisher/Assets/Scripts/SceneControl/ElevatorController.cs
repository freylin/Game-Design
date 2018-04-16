using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour {
    public GameObject elevatorWall;
    public GameObject elevatorFloor;
    public int numLevels;

    private Vector3 up;
    private Vector3 start, end;
    private bool moving;
    private float movingTime = 5;
    private float startTime;

    void Awake()
    {
        if(elevatorWall == null)
        {
            Debug.Log("No elevator wall attached!");
        }
        if(elevatorFloor == null)
        {
            Debug.Log("No elevator floor attached!");
        }
    }

	// Use this for initialization
	void Start () {
        MeshRenderer mr = elevatorWall.GetComponent<MeshRenderer>();
        float heightWall = mr.bounds.size.y;
        float heightLevel = heightWall / numLevels;
        up = new Vector3(0, heightLevel, 0);  // hard code!
        moving = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(moving == true)
        {
            if(Vector3.Distance(elevatorFloor.transform.position, end) < Mathf.Epsilon)
            {
                moving = false;
                return;
            }
            elevatorFloor.transform.position = Vector3.Lerp(start, end, (Time.time - startTime) / movingTime);
        }
	}

    void OnTriggerEnter()
    {
        //trigger elevator event!
        if(moving == false)
        {
            moving = true;
            start = elevatorFloor.transform.position;
            end = elevatorFloor.transform.position + up;
            startTime = Time.time;
        }
    }
}
