using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOcclusion : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;

    [FMODUnity.EventRef]
    public string fmodEvent;

    [Header("Occlusion Settings")]
    [SerializeField]
    private bool occlusionEnabled = false;

    [SerializeField]
    private string occlusionParameterName = null;

    [Range(0.0f, 10.0f)]
    [SerializeField]
    private float occlusionIntensity = 1f;
    private float currentOcclusion = 0.0f;
    private float nextOcclusionUpdate = 0.0f;

    private bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        StartPlaying();
    }

    void Update()
    {
        if (instance.isValid())
        {
            instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.gameObject));

            if (!occlusionEnabled)
            {
                currentOcclusion = 0.0f;
            }
            else if (Time.time >= nextOcclusionUpdate)
            {
                nextOcclusionUpdate = Time.time + FmodResonanceAudio.occlusionDetectionInterval;
                currentOcclusion = occlusionIntensity * FmodResonanceAudio.ComputeOcclusion(transform);
                instance.setParameterByName(occlusionParameterName, currentOcclusion);
            }
        }
    }

    void StopPlaying()
    {
        if(isPlaying)
        {
            instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            //instance.release();
            isPlaying = false;
        }
    }

    void StartPlaying()
    {
        if(!isPlaying)
        {
            instance.start();
            isPlaying = true;
        }
    }

    public void ResetRadio()
    {
        if(isPlaying)
        {
            StopPlaying();
            StartPlaying();
        }    
    }
}