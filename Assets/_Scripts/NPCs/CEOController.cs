using UnityEngine;
using FMODUnity;

public class CEOController : HeadController
{
    [Header("CEO Specific")]
    public PuzzleStatus PuzzleStatus;
    [EventRef] public string newEvent;

    private bool isComplete = false;

    protected override void Start()
    {
        base.Start();            
        Animator = GetComponent<Animator>();
        PuzzleStatus = GameObject.Find("Modular Puzzle").GetComponent<PuzzleStatus>();
    }

    protected override void Update()
    {
        base.Update();
        CheckPuzzleStatus();
    }

    private void CheckPuzzleStatus()
    {
        if(PuzzleStatus.isComplete && !isComplete)
        {
            Animator.SetBool("MissionComplete", true);
            //Animator.SetFloat("CycleOffset", Random.Range(0f, 1f));
            isComplete = true;
        }
    }

    protected override void ConversationCheck()
    {
        if (isComplete && !isSpeaking)
        {
            emit.Stop();
            emit.ChangeEvent(newEvent);
        }

        base.ConversationCheck();
        if(isComplete && !isSpeaking)
        {
            emit.Stop();
            emit.ChangeEvent(newEvent);
        }
    }
}
