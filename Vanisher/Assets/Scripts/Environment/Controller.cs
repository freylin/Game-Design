using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : Trigger {

    public bool isInTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isInTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isInTrigger = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isInTrigger)
            Action();
    }
}
