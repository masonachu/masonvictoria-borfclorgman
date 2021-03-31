using UnityEngine;
using FMODUnity;

public class FMODAudio : MonoBehaviour
{
    void sfx(string sound)
    {
        RuntimeManager.PlayOneShot(sound, transform.position);
    }    
}
