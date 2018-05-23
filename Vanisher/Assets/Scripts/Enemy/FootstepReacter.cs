using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class FootstepReacter : MonoBehaviour
{
    private UnityAction<Vector3, float> footstepEventListener;
    private StateController stateController;

    void Awake()
    {
        footstepEventListener = new UnityAction<Vector3, float>(FootstepEventHandler);

        stateController = GetComponent<StateController>();
        if (stateController == null)
        {
            Debug.LogError("No state controller attached!");
        }
    }

    void OnEnable()
    {
        EventManager.StartListening<FootstepEvent, Vector3, float>(footstepEventListener);
    }

    void OnDisable()
    {
        EventManager.StopListening<FootstepEvent, Vector3, float>(footstepEventListener);
    }

    void FootstepEventHandler(Vector3 worldPos, float maxDistance)
    {
        if (Vector3.Distance(this.transform.position, worldPos) > maxDistance) return;
        Omega.Debug.DrawCircle(worldPos, transform.up, worldPos + transform.forward * maxDistance, Color.red, 1f);

        stateController.lastSeenPosition = worldPos;
        stateController.HearSound = SoundType.Footstep;
    }
}