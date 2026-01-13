using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using playerAnimation;

public class PlayerHitState : PlayerState
{
    public PlayerHitState(PlayerController ctr, StateMachine machine) : base(ctr, machine) { }

    public override void Enter()
    {
        base.Enter();
        ctr.rb.velocity = new Vector2(ctr.hitDir * ctr.data.knockbackForceX, ctr.data.knockbackForceY);
        if (ctr.isGround) ctr.aniHash.PlayAni(PlayerAnimation.HitGround);
        else ctr.aniHash.PlayAni(PlayerAnimation.HitAir);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (ctr.InputDash && ctr.canDash) { machine.ChangeState(ctr.state.Dash); return; }
        if (ctr.isGround)
        {
            if (ctr.InputX != 0) { machine.ChangeState(ctr.state.Run); return; }
            if (ctr.InputDuck) { machine.ChangeState(ctr.state.Duck); return; }
            if (ctr.InputJump) { machine.ChangeState(ctr.state.Jump); return; }
            if (ctr.InputLock) { machine.ChangeState(ctr.state.Lock); return; }
            if (ctr.InputX == 0) { machine.ChangeState(ctr.state.Idle); return; }
        }
        if (!ctr.isGround) { machine.ChangeState(ctr.state.Fall); return; }
    }
}
