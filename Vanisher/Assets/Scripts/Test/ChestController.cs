using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestController : MonoBehaviour {
    public Canvas canvas;
    public Image exclamation;

    private Camera cam;
    private GameObject player;
    private Image myExclamation;
    private Transform top;
    private float distanceExclamation = 20.0f;
    private float distanceOpen = 3.0f;
    private float degree = -45;
    private bool isOpen = false;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        if(player == null)
        {
            Debug.Log("No player");
        }

        if(canvas == null)
        {
            Debug.Log("No canvas");
        }

        if(exclamation == null)
        {
            Debug.Log("No exclamation sprite attached");
        }

        //if(cam == null)
        //{
        //    Debug.Log("No main camera");
        //}

        cam = Camera.main;

        top = this.transform.Find("chest_top");
        if(top == null)
        {
            Debug.Log("No chest top");
        }
    }

	// Use this for initialization
	void Start () {
        myExclamation = Instantiate(exclamation) as Image;
        myExclamation.transform.SetParent(canvas.transform, true);
        myExclamation.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(this.transform.position, player.transform.position);
        //Debug.Log("distance between player and chest = " + distance);

        // check if player is around
        if (isOpen == false && distance < distanceExclamation)
        {
            Vector3 screenPos = cam.WorldToScreenPoint(this.transform.position + new Vector3(0, 2.0f, 0));
            myExclamation.transform.position = screenPos;
            myExclamation.enabled = true;
            Debug.Log("show exclamation");

            if (isOpen == false && distance < distanceOpen && Input.GetKeyDown("o"))
            {
                Debug.Log("open the chest!");
                EventManager.TriggerEvent<OpenChestEvent, Vector3>(this.transform.position);
                top.Rotate(0, 0, degree);
                isOpen = true;
            }

        }
        else
        {
            myExclamation.enabled = false;
        }
	}

    
    
}
