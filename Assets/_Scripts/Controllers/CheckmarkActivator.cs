using UnityEngine;
using UnityEngine.UI;

public class CheckmarkActivator : MonoBehaviour
{
    private Image Checkmark;

    public bool isActivated;

    private void Start()
    {
        Checkmark = GetComponent<Image>();
        Checkmark.enabled = false;
    }

    private void Update()
    {
        CheckmarkStatus();
    }

    public void CheckmarkStatus()
    {
        if(isActivated)
        {
            Checkmark.enabled = true;
        }
    }
}
