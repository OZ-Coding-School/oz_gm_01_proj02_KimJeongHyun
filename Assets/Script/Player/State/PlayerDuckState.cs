using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using playerAnimation;


public class PlayerDuckState : PlayerState
{
    public PlayerDuckState(PlayerController ctr, StateMachine machine) : base(ctr, machine) { }
    protected override bool canDuck => false;
    protected override bool canMove => false;
    protected override bool canLock => false;

    public override void Enter()
    {
        base.Enter();
        ctr.rb.velocity = new Vector2(0, ctr.rb.velocity.y);
        if (ctr.InputShoot) ctr.aniHash.PlayAni(PlayerAnimation.DuckShot);
        else ctr.aniHash.PlayAni(PlayerAnimation.DuckIdle);
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (ctr.InputShoot)
        {
            ctr.aniHash.PlayAni(PlayerAnimation.DuckShot);
            StateShoot();
        }
        else { ctr.aniHash.PlayAni(PlayerAnimation.DuckIdle); }

        if (!ctr.InputDuck)
        {
            if (ctr.InputX != 0) { machine.ChangeState(ctr.state.Run); return; }
            else {  machine.ChangeState(ctr.state.Idle);}
        }
    }

    protected override void StateShoot()
    {
        Vector2 dir = new Vector2(ctr.curDir, 0);
        Transform firePoint = ctr.firePoint[5];
        ctr.PlayerShoot(firePoint, dir);
    }
}
