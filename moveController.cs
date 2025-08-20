using System.Runtime.CompilerServices;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;

public class MoveController : MonoBehaviour
{
    #region horizontalMovementVariables
    private float _playerInput;
    private float usedSpeed;
    [SerializeField] private float walkSpeed = 3.33f;

    [SerializeField] private float runningSpeed = 6;
    #endregion
    private Rigidbody2D body;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        usedSpeed = walkSpeed; 
    }

    void FixedUpdate()
    {
        _playerInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(_playerInput * usedSpeed, body.linearVelocity.y);
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