using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using playerAnimation;
public class PlayerLockState : PlayerState
{
    public PlayerLockState(PlayerController ctr, StateMachine machine) : base(ctr, machine) { }

    public override void Enter()
    {
        base.Enter();
        ctr.rb.velocity = new Vector2 (0, ctr.rb.velocity.y);
        if (ctr.InputShoot) ctr.aniHash.PlayAni(PlayerAnimation.ShotStraight);
        else ctr.aniHash.PlayAni(PlayerAnimation.AimStraight);
    }

    public override void HandleInput()
    {
        StateShoot();
        if (!ctr.InputLock)
        {
            if (ctr.InputDash && ctr.canDash) { machine.ChangeState(ctr.state.Dash); return; }
            if (ctr.InputJump) { machine.ChangeState(ctr.state.Jump); return; }
            if (ctr.InputDuck) { machine.ChangeState(ctr.state.Duck); return; }
            if (ctr.InputX != 0) { machine.ChangeState(ctr.state.Run); return; }
            if (ctr.InputX == 0) { machine.ChangeState(ctr.state.Idle); return; }
        }
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (ctr.InputX != 0 && ctr.CurrentDir != (int)ctr.InputX) { ctr.Flip(); }
    }

    protected override void StateShoot()
    {
        bool isShoot = ctr.InputShoot;
        Vector2 dir = new Vector2(ctr.InputX, ctr.InputY);
        if (isShoot) { base.StateShoot(); }
        if (dir.x != 0 && dir.y > 0) ctr.aniHash.PlayAniSync(isShoot ? PlayerAnimation.ShotDiagonalUp : PlayerAnimation.AimDiagonalUp);
        else if (dir.x == 0 && dir.y > 0) ctr.aniHash.PlayAniSync(isShoot ? PlayerAnimation.ShotUp : PlayerAnimation.AimUp);
        else if (dir.x != 0 && dir.y < 0) ctr.aniHash.PlayAniSync(isShoot ? PlayerAnimation.ShotDiagonalDown : PlayerAnimation.AimDiagonalDown);
        else if (dir.x == 0 && dir.y < 0) ctr.aniHash.PlayAniSync(isShoot ? PlayerAnimation.ShotDown : PlayerAnimation.AimDown);
        else ctr.aniHash.PlayAniSync(isShoot ? PlayerAnimation.ShotStraight : PlayerAnimation.AimStraight);
    }
}
