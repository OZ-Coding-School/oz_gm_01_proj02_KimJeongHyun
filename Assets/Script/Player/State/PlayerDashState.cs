using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(PlayerController ctr, StateMachine machine) : base(ctr, machine,
        PlayerAnimation.DashGround, PlayerAnimation.DashAir) { }

    public override void Enter()
    {
        base.Enter();
        ctr.PlayerMovement.SetGravity(0);
        ctr.PlayerMovement.Dash();
        audio.PlaySFX(SFXType.PlayerDash);
        audio.StopSFX(SFXType.PeashooterLoop);
    }

    public override void HandleInput()
    {
        if (TryJump) { machine.ChangeState(ctr.PlayerState.Jump); return; }
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (timer >= ctr.PlayerData.DashTime)
        {
            if (ctr.PlayerCollision.IsGround) { machine.ChangeState(ctr.PlayerState.Idle); return; }
            else { machine.ChangeState(ctr.PlayerState.Fall); return; }
        }
    }

    public override void Exit()
    {
        ctr.PlayerMovement.SetGravity(ctr.PlayerData.GravityJump);
        if (ctr.PlayerInputHandler.InputShoot) { audio.PlaySFX(SFXType.PeashooterLoop); }
    }
}
