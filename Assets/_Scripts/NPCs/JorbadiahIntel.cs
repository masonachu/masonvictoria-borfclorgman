using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class JorbadiahIntel : HeadController
{
    public PuzzleStatus PuzzleStatus;
    public StudioEventEmitter CryingEmitter;

    protected override void Start()
    {
        base.Start();
        PuzzleStatus = GetComponentInParent<PuzzleStatus>();
        CryingEmitter = transform.Find("SoundZone").gameObject.GetComponent<StudioEventEmitter>();
    }

    protected override void ConversationCheck()
    {
        //if button is pressed, conversation starts
        if (playerClose && OculusLeft.primary && !isSpeaking || playerClose && OculusRight.primary && !isSpeaking)
        {
            emit.Play();
            if (emit.IsPlaying())
            {
                isSpeaking = true;
                CryingEmitter.Stop();
            }
        }

        if (!emit.IsPlaying() && isSpeaking)
        {
            isSpeaking = false;
            CryingEmitter.Play();
        }

        if (hasSpoken)
        {
            emit.SetParameter("HasSpoken", 1f);
        }

        //If You speak to Jorbadiah, the puzzle is complete
        if (isSpeaking && !PuzzleStatus.isComplete)
        {
            PuzzleStatus.isComplete = true;
            PuzzleStatus.PuzzleComplete();
        }
    }
}
