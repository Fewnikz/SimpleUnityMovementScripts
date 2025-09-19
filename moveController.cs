using UnityEngine;

public class MoveController : MonoBehaviour
{

    // Variabler til horisontal bevægelse
    private float _playerInput;
    private float usedSpeed; // Den aktuelle hastighed spilleren bevæger sig med
    [SerializeField] private float walkSpeed = 3.33f; // Gå-hastighed
    [SerializeField] private float runningSpeed = 6; // Løbe-hastighed (kan justeres i Inspector)

    private Rigidbody2D _rigidbody2D; // Reference til spillerens Rigidbody2D komponent

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>(); // Henter Rigidbody2D komponenten på spilleren
        usedSpeed = walkSpeed; // Starter med at gå       
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