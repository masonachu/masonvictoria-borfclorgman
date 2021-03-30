using UnityEngine;
using FMODUnity;

//Puzzles require this in order for the game manager to find if the puzzle is complete
[RequireComponent(typeof(PuzzleStatus))]

public class SaxPuzzle : MonoBehaviour
{
    public SaxophoneController saxophoneController;
    public PuzzleStatus puzzleStatus;
    public MyStudioEventEmitter emit;
    [EventRef] public string ev;

    private bool inTrigger = false;
    private bool playedSax = false;

    // Start is called before the first frame update
    void Start()
    {
        saxophoneController = GameObject.Find("Saxophone").GetComponent<SaxophoneController>();
        puzzleStatus = GetComponent<PuzzleStatus>();
        emit = GetComponent<MyStudioEventEmitter>();

        emit.ChangeEvent(ev);
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
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inTrigger = false;
        }
    }

    void PuzzleCheck()
    {
        if (saxophoneController.isPlaying && inTrigger && !playedSax)
        {
            playedSax = true;

            puzzleStatus.isComplete = true;
            puzzleStatus.PuzzleComplete();

            emit.Play();
        }
    }
}
