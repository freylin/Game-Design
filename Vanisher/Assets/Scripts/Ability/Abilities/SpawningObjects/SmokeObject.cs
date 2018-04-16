using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class SmokeObject : MonoBehaviour
{
    public float aliveTimeLimit = 5f;
    private CharacterStats stats;
    private SphereCollider cldr;
    private float startTime;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("SmokeObject: Cannot find Player gameobject.");
        }
        stats = player.GetComponent<CharacterStats>();
        if (stats == null)
        {
            Debug.LogError("SmokeObject: Cannot find Player's CharacterStats component.");
        }

        cldr = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, stats.transform.position) < cldr.radius)
        {
            //Debug.Log("player in range!");
            stats.Invisible = true;
        }
        else
        {
            stats.Invisible = false;
        }

        if (Time.time - startTime > aliveTimeLimit)
        {
            stats.Invisible = false;
            Destroy(this.gameObject);
        }
    }
}