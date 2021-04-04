using UnityEngine;
using FMODUnity;

public class SaxophoneController : MonoBehaviour
{
    public StudioEventEmitter Saxophone;

    public bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        Saxophone = gameObject.transform.Find("Mouthpiece Trigger").GetComponent<StudioEventEmitter>();
    }

    // Update is called once per frame
    void Update()
    {
        SaxStatus();
    }
    void SaxStatus()
    {
        if (Saxophone.IsPlaying() && !isPlaying)
        {
            isPlaying = true;
        }
        
        if (!Saxophone.IsPlaying() && isPlaying)
        {
            isPlaying = false;
        }
    }
}
