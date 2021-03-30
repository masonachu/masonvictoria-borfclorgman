using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdOffset : MonoBehaviour
{
    private Animator Animator;

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponentInChildren<Animator>();
        Animator.SetFloat("CycleOffset", Random.Range(0f, 1f));
    }
}
