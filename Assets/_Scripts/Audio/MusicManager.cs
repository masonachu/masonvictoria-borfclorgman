using UnityEngine;
using FMODUnity;

public class MusicManager : MonoBehaviour
{
    [Header("FMOD Events")]
    [EventRef] public string ambience;
    [EventRef] public string music;

    [Header("Debug")]
    public bool m_MuteMusic = false;
    public bool m_MuteAmbience = false;

    private FMOD.Studio.EventInstance amb;
    private FMOD.Studio.EventInstance mus;

    void Awake()
    {
        amb = RuntimeManager.CreateInstance(ambience);
        //mus = RuntimeManager.CreateInstance(music);
        DebugCheck();
    }

    void OnDestroy()
    {
        amb.release();
        mus.release();
    }

    void DebugCheck()
    {
        //Checks to see if Music and/or Ambience is muted
        if (m_MuteMusic == true)
        { Debug.Log("Music Muted in FMOD Manager!"); }
        else { EnableMusic(); }

        //Note that this does not mute any spacialized sound sources i.e. river, trees, etc.
        if (m_MuteAmbience == true)
        { Debug.Log("Ambience Muted in FMOD Manager!"); }
        else { EnableAmb(); }
    }

    void EnableMusic()
    {
        mus.start();
    }

    void EnableAmb()
    {
        amb.start();
    }
}
