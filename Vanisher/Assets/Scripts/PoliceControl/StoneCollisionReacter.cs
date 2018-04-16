using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class StoneCollisionReacter : MonoBehaviour
{
    private UnityAction<Vector3, float> stoneCollisionEventListener;
    private StateController stateController;

    void Awake()
    {
        stoneCollisionEventListener = new UnityAction<Vector3, float>(StoneCollisionEventHandler);

        stateController = GetComponent<StateController>();
        if (stateController == null)
        {
            Debug.LogError("No state controller attached!");
        }
    }

    void OnEnable()
    {
        EventManager.StartListening<StoneCollisionEvent, Vector3, float>(stoneCollisionEventListener);
    }

    void OnDisable()
    {
        EventManager.StopListening<StoneCollisionEvent, Vector3, float>(stoneCollisionEventListener);
    }

    void StoneCollisionEventHandler(Vector3 worldPos, float maxDistance)
    {
        if (Vector3.Distance(this.transform.position, worldPos) > maxDistance) return;
        Omega.Debug.DrawCircle(worldPos, transform.up, worldPos + transform.forward * maxDistance, Color.red, 1f);

        stateController.lastSeenPosition = worldPos;
        stateController.HearSound = SoundType.Stone;
    }
}