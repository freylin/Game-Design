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
    private List<StateController> enemiesInside;

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
        enemiesInside = new List<StateController>();
    }

    private void Start()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        stats.Invisible = false;
        foreach (var enemy in enemiesInside)
        {
            enemy.isBlind = false;
        }
        enemiesInside.Clear();

        if (Time.time - startTime > aliveTimeLimit)
        {
            Destroy(this.gameObject);
            return;
        }

        if (Vector3.Distance(transform.position, stats.transform.position) < cldr.radius)
        {
            //Debug.Log("player in range!");
            stats.Invisible = true;
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, cldr.radius);
        foreach (Collider c in hitColliders)
        {
            if (c.CompareTag("Police"))
            {
                StateController enemy = c.GetComponent<StateController>();
                enemiesInside.Add(enemy);
                enemy.isBlind = true;
            }
        }

    }
}