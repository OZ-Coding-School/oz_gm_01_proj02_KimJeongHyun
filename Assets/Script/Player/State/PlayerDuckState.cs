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
        Flip();
        if (ctr.PlayerInputHandler.InputShoot)
        {
            Shooting();
            PlayAni(PlayerAnimation.DuckShot);
        }
        else { PlayAni(PlayerAnimation.DuckIdle); }

        if (ctr.PlayerInputHandler.InputJump) { ctr.PlayerCollision.DropDown(); }
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (!ctr.PlayerInputHandler.InputDuck)
        {
            if (ctr.PlayerInputHandler.InputJump) { machine.ChangeState(ctr.PlayerState.Jump); return; }
            if (ctr.PlayerInputHandler.InputX != 0) { machine.ChangeState(ctr.PlayerState.Run); return; }
            if (ctr.PlayerInputHandler.InputX == 0) { machine.ChangeState(ctr.PlayerState.Idle); return; }
            if (ctr.PlayerInputHandler.InputLock) { machine.ChangeState(ctr.PlayerState.Lock); return; }
            if (TryDash) { machine.ChangeState(ctr.PlayerState.Dash); return; }
            if (TrySuper) { machine.ChangeState(ctr.PlayerState.Super); return; }
        }
        if (!ctr.PlayerCollision.IsGround) { machine.ChangeState(ctr.PlayerState.Fall); return; }
    }
}
