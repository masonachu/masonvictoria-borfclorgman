using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class DoorController : MonoBehaviour
{
    private Animator _doorAnim;

    [EventRef] public string sfx;

    // Start is called before the first frame update
    void Start()
    {
        _doorAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _doorAnim.SetBool("isOpening", true);
            //PlaySound(sfx);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _doorAnim.SetBool("isOpening", false);
            //PlaySound(sfx);
        }
    }

    void PlaySound()
    {
        RuntimeManager.PlayOneShot(sfx, transform.position);
    }
}
