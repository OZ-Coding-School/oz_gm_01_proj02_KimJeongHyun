using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDuckState : PlayerState
{
    public PlayerDuckState(PlayerController ctr, StateMachine machine) : base(ctr, machine, PlayerAnimation.DuckIdle) { }

    public override void Enter()
    {
        base.Enter();
        ctr.PlayerMovement.Stop();
    }

    public override void HandleInput()
    {        
        if (ctr.PlayerInputHandler.InputShoot)
        {
            Shooting();
            PlayAni(PlayerAnimation.DuckShot);
        }
        else { PlayAni(PlayerAnimation.DuckIdle); }

        if (ctr.PlayerInputHandler.InputJump) { ctr.PlayerCollision.DropDown(); }
        if (TryShotEX) { machine.ChangeState(ctr.PlayerState.ShotEX); return; }
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        Flip();
        if (!ctr.PlayerInputHandler.InputDuck) { machine.ChangeState(ctr.PlayerState.Idle); }
        if (!ctr.PlayerCollision.IsGround) { machine.ChangeState(ctr.PlayerState.Fall); return; }
    } 

    protected override void Shooting()
    {
        ctr.PlayerShooter.Shoot(new Vector2(ctr.PlayerMovement.CurrentDir, 0), true);
    }
}
