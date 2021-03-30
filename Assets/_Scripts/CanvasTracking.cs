using UnityEngine;
using UnityEngine.UI;

public class CanvasTracking : MonoBehaviour
{
    public void Update()
    {
        Camera camera = Camera.main;
        transform.LookAt(transform.rotation * Vector3.forward);

        transform.forward = Camera.main.transform.forward;
    }
}
