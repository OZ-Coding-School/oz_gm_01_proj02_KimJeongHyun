using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Windows;

public class PlayerMovement
{
    private readonly PlayerController controller;
    private readonly Rigidbody2D rb;
    private readonly PlayerDataSO data;

    public int CurrentDir { get; private set; } = 1;
    public float LastDashTime { get; private set; } = -100f;

    public int DashCount { get; private set; } = 0;
    public int JumpCount { get; private set; } = 0;

    public bool CanDash => (Time.time >= LastDashTime + data.DashCooldown) && DashCount > 0;
    public bool CanJump => JumpCount > 0;

    public PlayerMovement(PlayerController controller, Rigidbody2D rb,
        PlayerDataSO data)
    {
        this.controller = controller;
        this.rb = rb;
        this.data = data;
    }

    public void Move(float x)
    {
        rb.velocity = new Vector2(x * data.MoveSpeed, rb.velocity.y);
    }

    public void Jump()
    {
        JumpCount = 0;
        rb.velocity = new Vector2(rb.velocity.x, data.JumpForce);
    }

    public void Parry()
    {
        rb.velocity = new Vector2(rb.velocity.x, data.ParryJumpForce);
    }

    public void Stop()
    {
        rb.velocity = Vector2.zero;
    }

    public void Dash()
    {
        DashCount = 0;
        controller.StartCoroutine(DashCo());
    } 

    public void SetGravity(float val)
    {
        rb.gravityScale = val;
    }

    public void ResetJumpDash()
    {
        DashCount = 1;
        JumpCount = 1;
    }

    private IEnumerator DashCo()
    {
        LastDashTime = Time.time;
        rb.velocity = new Vector2(CurrentDir * data.DashSpeed, 0);

        yield return new WaitForSeconds(data.DashTime);
    }

    public void CheckFlip(float x)
    {
        if (x != 0 && x != CurrentDir)
        {
            CurrentDir *= -1;
            controller.transform.Rotate(0, 180, 0);
        }
    }
    
    public void SetCurDir(int dir)
    {
        CurrentDir = dir; 
    }

    public void CheckMaxJump()
    {
        if (rb.velocity.y > data.JumpForce)
        {
            rb.velocity = new Vector2(rb.velocity.x, data.JumpForce);
        }
    }
}
