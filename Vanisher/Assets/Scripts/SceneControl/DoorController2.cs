using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController2 : MonoBehaviour {
    private GameObject leftDoor, rightDoor;

    private bool openning, closing, isOpen, isClose;  // different states of the door
    private Vector3 startLeft, endLeft;  // the start position and end position of the left door
    private Vector3 startRight, endRight;  // the start position and end position of the right door
    private float openningEnterTime, closingEnterTime, openEnterTime;  // time stamps
    private float maxOpenTime;  // the door will close automatically after the door is being open for maxOpenTime
    private float openningTime;  // the time duration between beginning openning the door and finishing openning the door
    private float closingTime; // the time duration between beginning closing the door and finishing closing the door


    void Awake()
    {
        leftDoor = this.transform.parent.Find("LeftDoor").gameObject;
        rightDoor = this.transform.parent.Find("RightDoor").gameObject;
    }

    // Use this for initialization
    void Start () {
        openning = false;
        closing = false;
        isOpen = false;
        isClose = true;
        maxOpenTime = openningTime = closingTime = 5;

        float x = leftDoor.GetComponent<MeshRenderer>().bounds.size.x;
        //Debug.Log("x = " + x);

        startLeft = leftDoor.transform.position;
        startRight = rightDoor.transform.position;

        endLeft = leftDoor.transform.position - x * leftDoor.transform.right;
        endRight = rightDoor.transform.position + x * rightDoor.transform.right;
    }
	
	// Update is called once per frame
	void Update () {
        if (openning)
        {
            if (Mathf.Abs(Time.time - openningEnterTime) > openningTime)
            {
                openning = false;
                isOpen = true;
                openEnterTime = Time.time;  // transition to "open"
                Debug.Log("transition to open");
                return;
            }
            Vector3 newPositionLeft = Vector3.Lerp(startLeft, endLeft, (Time.time - openningEnterTime) / openningTime);
            leftDoor.transform.position = newPositionLeft;
            Vector3 newPositionRight = Vector3.Lerp(startRight, endRight, (Time.time - openningEnterTime) / openningTime);
            rightDoor.transform.position = newPositionRight;
            return;
        }

        if (closing)
        {
            if (Mathf.Abs(Time.time - closingEnterTime) > closingTime)
            {
                closing = false;
                isClose = true;
                return;
            }
            Vector3 newPositionLeft = Vector3.Lerp(endLeft, startLeft, (Time.time - closingEnterTime) / closingTime);
            leftDoor.transform.position = newPositionLeft;
            Vector3 newPositionRight = Vector3.Lerp(endRight, startRight, (Time.time - closingEnterTime) / closingTime);
            rightDoor.transform.position = newPositionRight;
            return;
        }

        if (isOpen)
        {
            if (Time.time - openEnterTime > maxOpenTime)
            {
                isOpen = false;
                closing = true;
                closingEnterTime = Time.time;  // transition to "closing"
                Debug.Log("transition to closing");
                return;
            }
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (isClose == true && openning == false && closing == false && isOpen == false)
        {
            openning = true;
            openningEnterTime = Time.time;  // transition to "openning"
            Debug.Log("transition to opening");
        }
    }
}
