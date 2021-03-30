using UnityEngine;
using FMODUnity;

public class SaxChangeEvent : MonoBehaviour
{
    public StudioEventEmitter Saxophone;
    public PuzzleStatus PuzzleStatus;

    [EventRef] public string outsidePerformance;
    [EventRef] public string insidePerformance;

    public bool isComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        Saxophone = GetComponentInChildren<StudioEventEmitter>();
        PuzzleStatus = GameObject.Find("SaxPuzzleZone").GetComponent<PuzzleStatus>();

        Saxophone.ChangeEvent(outsidePerformance);
        Saxophone.PlayEvent = EmitterGameEvent.TriggerEnter;
    }

    // Update is called once per frame
    void Update()
    {
        if (PuzzleStatus.isComplete && !isComplete)
        {
            Saxophone.Stop();
            Saxophone.ChangeEvent(insidePerformance);
            Saxophone.Play();

            isComplete = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mouth"))
        {
            Saxophone.SetParameter("Volume", 1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Mouth"))
        {
            Saxophone.SetParameter("Volume", 0f);
        }
    }    
}
