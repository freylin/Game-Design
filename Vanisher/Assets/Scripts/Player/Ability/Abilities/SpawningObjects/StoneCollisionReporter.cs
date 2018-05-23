using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Rigidbody))]
public class StoneCollisionReporter : MonoBehaviour
{

    public AudioClip stoneSound;
    public GameObject soundWaveEffect;
    public float soundRangeRatio = 0.1f;
    private AudioSource audioSource;
    private Rigidbody rb;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.Log("Audio Source can not be found");
        }

        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.Log("Rigidbody can not be found");
        }
    }

    void OnCollisionEnter(Collision c)
    {
        float maxDistance = soundRangeRatio * rb.velocity.magnitude;
        //Debug.Log("in footstep callback():  speed: " + rb.velocity.magnitude + "  distance: " + maxDistance);
        Omega.Debug.DrawCircle(c.contacts[0].point, Vector3.up, c.contacts[0].point + Vector3.forward * maxDistance, Color.blue, 1f);
        EventManager.TriggerEvent<StoneCollisionEvent, Vector3, float>(c.contacts[0].point, maxDistance);

        float vol = Random.Range(0.5f, 1.0f);
        audioSource.PlayOneShot(stoneSound, vol);
        Instantiate(soundWaveEffect, transform.position, Quaternion.Euler(-90f, 0, 0));
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject, stoneSound.length);
    }
}