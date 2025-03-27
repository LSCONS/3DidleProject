using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float monveSpeed = 5f;
    public float gravity = -9.8f;
    public float jumpForce = 5f;

    [Header("Refernces")]
    public Transform cameraTransform;

    private Vector2 moveInput;
    private bool jumpInput;
    private Vector3 velocity;

    private void Awake()
    {
        
    }

}
