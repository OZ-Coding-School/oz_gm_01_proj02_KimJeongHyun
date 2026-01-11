using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using playerAnimation;
public class PlayerLockState : PlayerState
{
    public PlayerLockState(PlayerController ctr, StateMachine machine) : base(ctr, machine) { }
    protected override bool canMove => false;
    protected override bool canLock => false;
    protected override bool canDuck => false;
    protected override bool canJump => false;

    public override void Enter()
    {
        base.Enter();
        ctr.rb.velocity = new Vector2 (0, ctr.rb.velocity.y);
        if (ctr.InputShoot) ctr.aniHash.PlayAni(PlayerAnimation.ShotStraight);
        else ctr.aniHash.PlayAni(PlayerAnimation.AimStraight);
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (!ctr.InputLock)
        {
            if (ctr.InputX != 0) { machine.ChangeState(ctr.state.Run); return; }
            if (ctr.InputDuck) { machine.ChangeState(ctr.state.Duck); return; }
            else machine.ChangeState(ctr.state.Idle); return;
        }      
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        LockAnimation();
    }

    private void LockAnimation()
    {
        bool isShoot = ctr.InputShoot;
        Vector2 dir = new Vector2(ctr.InputX, ctr.InputY);
        if (isShoot) { StateShoot(); }
        if (dir.x != 0 && dir.y > 0) ctr.aniHash.PlayAniSync(isShoot ? PlayerAnimation.ShotDiagonalUp : PlayerAnimation.AimDiagonalUp);
        else if (dir.x == 0 && dir.y > 0) ctr.aniHash.PlayAniSync(isShoot ? PlayerAnimation.ShotUp : PlayerAnimation.AimUp);
        else if (dir.x != 0 && dir.y < 0) ctr.aniHash.PlayAniSync(isShoot ? PlayerAnimation.ShotDiagonalDown : PlayerAnimation.AimDiagonalDown);
        else if (dir.x == 0 && dir.y < 0) ctr.aniHash.PlayAniSync(isShoot ? PlayerAnimation.ShotDown : PlayerAnimation.AimDown);
        else ctr.aniHash.PlayAniSync(isShoot ? PlayerAnimation.ShotStraight : PlayerAnimation.AimStraight);

    }
}
