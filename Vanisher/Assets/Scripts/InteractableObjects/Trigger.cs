using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

    public Camera closeupCamera;
    public GameObject[] triggeredObjects;
    public bool state = false;
    protected Animator anim;

  
    public void Action()
    {
        state = !state;
        anim.SetBool("isOn", state);
        for (int i = 0; i < triggeredObjects.Length; i++)
            triggeredObjects[i].GetComponent<Trigger>().Action();
    }
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }
}
