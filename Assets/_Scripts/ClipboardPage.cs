using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClipboardPage : MonoBehaviour
{
    public Image checkmark;

    private void Start()
    {
        checkmark = GetComponent<Image>();
    }

    void ActivateCheckmark()
    {
        checkmark.enabled = true;
    }
}
