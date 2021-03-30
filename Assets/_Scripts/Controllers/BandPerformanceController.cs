using UnityEngine;
using FMODUnity;

public class BandPerformanceController : HeadController
{
    private PuzzleStatus SaxPuzzle;
    private bool isComplete = false;
    private bool eventChanged = false;

    public MyStudioEventEmitter SoundZone;

    [Header("Band Performance Specific")]

    [EventRef] public string ev;

    protected override void Start()
    {
        base.Start();
        SaxPuzzle = GameObject.Find("SaxPuzzleZone").GetComponent<PuzzleStatus>();
        SoundZone = this.gameObject.GetComponentInChildren<MyStudioEventEmitter>();

        SoundZone.PlayEvent = EmitterGameEvent.TriggerEnter;
        SoundZone.StopEvent = EmitterGameEvent.TriggerExit;
        SoundZone.CollisionTag = "Player";
        SoundZone.OcclusionEnabled = true;

        SoundZone.ChangeEvent(ev);
        SoundZone.Play();
    }
    protected override void Update()
    {
        base.Update();
        CheckPuzzleStatus();
    }

    private void CheckPuzzleStatus()
    {
        if (SaxPuzzle.isComplete && !isComplete)
        {
            //Animator.SetBool("MissionComplete", true);
            //Animator.SetFloat("CycleOffset", Random.Range(0f, 1f));
            isComplete = true;
        }

        if (SaxPuzzle.isComplete && !eventChanged)
        {
            SoundZone.SetParameter("SaxPuzzleComplete", 1f);
            eventChanged = true;
        }
    }
}
