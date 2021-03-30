using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleActiveScript : MonoBehaviour
{
    protected bool isComplete = false;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PuzzleCompleted();
        }
    }
    public void PuzzleCompleted()
    {
        isComplete = true;
    }
}
