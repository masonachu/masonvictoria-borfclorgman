using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptionistController : HeadController
{
    public GameManager GM;

    private IEnumerator coroutine;
    public bool coroutineActive;
    public bool clipboardLoaded;

    protected override void Start()
    {
        base.Start();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
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

                if(isSpeaking && !clipboardLoaded && !coroutineActive && !hasSpoken)
                {
                    coroutine = LoadClipboard(35f);
                    StartCoroutine(coroutine);
                    coroutineActive = true;
                }

                if (isSpeaking && !clipboardLoaded && !coroutineActive && hasSpoken)
                {
                    coroutine = LoadClipboard(20f);
                    StartCoroutine(coroutine);
                    coroutineActive = true;
                }
            }
        }

        if (!emit.IsPlaying() && isSpeaking)
        {
            isSpeaking = false;
        }

        if (hasSpoken)
        {
            emit.SetParameter("HasSpoken", 1f);
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
            if (coroutineActive && !clipboardLoaded)
            {
                StopCoroutine(coroutine);
                coroutineActive = false;
                print("couroutine cancelled");
            }

            //set bool that this character has spoken already
            hasSpoken = true;
        }
    }

    public IEnumerator LoadClipboard(float waitTime)
    {
        print("couroutine started with a time of " + waitTime);
        yield return new WaitForSeconds(waitTime);
        GM.SpawnClipboardAtTransform();
        clipboardLoaded = true;
        print("coroutine complete in " + waitTime);
    }
}
