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
        if (ctr.InputShoot) StateShoot();
        if (ctr.InputDash && ctr.canDash) { machine.ChangeState(ctr.state.Dash); return; }       
    }
    public override void StateUpdate()
    {
        base.StateUpdate();
        if (ctr.InputX != 0 && ctr.CurrentDir != (int)ctr.InputX) { ctr.Flip(); }        
        if (ctr.isGround)
        {
            if (ctr.InputDuck) { machine.ChangeState(ctr.state.Duck); return; }
            if (ctr.InputX != 0) { machine.ChangeState(ctr.state.Run); return; }
            if (ctr.InputX == 0) { machine.ChangeState(ctr.state.Idle); return; }
            if (ctr.InputLock) { machine.ChangeState(ctr.state.Lock); return; }
        }
    }

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();
        ctr.MovementX();
    }
}
