using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Distract")]
public class Distract : Ability {
    public GameObject pebble;

    private Vector3 force;

    private GameObject throwingPebble;

    public override bool cast()
    {
        if (_isInCooldown)
        {
            return false;
        }

        caster = GameObject.FindGameObjectWithTag("LeftHandHold");

        setCastPositon();

        if (pebble == null)
        {
            return false;
        }

        throwingPebble = Instantiate<GameObject>(pebble, castPosition, castRotation);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        force = player.transform.forward * 30;
        throwingPebble.GetComponent<Rigidbody>().AddForce(force, ForceMode.VelocityChange);

        _isInCooldown = true;
        setCooldownTimer();

        return true;
    }
}


