using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcActivator : MonoBehaviour
{
    public GameObject npcs;

    private bool inTrigger;

    private void Start()
    {
        if(!npcs.gameObject.CompareTag("NPC"))
        {
            Debug.Log("NPC Tag is not on this game object!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(npcs.gameObject.CompareTag("NPC") && other.gameObject.CompareTag("Player"))
        {
            npcs.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (npcs.gameObject.CompareTag("NPC") && other.gameObject.CompareTag("Player"))
        {
            npcs.SetActive(false);
        }
    }
}
