using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    [SerializeField] private bool invisible = false;
    public bool Invisible { get { return invisible; } set { invisible = value; } }
}