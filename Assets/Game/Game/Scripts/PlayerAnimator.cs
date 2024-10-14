using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";
    private Animator aniamtor;

    [SerializeField]
    private Player player;
    private void Awake()
    {
        aniamtor = GetComponent<Animator>();
        aniamtor.SetBool(IS_WALKING, player.IsWalking());
    }

    private void Update()
    {
        aniamtor.SetBool(IS_WALKING, player.IsWalking());
    }
}
