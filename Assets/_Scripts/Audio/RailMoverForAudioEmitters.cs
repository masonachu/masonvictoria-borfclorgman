using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailMoverForAudioEmitters : MonoBehaviour
{
    //The rail used for the emitter
    public RailForAudioEmitters rail;
    public Transform listener;
    // controls rotation, so the emitter is always facing the player
    public bool lookAtPlayer = false;
    public bool smoothMove = true;
    public float moveSpeed = 5.0f;

    private Transform thisTransform;
    private Vector3 lastPosition;

    private void Start()
    {
        thisTransform = transform;
        lastPosition = thisTransform.position;
    }

    private void Update()
    {
        if (smoothMove)
        {
            lastPosition = Vector3.Lerp(lastPosition, rail.ProjectPositionOnRail(listener.position), Time.deltaTime * moveSpeed);
            thisTransform.position = lastPosition;
        }
        else
            thisTransform.position = rail.ProjectPositionOnRail(listener.position);


        // controls rotation, so the emitter is always faceing the player
        if (lookAtPlayer)
        {
            thisTransform.LookAt(listener.position);
        }
    }

}
