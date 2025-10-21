using System.Collections.Generic;
using UnityEngine;

/*
    This script provides jumping and movement in Unity 3D - Gatsby
*/

public class Player : MonoBehaviour
{
    // Camera Rotation
    public float mouseSensitivity = 2f;
    private float verticalRotation = 0f;
    public Transform cameraTransform;

    // Ground Movement
    private Rigidbody rb;
    public float MoveSpeed = 5f;
    private float moveHorizontal;
    private float moveForward;

    // Jumping
    public float jumpForce = 10f;
    public float fallMultiplier = 2.5f; // Multiplies gravity when falling down
    public float ascendMultiplier = 2f; // Multiplies gravity for ascending to peak of jump
    private bool isGrounded = true;
    public LayerMask groundLayer;
    private float groundCheckTimer = 0f;
    private float groundCheckDelay = 0.3f;
    private float playerHeight;
    private float raycastDistance;
    
    // Narrative 
    private bool canMove;
    public List<AudioClip> playerSounds;
    private AudioSource SoundPlayer;
    private float stepCounter = 0;
    public float stepInterval = 0.2f;


    public void LockPlayer()
    {
        canMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void FreePlayer()
    {
        canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        

        // Set the raycast to be slightly beneath the player's feet
        playerHeight = GetComponent<CapsuleCollider>().height * transform.localScale.y;
        raycastDistance = (playerHeight / 2) + 0.2f;

        SoundPlayer = GetComponent<AudioSource>();

        // Hides the mouse
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
        
        LockPlayer();
    }

    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveForward = Input.GetAxisRaw("Vertical");

        if (canMove == true)
        {
            RotateCamera();
            
        }

        if (Input.GetButtonDown("Jump") && isGrounded && canMove)
        {
            Jump();
        }

        // Checking when we're on the ground and keeping track of our ground check delay
        if (!isGrounded && groundCheckTimer <= 0f)
        {
            Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
            isGrounded = Physics.Raycast(rayOrigin, Vector3.down, raycastDistance, groundLayer);
        }
        else
        {
            groundCheckTimer -= Time.deltaTime;
        }

    }

    void FixedUpdate()
    {
        if (canMove == true)
        {
            MovePlayer();
            ApplyJumpPhysics();
            
        }


    }

    void MovePlayer()
    {

        stepCounter -= Time.deltaTime;

        Vector3 movement = (transform.right * moveHorizontal + transform.forward * moveForward).normalized;
        Vector3 targetVelocity = movement * MoveSpeed;

        // Apply movement to the Rigidbody
        Vector3 velocity = rb.linearVelocity;
        velocity.x = targetVelocity.x;
        velocity.z = targetVelocity.z;
        rb.linearVelocity = velocity;


        if(isGrounded && (moveHorizontal != 0 || moveForward != 0))
        {
            if (stepCounter <= 0)
            {
                Debug.Log("move");
                SoundPlayer.clip = playerSounds[0];
                //SoundPlayer.pitch = Random.Range(0.9f, 1.1f);
                SoundPlayer.Play();

                stepCounter = stepInterval;
            }
        }

        

        // If we aren't moving and are on the ground, stop velocity so we don't slide
        if (isGrounded && moveHorizontal == 0 && moveForward == 0 && canMove)
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            //SoundPlayer.Stop();
        }

    }

    void RotateCamera()
    {
        float horizontalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, horizontalRotation, 0);

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    void Jump()
    {
        isGrounded = false;
        groundCheckTimer = groundCheckDelay;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z); // Initial burst for the jump
    }

    void ApplyJumpPhysics()
    {
        if (rb.linearVelocity.y < 0)
        {
            // Falling: Apply fall multiplier to make descent faster
            rb.linearVelocity += Vector3.up * Physics.gravity.y * fallMultiplier * Time.fixedDeltaTime;
        } // Rising
        else if (rb.linearVelocity.y > 0)
        {
            // Rising: Change multiplier to make player reach peak of jump faster
            rb.linearVelocity += Vector3.up * Physics.gravity.y * ascendMultiplier * Time.fixedDeltaTime;
        }
    }

}