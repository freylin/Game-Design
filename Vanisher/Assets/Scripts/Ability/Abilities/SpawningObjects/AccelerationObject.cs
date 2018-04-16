using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class AccelerationObject : MonoBehaviour {

    GameObject target;

    float speed;

	void Start () {
        target = GameObject.FindGameObjectWithTag("Player");
        speed = target.GetComponent<ThirdPersonCharacter>().GetMoveSpeedMultiplier();
        //Debug.Log(speed);
    }
	
	// Update is called once per frame
	void Update () {
        target.GetComponent<ThirdPersonCharacter>().SetMoveSpeedMultiplier(speed + 0.5f);        
    }

    private void OnDestroy()
    {
        target.GetComponent<ThirdPersonCharacter>().SetMoveSpeedMultiplier(speed);
    }
}
