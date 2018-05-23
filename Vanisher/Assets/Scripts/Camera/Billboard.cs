using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    void LateUpdate()
    {
        var target = Camera.main.transform.position;
        transform.LookAt(target);
    }
}