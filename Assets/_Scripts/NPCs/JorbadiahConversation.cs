using System.Collections;
using UnityEngine;
using FMODUnity;

public class JorbadiahConversation : HeadController
{
    public GameManager GM;
    public PuzzleStatus PuzzleStatus;
    public StudioEventEmitter CryingEmitter;

    private IEnumerator coroutine;
    private bool coroutineActive;
    private bool saxophoneLoaded;

    protected override void Start()
    {
        base.Start();
        PuzzleStatus = GetComponentInParent<PuzzleStatus>();

        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        CryingEmitter = transform.Find("SoundZone").gameObject.GetComponent<StudioEventEmitter>();
    }

    protected override void ConversationCheck()
    {
        //if button is pressed, conversation starts
        if (playerClose && OculusLeft.primary && !isSpeaking || playerClose && OculusRight.primary && !isSpeaking)
        {
            emit.Play();
            if (emit.IsPlaying())
            {
                isSpeaking = true;
                CryingEmitter.Stop();

                if (isSpeaking && !saxophoneLoaded && !coroutineActive && !hasSpoken)
                {
                    coroutine = LoadSaxophone(43f);
                    StartCoroutine(coroutine);
                    coroutineActive = true;
                }

                if (isSpeaking && !saxophoneLoaded && !coroutineActive && hasSpoken)
                {
                    coroutine = LoadSaxophone(3f);
                    StartCoroutine(coroutine);
                    coroutineActive = true;
                }
            }
        }

        if (!emit.IsPlaying() && isSpeaking)
        {
            isSpeaking = false;
            CryingEmitter.Play();
        }

        if (hasSpoken)
        {
            emit.SetParameter("HasSpoken", 1f);
        }

        //If You speak to Jorbadiah, the puzzle is complete
        if (isSpeaking && !PuzzleStatus.isComplete)
        {
            PuzzleStatus.isComplete = true;
            PuzzleStatus.PuzzleComplete();
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        //if "Player" leaves the trigger area do this
        if (other.gameObject.CompareTag("Player"))
        {
            playerClose = false;
            uiImage.enabled = false;
        }

        //if player leaves during conversations
        if (!playerClose && isSpeaking)
        {
            //set parameter to snarky remark about cutting off the conversation and stop the clipboard from loading
            emit.SetParameter("PlayerClose", 0f);
            if (coroutineActive && !saxophoneLoaded)
            {
                StopCoroutine(coroutine);
                coroutineActive = false;
                Debug.Log("couroutine cancelled");
            }

            //set bool that this character has spoken already
            hasSpoken = true;
        }
    }

    public IEnumerator LoadSaxophone(float waitTime)
    {
        print("couroutine started with a time of " + waitTime);
        yield return new WaitForSeconds(waitTime);
        GM.SpawnSaxophoneAtTransform();
        saxophoneLoaded = true;
        print("coroutine complete in " + waitTime);
    }
}
