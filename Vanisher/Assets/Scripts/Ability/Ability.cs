using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject {

    public string abilityName;

    public Vector3 castPosition;
    public Quaternion castRotation;

    public GameObject caster;

    public float cooldown;

    private float _cooldownTimer;

    public bool _isInCooldown = false;

    public abstract bool cast();

    public void setCastPositon()  // cast position should be modified a little bit
    {
        if (caster != null)
        {
            castPosition = caster.transform.position;
            castRotation = caster.transform.rotation;
        }
    }

    //After casting a skill, this function must be called to set the skill into cooldown
    public void setCooldownTimer()
    {
        if (_isInCooldown)
        {
            _cooldownTimer = Time.time;
        }
    }
    //Check cooldown state in every tick. If skill is not in cooldown, then it return false, else it return if 
    //currentTime-timer is larger than cooldown.
    public bool CooldownState()
    {
        if (_isInCooldown)
            return Time.time - _cooldownTimer <= cooldown;
        else
            return false;
    }
    public void updateCooldown()
    {
		Debug.Log(_isInCooldown);
    }
}
