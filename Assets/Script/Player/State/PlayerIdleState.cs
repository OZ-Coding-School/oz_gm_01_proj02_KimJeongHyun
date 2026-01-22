using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerController ctr, StateMachine machine) : base(ctr, machine,
        PlayerAnimation.Idle) { }

    public override void Enter()
    {
        base.Enter();
        ctr.PlayerMovement.Stop();
    }

    public override void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.P)) { OnHit(false, new Vector2(1, 0)); }
        if (Input.GetKeyDown(KeyCode.O)) { OnHit(false, new Vector2(-1, 0)); }
        if (ctr.PlayerInputHandler.InputShoot)
        {
            Shooting();
            PlayAni(PlayerAnimation.ShotStraight);
        }
        else { PlayAni(PlayerAnimation.Idle); }

        if (TryDash) { machine.ChangeState(ctr.PlayerState.Dash); return; }
        if (TryShotEX)
        {
            ctr.PlayerStatus.UseEXEnergy();
            machine.ChangeState(ctr.PlayerState.ShotEX); return;
        } 
        if (ctr.PlayerInputHandler.InputJump) { machine.ChangeState(ctr.PlayerState.Jump); return; }
        if (ctr.PlayerInputHandler.InputX != 0) { machine.ChangeState(ctr.PlayerState.Run); return; }
        if (ctr.PlayerInputHandler.InputDuck) { machine.ChangeState(ctr.PlayerState.Duck); return; }
        if (ctr.PlayerInputHandler.InputLock) { machine.ChangeState(ctr.PlayerState.Lock); return; }
    }

    protected override void Shooting()
    {
        ctr.PlayerShooter.Shoot(new Vector2(ctr.PlayerMovement.CurrentDir, 0));
    }
}
