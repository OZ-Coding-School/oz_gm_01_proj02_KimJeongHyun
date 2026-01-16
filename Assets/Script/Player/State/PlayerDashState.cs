using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using playerAnimation;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(PlayerController ctr, StateMachine machine) : base(ctr, machine,
        PlayerAnimation.DashGround, PlayerAnimation.DashAir) { }

    public override void Enter()
    {
        base.Enter();
        ctr.PlayerMovement.Dash();
    }

    public override void HandleInput()
    {
        if (TryJump) { machine.ChangeState(ctr.PlayerState.Jump); return; }
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (timer >= ctr.PlayerData.DashTime)
        {
            if (ctr.PlayerCollision.IsGround)
            {
                if (ctr.PlayerInputHandler.InputJump) { machine.ChangeState(ctr.PlayerState.Jump); return; }
                if (ctr.PlayerInputHandler.InputX != 0) { machine.ChangeState(ctr.PlayerState.Run); return; }
                if (ctr.PlayerInputHandler.InputX == 0) { machine.ChangeState(ctr.PlayerState.Idle); return; }
                if (ctr.PlayerInputHandler.InputDuck) { machine.ChangeState(ctr.PlayerState.Duck); return; }
                if (ctr.PlayerInputHandler.InputLock) { machine.ChangeState(ctr.PlayerState.Lock); return; }
                if (TrySuper) { machine.ChangeState(ctr.PlayerState.Super); return; }
            }                                             
        }
    }


    public override void Exit()
    {
        ctr.PlayerMovement.SetGravity(ctr.PlayerData.GravityJump);
    }
}
