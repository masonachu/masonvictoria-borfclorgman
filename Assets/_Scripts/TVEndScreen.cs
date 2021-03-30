using UnityEngine;
using UnityEngine.UI;

public class TVEndScreen : MonoBehaviour
{
    private PuzzleStatus PuzzleStatus;
    private SpriteRenderer EndImage;

    // Start is called before the first frame update
    void Start()
    {
        PuzzleStatus = GameObject.Find("Modular Puzzle").GetComponent<PuzzleStatus>();
        EndImage = GetComponentInChildren<SpriteRenderer>();
        EndImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckStatus();
    }

    void CheckStatus()
    {
        if(PuzzleStatus.isComplete)
        {
            EndImage.enabled = true;
        }
    }
}
