using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class ModularPlugScript : MonoBehaviour
{
    [HideInInspector] public bool isConnected = false;
    [HideInInspector] public string objectName;

    [EventRef] private string sfx = "event:/Puzzles/Modular/Plug In Sound";

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == gameObject.name && !isConnected)
        {
            isConnected = true;
//          Debug.Log(other.gameObject.name + " is true");
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == gameObject.name && isConnected)
        {
            isConnected = false;
//          Debug.Log(other.gameObject.name + " is false");
        }
    }

    public void PlaySound()
    {
        RuntimeManager.PlayOneShot(sfx, transform.position);
    }
}
