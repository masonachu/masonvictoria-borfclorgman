using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Mission References")]
    public GameObject[] Puzzles;
    public GameObject[] Checkboxes;
    public bool[] puzzleStatus;
    public bool[] checkmarks;

    [Header("Clipboard References")]
    public GameObject clipboardPrefab;
    public Transform clipboardSpawn;

    [Header("Controllers")]
    public OculusInput OculusLeft;
    public OculusInput OculusRight;

    private bool allTrue = false;
    private bool clipboardActive = false;


    // Start is called before the first frame update
    void Start()
    {
        Puzzles = GameObject.FindGameObjectsWithTag("Puzzle");
        puzzleStatus = new bool[Puzzles.Length];

        checkmarks = new bool [Puzzles.Length];

        OculusLeft = GameObject.Find("Left Hand").GetComponent<OculusInput>();
        OculusRight = GameObject.Find("Right Hand").GetComponent<OculusInput>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPuzzleStatus();

        //Change this so its activated after initial speech
        if(!clipboardActive && OculusRight.secondary)
        {
            SpawnClipboardAtTransform();
            Checkboxes = GameObject.FindGameObjectsWithTag("Checkboxes");
            clipboardActive = true;
        }
    }

    void CheckPuzzleStatus()
    {
        // Check if a mission is completed
        for (int i = 0; i < Puzzles.Length; i++)
        {
            puzzleStatus[i] = Puzzles[i].GetComponent<PuzzleStatus>().isComplete;
            
            if(clipboardActive)
            {
                checkmarks[i] = puzzleStatus[i];
                Checkboxes[i].GetComponentInChildren<CheckmarkActivator>().isActivated = checkmarks[i];
            }
        }
    }

    void SpawnClipboardAtTransform()
    {
        GameObject Clipboard = Instantiate(clipboardPrefab, clipboardSpawn.position, clipboardSpawn.rotation);
    }
}
