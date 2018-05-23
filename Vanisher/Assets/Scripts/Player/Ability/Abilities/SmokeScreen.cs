using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/SmokeScreen")]
public class SmokeScreen : Ability
{

    public GameObject smokeScreen;

    private GameObject player;  // cache player to avoid finding it when calling cast()

    public override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
    }


    public override void PreviewEffect()
    {
        base.PreviewEffect();
        Debug.Log("preview effect in smoke screen");
    }

    public override bool cast()
    {
        if (currentNumber > 0)
        {
            if (_isInCooldown) return false;
            //caster = GameObject.FindGameObjectWithTag("Player");
            caster = player;
            setCastPositon();
            if (smokeScreen == null) return false;

            Instantiate(smokeScreen, castPosition, castRotation);

            _isInCooldown = true;
            setCooldownTimer();
            currentNumber--;
        }
        else
            Debug.Log("not enough " + this.name);
        return true;
    }

}