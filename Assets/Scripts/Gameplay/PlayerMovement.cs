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

    public float slowToMaxComponent = 0.3f;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Dash")]
    public float dashSpeed = 0.4f;
    public float unlimitedSpeedDuration = 0.73f;
    bool unlimitedSpeedActive = false;

    float dashTime;
    bool canDash;

    bool dashUnlocked;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public float coyoteTime = 0.18f;
    float coyoteTimeStart = -1f;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    GravityTraveller traveller;
    [SerializeField] InputCollector input;
    [SerializeField] GameObject CameraForVerticalLook;

    [SerializeField] float timeBetweenFootsteps = 1; // this is modified by speed;
    float lastFootstepTime;

    [Header("Events")]
    public UnityEvent JumpEvent;
    public UnityEvent FootstepEvent;
    public UnityEvent LandEvent;
    public UnityEvent DashEvent;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        traveller = GetComponent<GravityTraveller>();

        readyToJump = true;

        //input = GetComponent<InputCollector>();
        input.playerActions.Move.performed += ctx => MyInput(ctx.ReadValue<Vector2>());
        input.playerActions.Move.canceled += ctx => MyInput(ctx.ReadValue<Vector2>());

        input.playerActions.Jump.performed += ctx => TryJump();

        input.playerActions.Sprint.performed += ctx => TryDash();

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
            canDash = true;
        } else if (lastGrounded == true && !grounded)
        {
            coyoteTimeStart = Time.time;
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
        if (readyToJump && (grounded || Time.time - coyoteTimeStart <= coyoteTime))
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void TryDash()
    {
        if (canDash && dashUnlocked)
            Dash();
    }

    public void UnlockDash()
    {
        dashUnlocked = true;
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

        if (unlimitedSpeedActive)
        {
            if (Time.time - dashTime >= unlimitedSpeedDuration)
            {
                unlimitedSpeedActive = false;
            }
            else
            {
                rb.AddForce(traveller.GetCurrentForce * -1); //  * Time.deltaTime
                return;
            }          
        }

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed + ((flatVel - flatVel.normalized * moveSpeed) * slowToMaxComponent); // * Time.deltaTime);
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

    private void Dash()
    {
        float magnitude = Mathf.Abs(rb.linearVelocity.y); //.magnitude;

        // (camera's vertical look component + player move component) * magnitude of their movement + a bit more
        rb.linearVelocity = (CameraForVerticalLook.transform.forward.normalized.y * Vector3.up + moveDirection.normalized) * (magnitude + dashSpeed);

        dashTime = Time.time;

        canDash = false;
        unlimitedSpeedActive = true;

        DashEvent.Invoke();
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
