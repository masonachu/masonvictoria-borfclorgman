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
    [HideInInspector] public bool saxophoneActive = false;

    [Header("Saxophone References")]
    public GameObject saxophonePrefab;
    public Transform saxophoneSpawn;
    private bool clipboardActive = false;

    [Header("Controllers")]
    public OculusInput OculusLeft;
    public OculusInput OculusRight;

    [Header("Debug")]
    public bool DebugClipboard;
    public bool DebugSaxophone;

    private bool allTrue = false;


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

        //Change this so its activated after initial speech ... done!
        //kept for debug purposes
        if(DebugClipboard && !clipboardActive && OculusRight.secondary)
        {
            SpawnClipboardAtTransform();
        }
        
        if(DebugSaxophone && !saxophoneActive && OculusRight.secondary)
        {
            SpawnSaxophoneAtTransform();
            Debug.Log("Saxophone Spawned");
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

    public void SpawnClipboardAtTransform()
    {
        if(!clipboardActive)
        {
            GameObject Clipboard = Instantiate(clipboardPrefab, clipboardSpawn.position, clipboardSpawn.rotation);
            Checkboxes = GameObject.FindGameObjectsWithTag("Checkboxes");
            clipboardActive = true;
        }
    }
    
    public void SpawnSaxophoneAtTransform()
    {
        if(!clipboardActive)
        {
            GameObject Saxophone = Instantiate(saxophonePrefab, saxophoneSpawn.position, saxophoneSpawn.rotation);
            saxophoneActive = true;
        }
    }
}
