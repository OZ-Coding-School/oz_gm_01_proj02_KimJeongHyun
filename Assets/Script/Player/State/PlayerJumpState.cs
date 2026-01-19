using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(PlayerController ctr, StateMachine machine) : base(ctr, machine, PlayerAnimation.Jump) { }

    public override void Enter()
    {
        base.Enter();
        SetAirColSize();
        ctr.PlayerMovement.Jump();
    }

    public override void HandleInput()
    {
        if (timer > 0.2)
        {
            if (TryParry)
            {
                ctr.PlayerInputHandler.UseParryBuffer();
                machine.ChangeState(ctr.PlayerState.Parry);
                return;
            }
        }        
        if (ctr.PlayerInputHandler.InputJumpUp)
        {
            if (timer < ctr.PlayerData.LowJumpTime && ctr.Rb.velocity.y > 0)
            {
                ctr.Rb.velocity = new Vector2(ctr.Rb.velocity.x, ctr.PlayerData.LowJumpForce);
            }
        }

        if (TryDash) { machine.ChangeState(ctr.PlayerState.Dash); return; }
        if (ctr.PlayerInputHandler.InputShoot) { Shooting(); }
        if (TryShotEX) { machine.ChangeState(ctr.PlayerState.ShotEX); return; }
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        Flip();
        if (ctr.Rb.velocity.y < 0) { machine.ChangeState(ctr.PlayerState.Fall); return; }
    }

    public override void StateFixedUpdate()
    {
        Move();        
    }
    public override void Exit()
    {
        ctr.PlayerCollision.SetGroundColSize();
    }

    protected override void Shooting()
    {
        ctr.PlayerShooter.Shoot(new Vector2(ctr.PlayerMovement.CurrentDir, 0));
    }
}
