using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using playerAnimation;

public class PlayerRunState : PlayerState
{
    public PlayerRunState(PlayerController ctr, StateMachine machine) : base(ctr, machine, PlayerAnimation.Run) { }

    public override void HandleInput()
    {        
        if (ctr.PlayerInputHandler.InputShoot)
        {
            Shooting();
            PlayAni(CheckShotAni());
        }
        else { PlayAni(PlayerAnimation.Run); }
        if (ctr.PlayerInputHandler.InputX == 0) { machine.ChangeState(ctr.PlayerState.Idle); return; }
        if (ctr.PlayerInputHandler.InputJump) { machine.ChangeState(ctr.PlayerState.Jump); return; }
        if (ctr.PlayerInputHandler.InputDuck) { machine.ChangeState(ctr.PlayerState.Duck); return; }
        if (ctr.PlayerInputHandler.InputLock) { machine.ChangeState(ctr.PlayerState.Lock); return; }
        if (TryDash) { machine.ChangeState(ctr.PlayerState.Dash); return; }
        if (TrySuper) { machine.ChangeState(ctr.PlayerState.Super); return; }
    }    

    public override void StateUpdate()
    {
        base.StateUpdate();
        Flip();
    }

    public override void StateFixedUpdate()
    {
        Move();
        base.StateFixedUpdate();
    }

    protected override void Shooting()
    {
        if (ctr.PlayerInputHandler.InputY >= 0) { ctr.PlayerShooter.Shoot(ctr.PlayerInputHandler.InputDir); }
    }

    private PlayerAnimation CheckShotAni()
    {
        Vector2 dir = ctr.PlayerInputHandler.InputDir;
        bool isInputX = Mathf.Abs(dir.x) > 0.01f;
        PlayerAnimation ani = PlayerAnimation.RunShot;
        if (dir.y > 0) ani = isInputX ? PlayerAnimation.RunDiagonalUpShot : PlayerAnimation.RunShot;
        else ani = PlayerAnimation.RunShot;                
        return ani;
    }
}
