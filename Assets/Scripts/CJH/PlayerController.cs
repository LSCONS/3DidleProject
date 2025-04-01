using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;       // 이동속도  
    public float jumpForce = 5f;        // 점프력

    [Header("Refernces")]
    public Transform cameraTransform;

    private PlayerAnimationHandler animationHandler;
    private Rigidbody rb;
    private Vector2 moveInput;  // 입력받은 이동값
    private bool jumpInput;     //  점프 입력이 됐는지
    private bool isRunning;
    private bool isAttacking;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();        
        animationHandler = GetComponent<PlayerAnimationHandler>();
    }



    public void OnMove(InputAction.CallbackContext context)
    {

        moveInput = context.ReadValue<Vector2>();
 

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumpInput = true;
        }
        
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isRunning = true;
            animationHandler?.SetRunState(true);
        }
        else if (context.canceled)
        {
            isRunning = false;
            animationHandler?.SetRunState(false);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed || !isAttacking)
        {
            isAttacking = true;
            animationHandler?.PlayAttack();
        }
    }

    public void ResetAttackState()
    {
        isAttacking = false;
    }


    private void FixedUpdate()
    {

        Vector3 desireMoveDir = Vector3.zero;
        if (cameraTransform != null)
        {
            Vector3 camForward = cameraTransform.forward;
            camForward.y = 0;
            camForward.Normalize();

            Vector3 camRight = cameraTransform.right;
            camRight.y = 0;
            camRight.Normalize();


            desireMoveDir = camRight * moveInput.x + camForward * moveInput.y;
        }
        else
        {
            desireMoveDir = new Vector3(moveInput.x, 0, moveInput.y);
        }
        // 수평 속도 계산
        Vector3 currentVelocity = rb.velocity;
        Vector3 horizontalVelocity = desireMoveDir * (isRunning ? moveSpeed * 1.5f : moveSpeed);
        rb.velocity = new Vector3(horizontalVelocity.x, currentVelocity.y, horizontalVelocity.z);


        // 플레이어 회전
        Vector3 moveDir = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (moveDir.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10f);
        }

        if (jumpInput && IsGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }

        float speed = moveInput.sqrMagnitude;
        animationHandler?.SetMoveState(speed);

        jumpInput = false;

    }

    private bool IsGrounded()
    {
        float rayDistance = 1.1f;
        return Physics.Raycast(transform.position, Vector3.down, rayDistance);
    }


}
