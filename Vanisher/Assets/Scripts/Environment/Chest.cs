using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Trigger {

    private bool isInTrigger = false;

    public int[] abilitiesNums;
    private GameObject player;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            isInTrigger = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            isInTrigger = false;
        }
    }

    new void Action()
    {
        for (int i = 0;i< abilitiesNums.Length;i++)
        {
            player.GetComponent<CharacterAbilityController>().abilities[i].currentNumber += abilitiesNums[i];
        }
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isInTrigger)
            Action();
    }
}
