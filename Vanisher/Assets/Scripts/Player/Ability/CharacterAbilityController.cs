using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbilityController : MonoBehaviour {
    Animator anim;
    public Ability[] abilities;

    public int abilitySelected;

    private void Start()
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            abilities[i].Start();
            abilities[i].updateCooldown();
        }
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("No animator!");
        }
        abilitySelected = -1;
    }

    private void Update()
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                if (abilitySelected == i)
                {
                    abilities[abilitySelected].ClearPreviewEffect();
                    abilitySelected = -1;
                }
                else
                {
                    abilitySelected = i;
                    abilities[abilitySelected].preview = true;
                    for(int j = 0; j < abilities.Length; ++j)
                    {
                        if (j != i) abilities[j].ClearPreviewEffect();
                    }
                }

                if (abilities[i]._isInCooldown == true) break;

                //switch (abilities[i].abilityName)
                //{
                //    case "Distract":
                //        anim.SetBool("Throw", true);
                //        break;
                //    case "Accelerate":
                //        Debug.Log("acce");
                //        abilities[i].cast();
                //        break;
                //    case "Smoke Screen":
                //        Debug.Log("smk");
                //        abilities[i].cast();
                //        break;
                //}

                if(abilities[i].abilityName != "Distract")
                {
                    abilities[i].cast();
                    abilitySelected = -1;
                }
            }
        }

        if (Input.GetMouseButtonDown(0))  // click left mouse to fire an ability
        {
            if(abilitySelected >= 0 && abilities[abilitySelected].name == "Distract")
            {
                if(abilities[abilitySelected]._isInCooldown == false && abilities[abilitySelected].currentNumber>0)
                {
                    abilities[abilitySelected].ClearPreviewEffect();
                    //switch (abilities[abilitySelected].abilityName)
                    //{
                    //    case "Distract":
                    //        anim.SetBool("Throw", true);
                    //        break;
                    //    case "Accelerate":
                    //        abilities[abilitySelected].cast();
                    //        break;
                    //    case "Smoke Screen":
                    //        abilities[abilitySelected].cast();
                    //        break;
                    //}
                    anim.SetBool("Throw", true);
                    abilitySelected = -1;
                }
            }
        }


        //Debug.Log("ability selected = " + abilitySelected);
        if (abilitySelected >= 0 && abilities[abilitySelected].preview == true) abilities[abilitySelected].PreviewEffect();

        for (int i = 0; i < abilities.Length; i++)
            abilities[i].updateCooldown();
    }
}
