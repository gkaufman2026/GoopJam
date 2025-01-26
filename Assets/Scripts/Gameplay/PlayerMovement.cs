using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    [SerializeField] InputCollector input;

    [SerializeField] float timeBetweenFootsteps = 1; // this is modified by speed;
    float lastFootstepTime;

    [Header("Events")]
    public UnityEvent JumpEvent;
    public UnityEvent FootstepEvent;
    public UnityEvent LandEvent;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        //input = GetComponent<InputCollector>();
        input.playerActions.Move.performed += ctx => MyInput(ctx.ReadValue<Vector2>());
        input.playerActions.Move.canceled += ctx => MyInput(ctx.ReadValue<Vector2>());
        input.playerActions.Jump.performed += ctx => TryJump();

        LevelManager.Instance.RestartLevelEvent.AddListener(OnRestart);
    }

    private void Update()
    {
        bool lastGrounded = grounded;

        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        if (lastGrounded != grounded && grounded == true)
        {
            LandEvent.Invoke();
        }

        //MyInput();
        SpeedControl();   

        // handle drag
        if (grounded)
            rb.linearDamping = groundDrag;
        else
            rb.linearDamping = 0;

        if (grounded && rb.linearVelocity != Vector3.zero && (verticalInput != 0 || horizontalInput != 0))
        {
            Debug.Log("MOVESPEED IS " + rb.linearVelocity.magnitude / moveSpeed);
            if (Time.time - lastFootstepTime >= timeBetweenFootsteps * rb.linearVelocity.magnitude / moveSpeed) //  * (moveSpeed - rb.linearVelocity.magnitude)
            {
                FootstepEvent.Invoke();
                lastFootstepTime = Time.time;
            }
        }
        else
        {
            lastFootstepTime = -1;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MyInput(Vector2 moveValue)
    {
        horizontalInput = moveValue.x;
        verticalInput = moveValue.y;
    }

    private void TryJump()
    {
        // when to jump
        if (readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // reset y velocity
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        JumpEvent.Invoke();
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

    private void OnRestart(Level level)
    {
        rb.MovePosition(level.levelStartPoint.position);
        rb.MoveRotation(level.levelStartPoint.rotation);
        rb.linearVelocity = Vector3.zero;
    }
}
