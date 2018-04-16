using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Accelerate")]
public class Accelerate : Ability {
    public GameObject Accelerater;

    public override bool cast()
    {
        if (_isInCooldown) return false;
        caster = GameObject.FindGameObjectWithTag("Player");
        setCastPositon();
        if (Accelerater == null) return false;

        Instantiate<GameObject>(Accelerater, castPosition, castRotation);

        _isInCooldown = true;
        setCooldownTimer();
        return true;
    }

}
