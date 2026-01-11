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
        if (ctr.InputShoot) ctr.aniHash.PlayAni(PlayerAnimation.ShotStraight);
        else ctr.aniHash.PlayAni(PlayerAnimation.Idle);
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (ctr.InputShoot)
        {           
            ctr.aniHash.PlayAni(PlayerAnimation.ShotStraight);
            StateShoot();
        }
        else
        {
            ctr.aniHash.PlayAni(PlayerAnimation.Idle);
        }              
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (!ctr.isGround) { machine.ChangeState(ctr.state.Fall); return; }
    }

    protected override void StateShoot()
    {
        Vector2 dir = new Vector2(ctr.curDir, 0);
        Transform firePoint = ctr.firePoint[0];
        ctr.PlayerShoot(firePoint, dir);
    }
}
