using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AnimationHandler : MonoBehaviour
{
    private static readonly int IsMove = Animator.StringToHash("IsMove");
    private static readonly int IsRun = Animator.StringToHash("IsRun");
    private static readonly int JumpTrigger = Animator.StringToHash("JumpTrigger");
    private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");


    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void TriggerJump()
    {
        animator.SetTrigger(JumpTrigger);
    }

    
    public void SetMoveState(float speed)
    {
        animator.SetFloat(IsMove, speed);
        Debug.Log($"[애니메이션] IsMove: {speed}");
    }

}
