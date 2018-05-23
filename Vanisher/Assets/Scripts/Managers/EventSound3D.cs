using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EventSound3D : MonoBehaviour
{
    public AudioSource audioSrc;

    private float timeAwake;
    private float timeLife;

    void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
        if (audioSrc == null)
        {
            Debug.LogError("no audio source");
        }
        timeAwake = Time.time;
        timeLife = 2.0f;
    }


    void Update()
    {
        if (Time.time - timeAwake > timeLife)
        {
            Destroy(this.gameObject);
        }
    }
}
