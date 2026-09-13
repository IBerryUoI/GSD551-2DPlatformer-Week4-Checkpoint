using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 1.0f;
    
    [SerializeField]
    private float jumpSpeed = 5.0f;

    [SerializeField]
    private Rigidbody2D playerRigidBody;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private float groundCheckRadius = 0.2f;

    [SerializeField]
    private LayerMask groundLayer;

    private float horizontalInput;
    private bool isGrounded;
    private bool jumpPressed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Keyboard.current.dKey.ReadValue() - Keyboard.current.aKey.ReadValue();
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            jumpPressed = true;
        }
        
    }

    private void FixedUpdate()
    {
        CheckGround();
        Move(horizontalInput);

        if (jumpPressed)
        {
            Jump();
            jumpPressed = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
        {
            Debug.Log("Hey! I don't have a reference to my GroundCheck!");
            return;
        }

        if (isGrounded)
        {
            Gizmos.color = Color.green;    
        }
        else
        {
            Gizmos.color = Color.red;
        }
        
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        
    }

    private void Move(float movementInput)
    {
        float horizontalVelocity = movementInput * movementSpeed;
        Vector2 movementVelocity = new Vector2(horizontalVelocity, playerRigidBody.linearVelocity.y);
        playerRigidBody.linearVelocity = movementVelocity;
    }

    private void Jump()
    {
        if (isGrounded == false)
        {
            return;
        }

        Vector2 jumpVelocity = new Vector2(playerRigidBody.linearVelocity.x, jumpSpeed);
        playerRigidBody.linearVelocity = jumpVelocity;
    }

    private void CheckGround()
    {
        Collider2D detectedGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isGrounded = detectedGround != null;
    }
}
