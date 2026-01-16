using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using playerAnimation;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerController ctr, StateMachine machine) : base(ctr, machine,
        PlayerAnimation.Idle) { }

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
            PlayAni(PlayerAnimation.ShotStraight);
        }
        else { PlayAni(PlayerAnimation.Idle); }             
        if (ctr.PlayerInputHandler.InputJump) { machine.ChangeState(ctr.PlayerState.Jump); return; }
        if (ctr.PlayerInputHandler.InputX != 0) { machine.ChangeState(ctr.PlayerState.Run); return; }
        if (ctr.PlayerInputHandler.InputDuck) { machine.ChangeState(ctr.PlayerState.Duck); return; }
        if (ctr.PlayerInputHandler.InputLock) { machine.ChangeState(ctr.PlayerState.Lock); return; }
        if (TryDash) { machine.ChangeState(ctr.PlayerState.Dash); return; }
        if (TrySuper) { machine.ChangeState(ctr.PlayerState.Super); return; }
    }

}
