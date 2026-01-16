using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using playerAnimation;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
public class PlayerLockState : PlayerState
{
    public PlayerLockState(PlayerController ctr, StateMachine machine) : base(ctr, machine, PlayerAnimation.AimStraight) { }

    public override void Enter()
    {
        base.Enter();
        ctr.PlayerMovement.Stop();
    }
    public override void HandleInput()
    {
        Flip();
        if (ctr.PlayerInputHandler.InputShoot)
        {
            Shooting();
            PlayAniSync(CheckShotAni());
        }     
        else { PlayAniSync(CheckAimAni()); }
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (!ctr.PlayerInputHandler.InputLock)
        {
            if (ctr.PlayerInputHandler.InputJump) { machine.ChangeState(ctr.PlayerState.Jump); return; }
            if (ctr.PlayerInputHandler.InputX != 0) { machine.ChangeState(ctr.PlayerState.Run); return; }
            if (ctr.PlayerInputHandler.InputX == 0) { machine.ChangeState(ctr.PlayerState.Idle); return; }
            if (ctr.PlayerInputHandler.InputDuck) { machine.ChangeState(ctr.PlayerState.Duck); return; }
            if (TryDash) { machine.ChangeState(ctr.PlayerState.Dash); return; }
            if (TrySuper) { machine.ChangeState(ctr.PlayerState.Super); return; }
        }
    }

    private PlayerAnimation CheckShotAni()
    {
        Vector2 dir = ctr.PlayerInputHandler.InputDir;
        bool isInputX = Mathf.Abs(dir.x) > 0.01f;

        PlayerAnimation ani = PlayerAnimation.ShotStraight;
        if (dir.y > 0) ani = isInputX ? PlayerAnimation.ShotDiagonalUp : PlayerAnimation.ShotUp;
        else if (dir.y < 0) ani = isInputX ? PlayerAnimation.ShotDiagonalDown : PlayerAnimation.ShotDown;
        else ani = PlayerAnimation.ShotStraight;
        return ani;
    }

    private PlayerAnimation CheckAimAni()
    {
        Vector2 dir = ctr.PlayerInputHandler.InputDir;
        bool isInputX = Mathf.Abs(dir.x) > 0.01f;

        PlayerAnimation ani = PlayerAnimation.AimStraight;
        if (dir.y > 0) ani = isInputX ? PlayerAnimation.AimDiagonalUp : PlayerAnimation.AimUp;
        else if (dir.y < 0) ani = isInputX ? PlayerAnimation.AimDiagonalDown : PlayerAnimation.AimDown;
        else ani = PlayerAnimation.AimStraight;
        return ani;
    }
}
