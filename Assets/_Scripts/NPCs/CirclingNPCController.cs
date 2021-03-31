
using UnityEngine;
using FMODUnity;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]

public class CirclingNPCController : HeadController
{
    private Quaternion targetRotation;

    protected override void OnTriggerEnter(Collider other)
    {
        //if "Player" is in the trigger area do this
        if (other.gameObject.CompareTag("Player"))
        {
            //player is close enough to be seen
            playerClose = true;
            Animator.SetBool("PlayerClose", true);

            //this.transform.LookAt(LookObj);

            //should try to fix this... replaces code above
            //targetRotation = Quaternion.LookRotation(LookObj.position);
            //this.transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 0);

            Vector3 targetPosition = new Vector3(LookObj.position.x, this.transform.position.y, LookObj.position.z);
            this.transform.LookAt(targetPosition);

            //if the character has dialogue
            if (hasDialogue)
            {
                //if character has dialogue, enable UI image and set PlayerClose
                uiImage.enabled = true;
            }
            else
            {
                uiImage.enabled = false;
            }

            //if the character has spoken
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        //if "Player" leaves the trigger area do this
        if (other.gameObject.CompareTag("Player"))
        {
            playerClose = false;
            uiImage.enabled = false;
            Animator.SetBool("PlayerClose", false);
        }

        //if player leaves during conversations
        if (!playerClose && isSpeaking)
        {
            //set parameter to snarky remark about cutting off the conversation
            emit.SetParameter("PlayerClose", 0f);
            //set bool that this character has spoken already
            hasSpoken = true;
        }
    }
}