
using UnityEngine;
using FMODUnity;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]

public class HeadController : MonoBehaviour
{
    [Header("Debug")]
    public bool ikActive = true;
    public bool hideCanvas;

    [Header("References")]

    public Animator Animator;
    public GameObject CameraObj;
    public StudioEventEmitter emit;
    public Image uiImage;

    [HideInInspector] public bool playerClose = false;
    [HideInInspector] public Transform LookObj = null;

    protected float lookAt = 2.0f;
    protected float lookAway = 0.0f;
    protected float lookSpeed = 0;

    [Header("Controllers")]

    public OculusInput OculusLeft;
    public OculusInput OculusRight;

    [Header("Dialogue")]

    [EventRef] public string dialogue = null;
    protected bool hasDialogue = false;
    protected bool hasSpoken = false;
    protected bool isSpeaking = false;

    protected virtual void Start()
    {
        Animator = GetComponentInChildren<Animator>();
        uiImage = GetComponentInChildren<Image>();
        OculusLeft = GameObject.Find("Left Hand").GetComponent<OculusInput>();
        OculusRight = GameObject.Find("Right Hand").GetComponent<OculusInput>();
        CameraObj = GameObject.Find("VR Camera");
        LookObj = CameraObj.transform;
        ikActive = true;

        uiImage.enabled = false;
        InitializeDialogue();
    }

    protected virtual void Update()
    {
        if (hasDialogue)
        {
            ConversationCheck();
        }
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if (Animator)
        {
            Animator.SetLookAtPosition(LookObj.position);

            //if the IK is active, set the position and rotation directly to the goal. 
            if (ikActive && playerClose)
            {
                // Set the look target position, if one has been assigned
                if (LookObj != null)
                {
                    if(lookSpeed < lookAt)
                    { lookSpeed += 2f * Time.deltaTime; }
                    Animator.SetLookAtWeight(lookSpeed);
                }
            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else
            {
                if(ikActive && !playerClose)
                {
                    if (lookSpeed > lookAway)
                    { lookSpeed -= 2f * Time.deltaTime; }
                    Animator.SetLookAtWeight(lookSpeed);
                }
            }
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        //if "Player" is in the trigger area do this
        if (other.gameObject.CompareTag("Player"))
        {
            //player is close enough to be seen
            playerClose = true;

            //if the character has dialogue
            if (hasDialogue && !hideCanvas)
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

    protected virtual void OnTriggerExit(Collider other)
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
            //set parameter to snarky remark about cutting off the conversation
            emit.SetParameter("PlayerClose", 0f);
            //set bool that this character has spoken already
            hasSpoken = true;
        }
    }

    protected virtual void InitializeDialogue()
    {
        if (this.gameObject.CompareTag("HasDialogue") && dialogue != null)
        {
            emit = gameObject.AddComponent<StudioEventEmitter>();
            emit.ChangeEvent(dialogue);
            hasDialogue = true;
        }
        else
        {
            hasDialogue = false;
        }

        if (this.gameObject.CompareTag("HasDialogue") && dialogue == null)
        {
            Debug.Log("There is no dialogue in the string!");
        }
    }

    protected virtual void ConversationCheck()
    {
        //if button is pressed, conversation starts
        if (playerClose && OculusLeft.primary && !isSpeaking || playerClose && OculusRight.primary && !isSpeaking)
        {
            emit.Play();
            if(emit.IsPlaying())
            {
                isSpeaking = true;
            }
        }

        if (!emit.IsPlaying() && isSpeaking)
        {
            isSpeaking = false;
        }

        if(hasSpoken)
        {
            emit.SetParameter("HasSpoken", 1f);
        }
    }
}