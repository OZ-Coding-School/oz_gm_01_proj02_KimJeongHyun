using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerState
{
    public PlayerFallState(PlayerController ctr, StateMachine machine) : base(ctr, machine, PlayerAnimation.Jump) { }

    public override void Enter()
    {
        base.Enter();
        ctr.PlayerMovement.SetGravity(ctr.PlayerData.GravityFall);
    }

    public override void HandleInput()
    {
        if (ctr.PlayerInputHandler.InputShoot) { Shooting(); }
        if (TryDash) { machine.ChangeState(ctr.PlayerState.Dash); return; }
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        Flip();
        if (ctr.PlayerCollision.IsGround)
        {
            if (ctr.PlayerInputHandler.InputJump) { machine.ChangeState(ctr.PlayerState.Jump); return; }
            if (ctr.PlayerInputHandler.InputX != 0) { machine.ChangeState(ctr.PlayerState.Run); return; }
            if (ctr.PlayerInputHandler.InputX == 0) { machine.ChangeState(ctr.PlayerState.Idle); return; }
            if (ctr.PlayerInputHandler.InputDuck) { machine.ChangeState(ctr.PlayerState.Duck); return; }
            if (ctr.PlayerInputHandler.InputLock) { machine.ChangeState(ctr.PlayerState.Lock); return; }
            if (TrySuper) { machine.ChangeState(ctr.PlayerState.Super); return; }
            if (TryDash) { machine.ChangeState(ctr.PlayerState.Dash); return; }
        }
    }

    public override void StateFixedUpdate()
    {
        Move();
    }

    public override void Exit()
    {
        ctr.PlayerMovement.SetGravity(ctr.PlayerData.GravityJump);
    }
}
