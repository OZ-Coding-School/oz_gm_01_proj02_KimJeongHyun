using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using playerAnimation;


public class PlayerDuckState : PlayerState
{
    public PlayerDuckState(PlayerController ctr, StateMachine machine) : base(ctr, machine) { }

    public override void Enter()
    {
        base.Enter();
        ctr.rb.velocity = new Vector2(0, ctr.rb.velocity.y);
        if (ctr.InputShoot) ctr.aniHash.PlayAni(PlayerAnimation.DuckShot);
        else ctr.aniHash.PlayAni(PlayerAnimation.DuckIdle);
    }

    public override void HandleInput()
    {
        if (ctr.InputShoot)
        {
            StateShoot();
            ctr.aniHash.PlayAni(PlayerAnimation.DuckShot);
        } 
        else { ctr.aniHash.PlayAni(PlayerAnimation.DuckIdle); }

        if (ctr.InputJump && ctr.GetcurPlatform() != null)
        {
            ctr.IgnoreCurPlatform();
            machine.ChangeState(ctr.state.Fall); return;
        }
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (ctr.InputX != 0 && ctr.CurrentDir != (int)ctr.InputX) { ctr.Flip(); }

        if (!ctr.InputDuck)
        {
            if (ctr.InputJump) { machine.ChangeState(ctr.state.Jump); return; }
            if (ctr.InputX != 0) { machine.ChangeState(ctr.state.Run); return; }
            if (ctr.InputX == 0) { machine.ChangeState(ctr.state.Idle); return; }
        }
    }

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();
        ctr.rb.velocity = new Vector2(0, ctr.rb.velocity.y);
    }

    protected override void StateShoot()
    {
        Vector2 dir = new Vector2(ctr.CurrentDir, 0);
        Transform firePoint = ctr.firePoint[5];
        ctr.PlayerShoot(firePoint, dir);
    }    
}
