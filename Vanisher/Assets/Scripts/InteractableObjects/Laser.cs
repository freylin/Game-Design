using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Trigger
{
    new public void Action()
    {
        state = !state;

        
    }
    private void Update()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Renderer>().enabled = state;
            child.GetComponent<Collider>().enabled = state;
        }
    }
}
