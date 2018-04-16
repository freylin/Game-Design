using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/SmokeScreen")]
public class SmokeScreen : Ability {

    public GameObject smokeScreen;

    public override bool cast()
    {
        if (_isInCooldown) return false;
        caster = GameObject.FindGameObjectWithTag("Player");
        setCastPositon();
        if (smokeScreen == null) return false;

        GameObject Screen = Instantiate<GameObject>(smokeScreen, castPosition, castRotation);

        _isInCooldown = true;
        setCooldownTimer();
        return true;
    }

   
}
