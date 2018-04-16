using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbilityController : MonoBehaviour {
    Animator anim;
    public Ability[] abilities;

    private void Start()
    {
        for (int i = 0; i < abilities.Length; i++)
            abilities[i].updateCooldown();
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.Log("No animator!");
        }
    }

    private void Update()
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                //if (!abilities[i].cast())
                //{
                //    Debug.Log(abilities[i].abilityName + " FAILED");
                //    continue;
                // }

                //ability animation
                //if (abilities[i].abilityName == "Distract" && abilities[i]._isInCooldown == false)
                //{
                //    anim.SetBool("Throw", true);
                //}

                if (abilities[i]._isInCooldown == true) break;

                switch (abilities[i].abilityName)
                {
                    case "Distract":
                        anim.SetBool("Throw", true);
                        break;
                    case "Accelerate":
                        //Debug.Log("acce");
                        abilities[i].cast();
                        break;
                    case "Smoke Screen":
                        //Debug.Log("smk");
                        abilities[i].cast();
                        break;
                }
            }
        }





        for (int i = 0; i < abilities.Length; i++)
            abilities[i].updateCooldown();
    }
}
