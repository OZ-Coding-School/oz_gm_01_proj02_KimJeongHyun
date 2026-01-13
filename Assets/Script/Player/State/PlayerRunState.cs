using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using playerAnimation;

public class PlayerRunState : PlayerState
{
    public PlayerRunState(PlayerController ctr, StateMachine machine) : base(ctr, machine) { }

    public override void Enter()
    {
        base.Enter();
        if (ctr.InputShoot) ctr.aniHash.PlayAni(PlayerAnimation.RunShot);
        else ctr.aniHash.PlayAni(PlayerAnimation.Run);
    }

    public override void HandleInput()
    {
        if (ctr.InputShoot)
        {
            StateShoot();
            if (ctr.InputY != 0) ctr.aniHash.PlayAni(PlayerAnimation.RunDiagonalUpShot);
            else ctr.aniHash.PlayAni(PlayerAnimation.RunShot);
        }
        else { ctr.aniHash.PlayAni(PlayerAnimation.Run); }

        if (ctr.InputDash && ctr.canDash) { machine.ChangeState(ctr.state.Dash); return; }
        if (ctr.InputJump) { machine.ChangeState(ctr.state.Jump); return; }
        if (ctr.InputDuck) { machine.ChangeState(ctr.state.Duck); return; }
        if (ctr.InputX == 0) { machine.ChangeState(ctr.state.Idle); return; }
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (ctr.InputX != 0 && ctr.CurrentDir != (int)ctr.InputX) { ctr.Flip(); }
        if (!ctr.isGround) { machine.ChangeState(ctr.state.Fall); return; }
    }

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();
        ctr.MovementX();
    }
}
