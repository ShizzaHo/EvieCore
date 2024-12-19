using UnityEngine;
using UnityEngine.Events;

public class BasicMovement : MonoBehaviour, EvieCoreUpdate
{
    //=== BasicStart ===

    [Header("Movement Settings")]
    [Tooltip("The speed at which the player moves.")]
    [Range(1f, 20f)]
    public float moveSpeed = 10f;

    [Tooltip("The speed at which the player runs.")]
    [Range(1f, 30f)]
    public float runSpeed = 15f;

    [Tooltip("The drag applied when the player is grounded.")]
    [Range(0f, 10f)]
    public float groundDrag = 4f;

    [Header("Jump Settings")]
    [Tooltip("The height of the player, used for ground detection.")]
    [Range(0.1f, 3f)]
    public float playerHeight = 2f;

    [Tooltip("Force applied when the player jumps.")]
    [Range(1f, 50f)]
    public float jumpForce = 10f;

    [Header("Control Options")]
    [Tooltip("Allow the player to jump.")]
    public bool canJump = true;

    [Tooltip("Allow the player to run.")]
    public bool canRun = true;

    [Header("References")]
    [Tooltip("The orientation object to define movement direction.")]
    public Transform orientation;

    private bool grounded;
    private bool isJumping;
    private bool isRunning;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;

    private Rigidbody rb;

    //=== CameraAnimationStart ===

    public UnityAction CameraAnimationMovement_WALKING;
    public UnityAction CameraAnimationMovement_JUMP;
    public UnityAction CameraAnimationMovement_RUNNING;

    //=== CameraAnimationEnd ===

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        UpdateManager.Instance.Register(this);
    }

    public void OnUpdate()
    {
        grounded = GroundCheck();

        GetInput();

        if (grounded)
        {
            rb.linearDamping = groundDrag;

            // Jumping logic
            if (canJump && Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                Jump();
            }

            // Running logic
            if (canRun && Input.GetKey(KeyCode.LeftShift))
            {
                isRunning = true;
                CameraAnimationMovement_RUNNING?.Invoke();
            }
            else
            {
                isRunning = false;
            }

            // Walking animation
            if (horizontalInput != 0 || verticalInput != 0)
            {
                if (!isRunning)
                    CameraAnimationMovement_WALKING?.Invoke();
            }
        }
        else
        {
            rb.linearDamping = 0;
            if (isJumping)
            {
                CameraAnimationMovement_JUMP?.Invoke();
            }
        }
    }

    public void FixedUpdate()
    {
        MovePlayer();
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isJumping = true;
    }

    private bool GroundCheck()
    {
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f);
        if (isGrounded && isJumping)
        {
            isJumping = false;
        }
        return isGrounded;
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        if (grounded)
        {
            float currentSpeed = moveSpeed;

            if (isRunning)
            {
                currentSpeed = runSpeed;
            }

            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
            rb.AddForce(moveDirection.normalized * currentSpeed * 10f, ForceMode.Force);
        }
        else
        {
            moveDirection = Vector3.zero;
        }
    }

    private void OnDestroy()
    {
        UpdateManager.Instance.Unregister(this);
    }
}
