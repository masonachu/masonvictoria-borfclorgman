using UnityEngine;
using FMODUnity;

public class SongPlay : MonoBehaviour
{
    public PuzzleStatus PuzzleStatus;
    public MyStudioEventEmitter emit;
    [EventRef] public string ev;

    // Start is called before the first frame update
    void Start()
    {
        emit = GetComponent<MyStudioEventEmitter>();
        emit.ChangeEvent(ev);
        PuzzleStatus = GetComponent<PuzzleStatus>();
    }

    private void Update()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        if(PuzzleStatus.isComplete && !emit.IsPlaying())
        {
            emit.Play();
        }
    }
}
