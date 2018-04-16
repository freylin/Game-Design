using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// attach this script to the thief character
// trigger the footstep event when one of his feet touches the ground (or any of other colliders)
// and the thief is not crounching

[RequireComponent(typeof(AudioSource), typeof(Rigidbody))]
public class FootstepReporter : MonoBehaviour
{
    public AudioClip footstepSound;  // to access footstep sound
    public float footstepSoundRangeRatio = 1.5f;

    private AudioSource audioSource;  // to play sound
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

    void FootstepCallback()
    {
        float vol = Random.Range(0.4f, 0.6f);  // sound volumn when running

        if (GameObject.FindObjectOfType<AccelerationObject>())
        {
            vol += Random.Range(0.1f, 0.4f);  // sound volumn when running fast
        }

        float maxDistance = footstepSoundRangeRatio * rb.velocity.magnitude;
        //Debug.Log("in footstep callback():  speed: " + rb.velocity.magnitude + "  distance: " + maxDistance);
        Omega.Debug.DrawCircle(transform.position, transform.up, transform.position + transform.forward * maxDistance, Color.blue, 1f);

        EventManager.TriggerEvent<FootstepEvent, Vector3, float>(this.transform.position, maxDistance);

        audioSource.PlayOneShot(footstepSound, vol);
    }
}