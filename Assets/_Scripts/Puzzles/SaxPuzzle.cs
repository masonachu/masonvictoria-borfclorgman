using UnityEngine;
using FMODUnity;

//Puzzles require this in order for the game manager to find if the puzzle is complete
[RequireComponent(typeof(PuzzleStatus))]

public class SaxPuzzle : MonoBehaviour
{
    public GameManager GM;
    public SaxophoneController saxophoneController;
    public PuzzleStatus puzzleStatus;
    public MyStudioEventEmitter emit;
    [EventRef] public string ev;

    private bool inTrigger = false;
    private bool playedSax = false;
    private bool isInstantiated = false;

    // Start is called before the first frame update
    void Start()
    {
        puzzleStatus = GetComponent<PuzzleStatus>();
        emit = GetComponent<MyStudioEventEmitter>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();

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
        if(GM.saxophoneActive && !isInstantiated)
        {
            saxophoneController = GameObject.Find("Saxophone(Clone)").GetComponent<SaxophoneController>();
            isInstantiated = true;
        }

        if (isInstantiated)
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
}
