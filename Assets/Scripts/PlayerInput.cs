using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isGrounded = false;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 10f;

    private Vector2 direction = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
        Debug.Log(direction);
    }

    void Update()
    {
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
    }

    public void OnJump(InputValue value)
    {
        if (isGrounded && value.isPressed)
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (isGrounded) {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (Vector2.Angle(collision.GetContact(0).normal, Vector2.up) < 45f)
        {
            isGrounded = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Vector2.Angle(collision.GetContact(0).normal, Vector2.up) < 45f)
        {
            isGrounded = true;
        }
    }
}