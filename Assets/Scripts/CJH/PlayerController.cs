using System.Collections;
using System.Collections.Generic;
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

    private Rigidbody rb;
    private Vector2 moveInput;  // 입력받은 이동값
    private bool jumpInput;     //  점프 입력이 됐는지

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();        
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
        Vector3 horizontalVelocity = desireMoveDir * moveSpeed;
        rb.velocity = new Vector3(horizontalVelocity.x, currentVelocity.y, horizontalVelocity.z);

        // 플레이어 회전
        if (desireMoveDir.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(desireMoveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10f);
        }

        if (jumpInput && IsGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }

        jumpInput = false;

    }

    private bool IsGrounded()
    {
        float rayDistance = 1.1f;
        return Physics.Raycast(transform.position, Vector3.down, rayDistance);
    }


}
