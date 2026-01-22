using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        if (TryShotEX)
        {
            ctr.PlayerStatus.UseEXEnergy();
            machine.ChangeState(ctr.PlayerState.ShotEX); return;
        }
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        Flip();
        if (!ctr.PlayerInputHandler.InputLock) { machine.ChangeState(ctr.PlayerState.Idle); return; }
        if (!ctr.PlayerCollision.IsGround) { machine.ChangeState(ctr.PlayerState.Fall); return; }
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
