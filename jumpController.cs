using UnityEngine;
using MoreMountains.Feedbacks;

public class jumpController : MonoBehaviour
{
    // Raycast and ground check
    [SerializeField] private Transform feet;
    [SerializeField] private float raycastLength;
    [SerializeField] private LayerMask groundMask;
    private bool _onGround; // Checks if the player is on the ground

    // Variables for jump and gravity
    [SerializeField] private float jumpPower; // How powerful the player's jump is
    [SerializeField] private float gravityFall; // The gravity then the player falls down
    [SerializeField] private float normalGravityScale; // Normal gravity then the player moves up or stands still
    private bool _jumpButtonPressed;
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private MMFeedbacks jumpFeedback; // Remove this unless you have the Feel asset form unity

    // Variables for variable jump height
    public float jumpStartTime; // What the timer for holding jump starts at
    private float _jumpTime; // The amount of time the player can hold jump
    private bool _isJumping;
    [SerializeField] private float jumpPowerFraction;

    // Variables for coyoteJump
    [SerializeField] private float coyoteTimer;
    [SerializeField] private float coyoteStartTimer;
    private bool _canCoyoteJump;

    // FixedUpdate Functions
    private void PerformJump()
    {
        // Check for jump-input, and if the player stands on the ground, then they jump
        if (_jumpButtonPressed)
        {
            // Makes sure the players downward force is reset
            _rigidbody2D.linearVelocity = new Vector2(_rigidbody2D.linearVelocity.x, 0);

            // Vector2.up is the same as (0, 1)
            _rigidbody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jumpFeedback?.PlayFeedbacks();
            _jumpButtonPressed = false;
        }
    }
    private void ApplyVariableJumpHeight()
    {
        if (Input.GetKey(KeyCode.Space) && _isJumping)
        {
            if (_jumpTime > 0)
            {
                _rigidbody2D.AddForce(Vector2.up * (jumpPower * jumpPowerFraction), ForceMode2D.Impulse);
                _jumpTime -= Time.deltaTime;
            }
            else
            {
                _isJumping = false;
            }
        }
    }

    // Update Functions

    private void UpdateGravityScale()
    {
        // Tjekker om spillerens velocity er højer eller = 0
        if (_rigidbody2D.linearVelocity.y >= 0)
        {
            // Gør så gravityScale er = normalGravityScale
            _rigidbody2D.gravityScale = normalGravityScale;
        }
        // Tjekker om spillerens velocity er lavere
        else if (_rigidbody2D.linearVelocity.y < 0)
        {
            // Gør så spillerens gravityScale er = gravityFall
            _rigidbody2D.gravityScale = gravityFall;
        }        
    }
    private void HandleJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _canCoyoteJump)
        {
            _jumpButtonPressed = true;
            _isJumping = true;
            _jumpTime = jumpStartTime;
        }
    }
    private void UpdateCoyoteTime()
    {
        // Handles coyote
        if (_onGround == false)
        {
            coyoteTimer -= Time.deltaTime;
        }
        else
        {
            coyoteTimer = coyoteStartTimer;
        }

        if (coyoteTimer > 0)
        {
            _canCoyoteJump = true;
        }
        else
        {
            _canCoyoteJump = false;
        }
    }
    private void CreateRaycast()
    {
        // Draws and creates a raycast
        Debug.DrawRay(feet.position, Vector2.down * raycastLength, Color.red);
        RaycastHit2D groundHit = Physics2D.Raycast(feet.position, Vector2.down, raycastLength, groundMask);

        // Handles ground detection from raycast
        if (groundHit.collider != null && groundHit.collider != gameObject)
        {
            _onGround = true;
        }
        else
        {
            _onGround = false;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        PerformJump();
        ApplyVariableJumpHeight();
        CreateRaycast();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGravityScale();
        HandleJumpInput();

        // Makes sure that then the jump button is realeased isJumping is set to false. And that coyoteTimer is set to 0
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _isJumping = false;
            coyoteTimer = 0;
        }

        UpdateCoyoteTime();
    }

}

