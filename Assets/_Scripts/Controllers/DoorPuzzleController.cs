using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class DoorPuzzleController : MonoBehaviour
{
    public SaxophoneController saxophoneController;
    public Animator animator;
    public PuzzleStatus puzzleStatus;

    [EventRef] public string audioEvent;

    private bool inTrigger = false;
    private bool playedSax = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.transform.parent.GetComponent<Animator>();
        saxophoneController = GameObject.Find("Saxophone").GetComponent<SaxophoneController>();
        puzzleStatus = GetComponentInParent<PuzzleStatus>();
    }

    private void Update()
    {
        PuzzleCheck();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inTrigger = true;
            if (playedSax) { animator.SetBool("isOpening", true); }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inTrigger = false;
            animator.SetBool("isOpening", false);
        }
    }

    void PuzzleCheck()
    {
        if (saxophoneController.isPlaying && inTrigger && !playedSax)
        {
            playedSax = true;
            animator.SetBool("isOpening", true);

            puzzleStatus.isComplete = true;
            puzzleStatus.PuzzleComplete();
        }
    }    
}
