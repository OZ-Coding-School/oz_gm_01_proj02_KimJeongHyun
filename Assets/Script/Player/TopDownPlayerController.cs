using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayerController : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    private Rigidbody2D rb;
    private Vector2 input;
    private Animator anim;
    private Vector2 lastMoveDir;
    private bool facingRight = true;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        SetInput();
        Animate();
        if(input.x < 0 && facingRight || input.x > 0 && !facingRight)
        {
            Flip();
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = input * moveSpeed;
    }

    private void SetInput()
    {
        float moveX = InputManager.Instance.GetHorizontal();
        float moveY = InputManager.Instance.GetVertical();

        if ((moveX == 0 && moveY == 0) && (input.x != 0 || input.y != 0))
        {
            lastMoveDir = input;
        }

        input.x = InputManager.Instance.GetHorizontal();
        input.y = InputManager.Instance.GetVertical();
        input.Normalize();
    }

    private void Animate()
    {
        anim.SetFloat("InputX", input.x);
        anim.SetFloat("InputY", input.y);
        anim.SetFloat("MoveMagnitude", input.magnitude);
        anim.SetFloat("LastInputX", lastMoveDir.x);
        anim.SetFloat("LastInputY", lastMoveDir.y);
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingRight = !facingRight;
    }
}
