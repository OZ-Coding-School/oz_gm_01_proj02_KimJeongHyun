using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using playerAnimation;


public class PlayerDuckState : PlayerState
{
    public PlayerDuckState(PlayerController ctr, StateMachine machine) : base(ctr, machine) { }
    protected override bool canMove => false;

    public override void Enter()
    {
        ctr.SetVelocityX(0);

        if (ctr.InputShoot) ctr.aniHash.PlayAni(PlayerAnimation.DuckShot);
        else ctr.aniHash.PlayAni(PlayerAnimation.DuckIdle);
    }

    public override void HandleInput()
    {
        if (!ctr.InputDuck) { machine.ChangeState(ctr.state.Idle); return; }
        if (ctr.InputJump) { machine.ChangeState(ctr.state.Jump); return; }
        if (ctr.InputShoot)
        {
            ctr.aniHash.PlayAni(PlayerAnimation.DuckShot);
            StateShoot();
        }
        else
        {
            ctr.aniHash.PlayAni(PlayerAnimation.DuckIdle);
        }
    }

    protected override void StateShoot()
    {
        Vector2 dir = new Vector2(ctr.curDir, 0);
        Transform firePoint = ctr.firePoint[5];
        ctr.PlayerShoot(firePoint, dir);
    }
}
