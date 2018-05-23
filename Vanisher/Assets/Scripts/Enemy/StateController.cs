using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum SoundType { None, Stone, Footstep };

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class StateController : MonoBehaviour
{
    [HideInInspector] public Animator animator;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public UnityStandardAssets.Characters.ThirdPerson.AICharacterControl characterControl;

    public State currentState;
    public EnemyStats enemyStats;
    public State remainState;
    public State idleState;
    public List<Transform> waypoints;
    public int nextWaypoint;
    [HideInInspector] public Transform viewpoint;  // The eye position of the enemy

    public Transform target;
    public Vector3 lastSeenPosition;
    [HideInInspector] public SpriteRenderer stateIcon;
    [HideInInspector] public Light spotLight;

    public bool isBlind;
    public SoundType HearSound { get; set; }

    [SerializeField] private float stateTimeElapsed;
    private bool isActive;

    void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("PoliceManager:ERR: cannot find Animator component.");
        }

        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.LogError("PoliceController:ERR: cannot find NavMeshAgent component.");
        }

        characterControl = GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>();
        if (characterControl == null)
        {
            Debug.LogError("PoliceController:ERR: cannot find AICharacterControl component.");
        }

        viewpoint = transform.Find("swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:Neck/swat:Neck1/swat:Head/Viewpoint");
        if (viewpoint == null)
        {
            Debug.LogError("PoliceController:ERR: cannot find child game object Viewpoint.");
        }

        stateIcon = transform.Find("swat:Hips/swat:Spine/swat:Spine1/swat:Spine2/swat:Neck/swat:Neck1/swat:Head/swat:HeadTop_End/HeadTop/StateIcon").GetComponent<SpriteRenderer>();
        if (stateIcon == null)
        {
            Debug.LogError("PoliceController:ERR: cannot find child game object StateIcon.");
        }

        spotLight = transform.Find("Spotlight").GetComponent<Light>();
        if (spotLight == null)
        {
            Debug.LogError("PoliceController:ERR: cannot find child game object Spotlight.");
        }
        spotLight.spotAngle = enemyStats.lookAngle;
        spotLight.range = enemyStats.lookRange;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("PoliceManager:ERR: cannot find Player gameobject.");
        }
        target = player.transform;

        HearSound = SoundType.None;
        isBlind = false;
    }

    public void SetupAI(bool isActive, List<Transform> waypoints)
    {
        this.waypoints = waypoints;
        this.isActive = isActive;
        if (this.isActive)
        {
            navMeshAgent.enabled = true;
        }
        else
        {
            navMeshAgent.enabled = false;
        }
    }

    public void TransitionToState(State nextState)
    {
        if (nextState != remainState)
        {
            OnExitState();
            currentState.OnExit(this);
            currentState = nextState;
            currentState.OnEnter(this);
        }
    }

    public bool CheckIfCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return stateTimeElapsed >= duration;
    }

    private void OnExitState()
    {
        stateTimeElapsed = 0;
    }

    void Update()
    {
        if (!isActive)
            return;

        currentState.UpdateState(this);

        stateIcon.color = currentState.sceneGizmoColor;
        spotLight.color = currentState.sceneGizmoColor;
        //Debug.DrawLine(viewpoint.position, viewpoint.forward * enemyStats.lookSphereCastRadius, currentState.sceneGizmoColor);
    }

    void OnDrawGizmosSelected()
    {
        if (currentState != null && viewpoint != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawSphere(viewpoint.position + viewpoint.forward * enemyStats.lookSphereCastRadius * 2.5f, enemyStats.lookSphereCastRadius);
            Gizmos.DrawWireSphere(viewpoint.position, enemyStats.minAlertRadius);
        }
    }

}