using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {
    public bool toRight;  // whether the door goes to left or right

    private bool openning, closing, isOpen, isClose;  // different states of the door
    private Vector3 start, end;  // the start position and end position of the door
    private float openningEnterTime, closingEnterTime, openEnterTime;  // time stamps
    private float maxOpenTime;  // the door will close automatically after the door is being open for maxOpenTime
    private float openningTime;  // the time duration between beginning openning the door and finishing openning the door
    private float closingTime; // the time duration between beginning closing the door and finishing closing the door

	// Use this for initialization
	void Start () {
        openning = false;
        closing = false;
        isOpen = false;
        isClose = true;
        maxOpenTime = 5;
        openningTime = closingTime = 5;

        start = this.transform.position;
        float x = this.GetComponent<MeshRenderer>().bounds.size.x;
        float z = this.GetComponent<MeshRenderer>().bounds.size.z;
        
        float distance = Mathf.Max(x, z);
        if (toRight)
            end = this.transform.position + distance * this.transform.right;
        else
            end = this.transform.position - distance * this.transform.right;
	}
	
	// Update is called once per frame
	void Update() {
        if (openning)
        {
            if(Vector3.Distance(this.transform.position, end) < 10 * Mathf.Epsilon)
            {
                openning = false;
                isOpen = true;
                openEnterTime = Time.time;  // transition to "open"
                return;
            }
            Vector3 newPosition = Vector3.Lerp(start, end, (Time.time - openningEnterTime) / openningTime);
            this.transform.position = newPosition;
            return;
        }

        if (closing)
        {
            if (Vector3.Distance(this.transform.position, start) < 10 * Mathf.Epsilon)
            {
                closing = false;
                isClose = true;
                return;
            }
            Vector3 newPosition = Vector3.Lerp(end, start, (Time.time - closingEnterTime) / closingTime);
            this.transform.position = newPosition;
            return;
        }

        if (isOpen)
        {
            if(Time.time - openEnterTime > maxOpenTime)
            {
                isOpen = false;
                closing = true;
                closingEnterTime = Time.time;  // transition to "closing"
                return;
            }
        }

	}

    void OnTriggerEnter(Collider c)
    {
        if(isClose == true && openning == false)
        {
            openning = true;
            openningEnterTime = Time.time;  // transition to "openning"
        }
    }
}
