using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleStatus : MonoBehaviour
{
    public bool isComplete = false;

    public virtual void PuzzleComplete()
    {
        if(isComplete)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Music/Enter Stinger", transform.position);
            Debug.Log("Mission Completed");
        }
    }
}
