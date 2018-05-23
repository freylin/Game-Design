using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Accelerate")]
public class Accelerate : Ability {
    public GameObject Accelerater;

    private GameObject player;  // cache player to avoid finding it when calling cast()
    private ParticleSystem particleSystem;  // accelerate effect emitted from player's feet

    public override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
        if(player == null)
        {
            Debug.LogError("No player");
        }
        particleSystem = player.transform.Find("AccelerateEffect").GetComponent<ParticleSystem>();
        if(particleSystem == null)
        {
            Debug.LogError("No particle system for accelerate effect");
        }
    }

    public override void PreviewEffect()
    {
        base.PreviewEffect();
        Debug.Log("preview effect in Accelerate");
    }

    public override bool cast()
    {
        if (currentNumber > 0)
        {
            if (_isInCooldown) return false;
            //caster = GameObject.FindGameObjectWithTag("Player");
            caster = player;
            setCastPositon();
            if (Accelerater == null) return false;

            Instantiate<GameObject>(Accelerater, castPosition, castRotation);
            particleSystem.Play();
            


            _isInCooldown = true;
            setCooldownTimer();
            currentNumber--;
        }
        else
            Debug.Log("not enough " + this.name);
        return true;
    }

}
