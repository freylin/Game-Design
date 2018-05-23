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
            child.GetComponent<Collider>().enabled = state;
            foreach (Transform grandchild in child.transform)
            {
                grandchild.GetComponent<Renderer>().enabled = state;

            }
        }
    }
}
