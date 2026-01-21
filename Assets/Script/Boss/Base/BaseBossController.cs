using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BaseBossController : Entity
{
    public Transform playerTrs;
    public Transform groundCheckTrs;
    public Transform wallCheckTrs;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public  int curDir = -1;

    public float groundCheckRadius = 0.2f;
    public float wallCheckRadius = 1f;

    protected override void FixedUpdate()
    {
        IsGround();
        base.FixedUpdate();
    }

    public bool IsGround()
    {
        return Physics2D.OverlapCircle(groundCheckTrs.position, groundCheckRadius, groundLayer);
    }

    public void FlipToPlayer()
    {
        int temp = playerTrs.position.x > transform.position.x ? 1 : -1;
        if (temp != curDir)
        {
            transform.Rotate(0, 180, 0);
            curDir = temp;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent<IDamageable>(out var target))
        {
            Debug.Log("플레이어 감지됨");
            Vector2 dir = transform.position;
            target.OnDamage(1, dir);
        }
    }
}
