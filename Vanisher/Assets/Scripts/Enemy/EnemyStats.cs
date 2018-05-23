using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/EnemyStats")]
public class EnemyStats : ScriptableObject
{

    //public float moveSpeed = 1;

    public float lookRange = 10f;
    public float lookAngle = 80f;  // degree
    public float lookSphereCastRadius = 0.1f;
    public float minAlertRadius = 1f;

    //public float attackRange = 1f;
    //public float attackRate = 1f;
    //public float attackForce = 15f;
    //public int attackDamage = 50;

    public float suspectDuration = 3f;

    public float searchDuration = 5f;
    public float searchingTurnSpeed = 90f;

    public float catchDistance = 1.5f;  // the maximum distance that enemy can catch the player
}