using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";

    [SerializeField] private ContainerCounter containerCounter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.OnPlayerGrabbedObejct += ContainerCounter_OnPlayerGrabbedObejct;
    }

    private void ContainerCounter_OnPlayerGrabbedObejct(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
