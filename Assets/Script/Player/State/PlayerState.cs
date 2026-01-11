using System;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerState : BaseState<PlayerController>
{    
    public PlayerState(PlayerController ctr, StateMachine machine) : base(ctr, machine) { }

    protected virtual bool canJump => true;
    protected virtual bool canDash => true;
    protected virtual bool canDuck => true;
    protected virtual bool canLock => true;
    protected virtual bool canParry => false;
    protected virtual bool canSuperAttack => true;
    protected float timer;

    public override void Enter()
    {
        timer = 0;
        ctr.rb.gravityScale = canGravity ? ctr.data.gravityVal : 0;
    }

    public override void HandleInput()
    {
        if (canParry && ctr.InputJump) { machine.ChangeState(ctr.state.Parry); return; }
        if (canDash && ctr.InputDash && ctr._canDash) { machine.ChangeState(ctr.state.Dash); return; }
        if (canSuperAttack && ctr.InputSuper && ctr.HasEnergy) { machine.ChangeState(ctr.state.Super); return; }

        if (ctr.isGround)
        {
            if (canJump && ctr.InputJump) { machine.ChangeState(ctr.state.Jump); return; }
            if (canDuck && ctr.InputDuck) { machine.ChangeState(ctr.state.Duck); return; }
            if (canLock && ctr.InputLock) { machine.ChangeState(ctr.state.Lock); return; }
            if (machine.CurState != ctr.state.Jump)
            {
                if (canMove && ctr.InputX != 0) { machine.ChangeState(ctr.state.Run); return; }
                if (canMove && ctr.InputX == 0) { machine.ChangeState(ctr.state.Idle); return; }
            }
        }
    }
   

    public override void StateUpdate()
    {
        timer += Time.deltaTime;  

        if (ctr.isGround && Time.time >= ctr.lastDashTime + ctr.data.dashCooldown)
        {
            ctr.SetCanDash(true);
        }
        if (canFlip && ctr.InputX != 0)
        {
            if (ctr.curDir != (int)ctr.InputX)
            {
                ctr.FlipX();
                ctr.transform.Rotate(0, 180, 0);
            }
        }
    }

    public override void StateFixedUpdate()
    {
        if (canMove == true)
        {
            float xSpeed = ctr.InputX * ctr.data.moveSpeed;
            ctr.rb.velocity = new Vector2(xSpeed, ctr.rb.velocity.y);
        }
        float maxFallSpeed = -15f;
        if (ctr.rb.velocity.y < maxFallSpeed)
        {
            ctr.rb.velocity = new Vector2(ctr.rb.velocity.x, maxFallSpeed);
        }
    }

    public override void OnHit(int dir)
    {
        if (ctr.isInvincible) return;
        ctr.state.Hit.SetDir(dir);
        machine.ChangeState(ctr.state.Hit);
    }

    protected virtual void StateShoot()
    {
        Vector2 dir = new Vector2(ctr.InputX, ctr.InputY).normalized;
        if (dir == Vector2.zero) { dir = new Vector2(ctr.curDir, 0); }

        UnityEngine.Transform firePoint = GetFirePoint(dir);
        ctr.PlayerShoot(firePoint, dir);
    }

    protected virtual UnityEngine.Transform GetFirePoint(Vector2 dir)
    {
        bool isInputX = dir.x != 0;
        int index = 0;

        if (dir.y > 0) index = isInputX ? 2 : 4;
        else if (dir.y < 0) index = isInputX ? 1 : 3;
        else index = 0;
        return ctr.firePoint[index];
    }
}
