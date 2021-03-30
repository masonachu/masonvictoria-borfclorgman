using UnityEngine;

public class HeadphoneControllerTip : MonoBehaviour
{
    public HeadphoneController headphoneController;

    [HideInInspector] public bool isConnected = false;
    [HideInInspector] public bool isPlaying = false;
    [HideInInspector] public string objectName;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("SocketPuzzle") && !isConnected && !isPlaying)
        {
            if ( other.gameObject.name == "A1" && !isConnected || 
                 other.gameObject.name == "A2" && !isConnected || 
                 other.gameObject.name == "A3" && !isConnected || 
                 other.gameObject.name == "A4" && !isConnected )
            {
                isConnected = true;
            }
        }

        objectName = other.gameObject.name;

        if (isConnected && !isPlaying)
        {

            headphoneController.ChangeEventAccordionly();
            headphoneController.PlayAudioEvent();
            isPlaying = true;

            Debug.Log("event is playing");
        }
}

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SocketPuzzle") && isConnected && isPlaying)
        {
            if (isConnected && isPlaying)
            {
                headphoneController.StopAudioEvent();
                isConnected = false;
                isPlaying = false;

                Debug.Log("event is not playing");

            }
        }

    }
}
