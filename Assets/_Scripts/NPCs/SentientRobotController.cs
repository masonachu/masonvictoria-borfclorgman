using UnityEngine;
using FMODUnity;

public class SentientRobotController : HeadController
{
    private PuzzleStatus SaxPuzzle;
    private bool isCheering = false;
    private bool eventChanged = false;

    public MyStudioEventEmitter SoundZone;

    [Header("Sentient Robot Specific")]
    [EventRef] public string booEvent;
    [EventRef] public string cheerEvent;

    [EventRef] public string preCompleteVoiceover;
    [EventRef] public string postCompleteVoiceover;

    protected override void Start()
    {
        base.Start();
        SaxPuzzle = GameObject.Find("SaxPuzzleZone").GetComponent<PuzzleStatus>();
    }
    protected override void Update()
    {
        base.Update();
        CheckPuzzleStatus();
    }

    protected override void ConversationCheck()
    {
        if(isCheering && !eventChanged)
        {
            emit.ChangeEvent(postCompleteVoiceover);

            SoundZone.Stop();
            SoundZone.ChangeEvent(cheerEvent);
            SoundZone.Play();
            eventChanged = true;
        }

        base.ConversationCheck();
    }

    protected override void InitializeDialogue()
    {
        if (this.gameObject.CompareTag("HasDialogue"))
        {
            emit = gameObject.AddComponent<StudioEventEmitter>();
            emit.ChangeEvent(preCompleteVoiceover);
            SoundZone.ChangeEvent(booEvent);
            SoundZone.Play();
            hasDialogue = true;
        }
        else
        {
            hasDialogue = false;
        }
    }

    private void CheckPuzzleStatus()
    {
        if(SaxPuzzle.isComplete && !isCheering)
        {
            Animator.SetBool("MissionComplete", true);
            //Animator.SetFloat("CycleOffset", Random.Range(0f, 1f));
            isCheering = true;
        }
    }

    private void OnDestroy()
    {
        SoundZone.Stop();
    }
}
