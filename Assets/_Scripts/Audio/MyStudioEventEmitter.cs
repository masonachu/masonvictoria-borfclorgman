using UnityEngine;
using FMODUnity;
using System;

public class MyStudioEventEmitter : StudioEventEmitter
{
    //-- Start of custom occlusion settings --//

    public bool OcclusionEnabled = false;
    [SerializeField]
    protected string OcclusionParameterName = "Occlusion";
    [Range(0.0f, 10.0f)]

    [SerializeField]
    protected float OcclusionIntensity = 1f;
    protected float CurrentOcclusion = 0.0f;
    protected float NextOcclusionUpdate = 0.0f;

    //-- End of custom occlusion settings --//
    protected void OcclusionUpdate()
    {
        if (Time.time >= NextOcclusionUpdate)
        {
            NextOcclusionUpdate = Time.time + FmodResonanceAudio.occlusionDetectionInterval;
            CurrentOcclusion = OcclusionIntensity * FmodResonanceAudio.ComputeOcclusion(transform);
            instance.setParameterByName(OcclusionParameterName, CurrentOcclusion);
        }
    }

    private void Update()
    {
        if (!OcclusionEnabled)
        {
            CurrentOcclusion = 0.0f;
        }
        else
        {
            OcclusionUpdate();
        }
    }
}
