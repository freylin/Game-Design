using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject {

    public string abilityName;

    public Vector3 castPosition;
    public Quaternion castRotation;

    public GameObject caster;

    public int currentNumber = 0;

    public float cooldown;  // duration of cool down

    public float inCooldownTime;  // how long have the player's skill been in cooldown state

    private float _cooldownTimer;  // the time when player last casts the skill

    public bool _isInCooldown = false;

    public bool preview = false;

    public abstract bool cast();

    public virtual void Start()
    {
        // do any possible initialization
        //Debug.Log("start in Ability");
        _isInCooldown = false;

        inCooldownTime = cooldown;
        currentNumber = 0;   // hard code -Yaohong
    }

    public virtual void PreviewEffect()
    {
        //Debug.Log("preview effect in Ability");
    }

    public virtual void ClearPreviewEffect()
    {
        //Debug.Log("clear preview effect in Ability");
    }

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
        {
            inCooldownTime = Time.time - _cooldownTimer;
            return inCooldownTime <= cooldown;
        }
        else
            return false;
    }
    public void updateCooldown()
    {
		_isInCooldown = CooldownState ();
    }
}
