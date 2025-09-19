using UnityEngine;

public class MoveController : MonoBehaviour
{

    // Horizontal movement variables
    private float _playerInput;
    private float usedSpeed; // The actual speed the player uses
    [SerializeField] private float walkSpeed = 3.33f;
    [SerializeField] private float runningSpeed = 6;

    private Rigidbody2D _rigidbody2D; // Reference to the players rigidbody2D component

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>(); // Gets the rigidbody2D component
        usedSpeed = walkSpeed;  
    }

    void FixedUpdate()
    {
        // Here is the horizontal movement of the player
        _playerInput = Input.GetAxis("Horizontal");
        _rigidbody2D.linearVelocity = new Vector2(_playerInput * usedSpeed, _rigidbody2D.linearVelocity.y);
    }

    // Update is called every frame
    void Update()
    {

        // Makes sure the player is facing the right direction
        if (_playerInput < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (_playerInput > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }


        // Changes between running and walking speed
        if (Input.GetKey(KeyCode.LeftShift))
        {
            usedSpeed = runningSpeed;
        }
        else
        {
            usedSpeed = walkSpeed;
        }
    }
}