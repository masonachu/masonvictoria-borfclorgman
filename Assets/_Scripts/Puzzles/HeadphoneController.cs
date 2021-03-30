using UnityEngine;
using FMODUnity;

public class HeadphoneController : MonoBehaviour
{
    private string ev;
    private StudioEventEmitter emit;

    [EventRef] public string event1;
    [EventRef] public string event2;
    [EventRef] public string event3;
    [EventRef] public string event4;

    public HeadphoneControllerTip tip;

    private void Start()
    {
        gameObject.AddComponent<StudioEventEmitter>();
        emit = GetComponent<StudioEventEmitter>();
    }

    public void ChangeEventAccordionly()
    {
        if(tip.objectName == "A1")
        {
            ev = event1;
        }
        
        if(tip.objectName == "A2")
        {
            ev = event2;
        }
        
        if(tip.objectName == "A3")
        {
            ev = event3;
        }
        
        if(tip.objectName == "A4")
        {
            ev = event4;
        }

//        Debug.Log("emitter event changed to " + ev);
    }

    public void PlayAudioEvent()
    {
        if(tip.isConnected && !tip.isPlaying)
        {
            emit.ChangeEvent(ev);
            emit.Play();
        }

    }

    public void StopAudioEvent()
    {
            emit.Stop();
    }
}
