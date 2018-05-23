using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmfulArea : MonoBehaviour {

    public int damagePerHit;
    public float damageInterval;


    float lastDamageTime;

    private void Start()
    {
        damagePerHit = 10;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Time.time - lastDamageTime > damageInterval)
            {
                other.GetComponent<PlayerHealth>().damage(damagePerHit);
                lastDamageTime = Time.time;

                // play "get hit" sound
                EventManager.TriggerEvent<GetHitEvent, Vector3>(other.transform.position);
            }
        }
    }
}
