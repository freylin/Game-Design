using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class AccelerationObject : MonoBehaviour {

    private GameObject target;
    private ParticleSystem particleSystem;
    private float speed;

	void Start () {
        target = GameObject.FindGameObjectWithTag("Player");
        if(target == null)
        {
            Debug.LogError("No target, i.e. player");
        }
        speed = target.GetComponent<ThirdPersonCharacter>().GetMoveSpeedMultiplier();
        if(speed <= 0)
        {
            Debug.LogError("speed multiplier is wrong");
        }
        particleSystem = target.transform.Find("AccelerateEffect").GetComponent<ParticleSystem>();
        if(particleSystem == null)
        {
            Debug.LogError("No particle system for accelerate effect");
        }
    }
	
	// Update is called once per frame
	void Update () {
        target.GetComponent<ThirdPersonCharacter>().SetMoveSpeedMultiplier(speed + 0.5f);        
    }

    private void OnDestroy()
    {
        target.GetComponent<ThirdPersonCharacter>().SetMoveSpeedMultiplier(speed);
        particleSystem.Stop();
    }
}
