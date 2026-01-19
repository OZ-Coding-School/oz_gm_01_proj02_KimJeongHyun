using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerState
{
    public PlayerFallState(PlayerController ctr, StateMachine machine) : base(ctr, machine, PlayerAnimation.Jump) { }

    public override void Enter()
    {
        base.Enter();
        SetAirColSize();
        ctr.PlayerMovement.SetGravity(ctr.PlayerData.GravityFall);
    }

    public override void HandleInput()
    {
        if (TryParry)
        {
            ctr.PlayerInputHandler.UseParryBuffer();
            machine.ChangeState(ctr.PlayerState.Parry);
            return;
        }
        if (ctr.PlayerInputHandler.InputShoot) { Shooting(); }
        if (TryShotEX) { machine.ChangeState(ctr.PlayerState.ShotEX); return; }
        if (TryDash) { machine.ChangeState(ctr.PlayerState.Dash); return; }
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        Flip();
        if (ctr.PlayerCollision.IsGround) { machine.ChangeState(ctr.PlayerState.Idle); return; }
    }

    public override void StateFixedUpdate()
    {
        Move();
    }

    public override void Exit()
    {
        ctr.PlayerMovement.SetGravity(ctr.PlayerData.GravityJump);
        ctr.PlayerCollision.SetGroundColSize();
    }

    protected override void Shooting()
    {
        ctr.PlayerShooter.Shoot(new Vector2(ctr.PlayerMovement.CurrentDir, 0));
    }
}
