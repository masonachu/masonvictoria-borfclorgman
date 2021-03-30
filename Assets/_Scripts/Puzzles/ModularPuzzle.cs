using UnityEngine;

public class ModularPuzzle : MonoBehaviour
{
    public GameObject[] sockets;
    public bool[] boolContainer;

    private bool allTrue = false;
    private PuzzleStatus puzzleStatus;

    void Awake()
    {
        sockets = GameObject.FindGameObjectsWithTag("SocketPuzzle");
        boolContainer = new bool[sockets.Length];

        puzzleStatus = GetComponentInParent<PuzzleStatus>();
    }

    private void Update()
    {
        CheckPuzzleStatus();
    }

    void CheckPuzzleStatus()
    {
        for (int i = 0; i < sockets.Length; i++)
        {
            boolContainer[i] = sockets[i].GetComponent<ModularPlugScript>().isConnected;
        }

        foreach (bool b in boolContainer)
        {
            if (b)
            {
                allTrue = true;
            }
            else
            {
                allTrue = false;
                break;
            }
        }

        if(allTrue)
        {
            if (allTrue && !puzzleStatus.isComplete)
            {
                //Debug.Log("Puzzle Completed");
                puzzleStatus.isComplete = true;
                puzzleStatus.PuzzleComplete();
            }
        }
    }
}
