using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This class is used as callbacks for animation events
//Please name the functions as <animation name>_callback()

public class CharacterAnimationCallbacks : MonoBehaviour {

    Ability[] abilities;
    Animator anim;

    private void Start()
    {
        abilities = GetComponent<CharacterAbilityController>().abilities;
        //Debug.Log(abilities);
        anim = GetComponent<Animator>();
    }

    void throw_callback()
    {
        for (int i = 0;i< abilities.Length;i++)
        {
            if (abilities[i].abilityName == "Distract")
            {
                abilities[i].cast();
                anim.SetBool("Throw", false);
                break;
            }
        }
    }
	
}
