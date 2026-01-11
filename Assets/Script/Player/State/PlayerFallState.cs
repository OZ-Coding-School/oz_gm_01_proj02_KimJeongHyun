using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using playerAnimation;


public class PlayerFallState : PlayerState
{
    public PlayerFallState(PlayerController ctr, StateMachine machine) : base(ctr, machine) { }

    public override void Enter()
    {
        base.Enter();
        ctr.aniHash.PlayAniSync(PlayerAnimation.Jump);
        ctr.rb.gravityScale = ctr.data.gravityVal * 1.3f;
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (ctr.rb.velocity.y <= 0 && ctr.isGround)
        {
            if (ctr.InputX != 0) { machine.ChangeState(ctr.state.Run); return; }
            if (ctr.InputDuck) { machine.ChangeState(ctr.state.Duck); return; }
            else machine.ChangeState(ctr.state.Idle); return;
        }
        if (ctr.InputDash && ctr._canDash) { machine.ChangeState(ctr.state.Dash); return; }
        if (ctr.InputShoot) StateShoot();
    }
}
