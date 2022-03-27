using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRb;
    private PlayerInput inputActions;
    private InputAction move;
    private InputAction jump;
    private InputAction touch;
    private InputAction touchRelease;
    private InputAction touchPos;

    private float speed = 10;
    private float jumpStrength = 5;

    private GroundCheck groundCheck;

    [SerializeField] Text text;
    void Awake()
    {

        inputActions = new PlayerInput();
        move = inputActions.Player.Move;
        move.Enable();
        jump = inputActions.Player.Jump;
        jump.Enable();
        jump.performed += HandleJump;
        touch = inputActions.Player.Touch;
        touch.Enable();
        touchRelease = inputActions.Player.TouchRelease;
        touchRelease.Enable();
        touchPos = inputActions.Player.TouchPos;
        touchPos.Enable();

        playerRb = this.GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck").GetComponent<GroundCheck>();
    }


    void Update()
    {
        HandleMovement();
    }
    void HandleMovement()
    {
        float horizontalInput = move.ReadValue<Vector2>().x;
        //if (IsGrounded())
        {
            //playerRb.AddForce(transform.right * speed * horizontalInput);
            playerRb.velocity = new Vector2(horizontalInput * speed, playerRb.velocity.y);
        }
    }

    void HandleJump(InputAction.CallbackContext ctx)
    {
        if (IsGrounded())
        {
            //playerRb.AddForce(transform.up * jumpStrength, ForceMode2D.Impulse);
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpStrength);
        }
    }

    private bool IsGrounded()
    {
        return groundCheck.isGrounded;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("GAME OVER");
            text.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
