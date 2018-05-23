using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour {
    public EventSound3D eventSound3DPrefab;

    public AudioClip getHitAudio;
    public AudioClip openChestAudio;

    private UnityAction<Vector3> getHitEventListener;
    private UnityAction<Vector3> openChestEventListener;

    private void Awake()
    {
        getHitEventListener = new UnityAction<Vector3>(getHitEventHandler);
        openChestEventListener = new UnityAction<Vector3>(openChestEventHandler);
    }

    private void OnEnable()
    {
        EventManager.StartListening<GetHitEvent, Vector3>(getHitEventListener);
        EventManager.StartListening<OpenChestEvent, Vector3>(openChestEventListener);
    }

    private void OnDisable()
    {
        EventManager.StopListening<GetHitEvent, Vector3>(getHitEventListener);
        EventManager.StopListening<OpenChestEvent, Vector3>(openChestEventListener);
    }

    private void getHitEventHandler(Vector3 worldPos)
    {
        //if (eventSound3DPrefab)
        //{
        //    EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

        //    snd.audioSrc.clip = this.getHitAudio;

        //    snd.audioSrc.minDistance = 5f;
        //    snd.audioSrc.maxDistance = 100f;

        //    snd.audioSrc.Play();
        //}
        PlaySound(worldPos, this.getHitAudio);
    }

    private void openChestEventHandler(Vector3 worldPos)
    {
        PlaySound(worldPos, this.openChestAudio);
    }

    private void PlaySound(Vector3 worldPos, AudioClip audioClip, float minDistance = 5f, float maxDistance = 100f)
    {
        if (eventSound3DPrefab)
        {
            EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);
            snd.audioSrc.clip = audioClip;
            snd.audioSrc.minDistance = minDistance;
            snd.audioSrc.maxDistance = maxDistance;
            snd.audioSrc.Play();
        }
    }
}
