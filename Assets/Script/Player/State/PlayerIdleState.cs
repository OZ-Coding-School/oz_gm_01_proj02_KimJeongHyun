using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using playerAnimation;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerController ctr, StateMachine machine) : base(ctr, machine) { }

    public override void Enter()
    {
        base.Enter();
        ctr.rb.velocity = new Vector2(0, ctr.rb.velocity.y);
        if (ctr.InputShoot) ctr.aniHash.PlayAni(PlayerAnimation.ShotStraight);
        else ctr.aniHash.PlayAni(PlayerAnimation.Idle);
    }

    public override void HandleInput()
    {
        if (ctr.InputShoot)
        {
            StateShoot();
            ctr.aniHash.PlayAni(PlayerAnimation.ShotStraight);
        }
        else { ctr.aniHash.PlayAni(PlayerAnimation.Idle); }
        if (ctr.InputDash && ctr.canDash) { machine.ChangeState(ctr.state.Dash); return; }
        if (ctr.InputJump) { machine.ChangeState(ctr.state.Jump); return; }
        if (ctr.InputDuck) { machine.ChangeState(ctr.state.Duck); return; }
        if (ctr.InputX != 0) { machine.ChangeState(ctr.state.Run); return; }
        if (ctr.InputLock) { machine.ChangeState(ctr.state.Lock); return; }
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (ctr.InputX != 0 && ctr.CurrentDir != (int)ctr.InputX) { ctr.Flip(); }
        if (!ctr.isGround) { machine.ChangeState(ctr.state.Fall); return; }
    }

    protected override void StateShoot()
    {
        Vector2 dir = new Vector2(ctr.CurrentDir, 0);
        Transform firePoint = ctr.firePoint[0];
        ctr.PlayerShoot(firePoint, dir);
    }
}
