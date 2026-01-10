using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using playerAnimation;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerController ctr, StateMachine machine) : base(ctr, machine) { }

    public override void Enter()
    {
        if (ctr.InputShoot) ctr.aniHash.PlayAni(PlayerAnimation.ShotStraight);
        else ctr.aniHash.PlayAni(PlayerAnimation.Idle);
    }

    public override void HandleInput()
    {
        if (ctr.InputX != 0) { machine.ChangeState(ctr.state.Run); return; }
        if (ctr.InputJump) { machine.ChangeState(ctr.state.Jump); return; }
        if (ctr.InputDuck) { machine.ChangeState(ctr.state.Duck); return; }
        if (ctr.InputLock) {  machine.ChangeState(ctr.state.Lock); return; }
        if (ctr.InputShoot)
        {           
            ctr.aniHash.PlayAni(PlayerAnimation.ShotStraight);
            StateShoot();
        }
        else
        {
            ctr.aniHash.PlayAni(PlayerAnimation.Idle);
        }
        if(ctr.InputDash)
        {
            OnHit(-1);
        }
    }

    protected override void StateShoot()
    {
        Vector2 dir = new Vector2(ctr.curDir, 0);
        Transform firePoint = ctr.firePoint[0];
        ctr.PlayerShoot(firePoint, dir);
    }

    public override void StateFixedUpdate()
    {
        ctr.SetVelocityX(0);
    }
}
