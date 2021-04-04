using UnityEngine;

public class HeadphoneControllerTip : MonoBehaviour
{
    public HeadphoneController headphoneController;

    [HideInInspector] public string objectName;
    [HideInInspector] public bool m_isConnected;
    [HideInInspector] public bool isPlaying = false;
    [HideInInspector] public bool isConnected { get { return m_isConnected; } set { m_isConnected = value; } }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SocketPuzzle") && m_isConnected && !isPlaying)
        {
            objectName = other.gameObject.name;
            headphoneController.ChangeEventAccordionly();
            headphoneController.PlayAudioEvent();
            isPlaying = true;

            //Debug.Log("event is playing");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (!m_isConnected && isPlaying)
        {
            headphoneController.StopAudioEvent();
            isPlaying = false;
        }
    }
}
