using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Distract")]
public class Distract : Ability
{
    public GameObject pebble;
    public Vector3 force;
    public float forceMultipler = 30.0f;

    private GameObject player;
    private GameObject throwingPebble;
    private GameObject throwPositionEstimated;
    private LineRenderer lineRenderer;
    private int numSegments = 30;  // use 30 line segments to approximate the trajectory
    private float deltaTime = 0.2f;  // after every 0.2 second draw a line segment

    private float widthMultiplier = 0.05f;

    public override void Start()
    {
        base.Start();
        //Debug.Log("Start in Distract");
        player = GameObject.FindGameObjectWithTag("Player");
        caster = GameObject.FindGameObjectWithTag("LeftHandHold");
        throwPositionEstimated = player.transform.Find("ThrowPosEstimated").gameObject;
        lineRenderer = throwPositionEstimated.GetComponent<LineRenderer>();
        lineRenderer.loop = false;
        lineRenderer.widthMultiplier = widthMultiplier;
        lineRenderer.positionCount = numSegments + 1;

        preview = true;
    }

    public override void PreviewEffect()
    {
        base.PreviewEffect();
        Debug.Log("preview effect in Distract");

        Vector3 initPosition = throwPositionEstimated.transform.position;
        Vector3 initVelocity = player.transform.forward * forceMultipler;
        ShowTrajectory(initPosition, initVelocity);
    }

    public override void ClearPreviewEffect()
    {
        base.ClearPreviewEffect();
        Debug.Log("clear preview effect in Distract");

        lineRenderer.enabled = false;

        preview = false;
    }

    public override bool cast()
    {
        if (currentNumber > 0)
        {
            if (_isInCooldown)
            {
                return false;
            }

            setCastPositon();

            //Debug.Log("player position = " + player.transform.position);
            //Debug.Log("caster position = " + castPosition);
            //Debug.Log("diff = " + (castPosition - player.transform.position));

            if (pebble == null)
            {
                return false;
            }

            throwingPebble = Instantiate<GameObject>(pebble, castPosition, castRotation);

            //GameObject player = GameObject.FindGameObjectWithTag("Player");

            force = player.transform.forward * forceMultipler;

            throwingPebble.GetComponent<Rigidbody>().AddForce(force, ForceMode.VelocityChange);

            _isInCooldown = true;
            setCooldownTimer();

            currentNumber--;
        }
        else
            Debug.Log("not enough " + this.name);

        return true;
    }

    private void ShowTrajectory(Vector3 initPosition, Vector3 initVelocity)
    {
        Debug.Log("show trajectory");
        lineRenderer.enabled = true;
        Vector3 g = Physics.gravity;  // (0, -9.8, 0)
        float gy = g.y;
        float t = 0f;
        Vector3 pos = initPosition;
        lineRenderer.SetPosition(0, pos);
        float x, y, z;
        for (int i = 1; i <= numSegments; ++i)
        {
            t += deltaTime;
            x = initPosition.x + initVelocity.x * t;
            y = initPosition.y + initVelocity.y * t + gy * t * t / 2;
            z = initPosition.z + initVelocity.z * t;
            pos.x = x;
            pos.y = y;
            pos.z = z;

            lineRenderer.SetPosition(i, pos);
        }
    }
}