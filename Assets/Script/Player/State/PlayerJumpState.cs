using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using playerAnimation;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(PlayerController ctr, StateMachine machine) : base(ctr, machine, PlayerAnimation.Jump) { }

    public override void Enter()
    {
        base.Enter();
        ctr.PlayerMovement.Jump();
    }

    public override void HandleInput()
    {
        Flip();
        if (TryParry) { machine.ChangeState(ctr.PlayerState.Parry); return; }
        if (TryDash) { machine.ChangeState(ctr.PlayerState.Dash); return; }
        if (ctr.PlayerInputHandler.InputJumpUp)
        {
            if (timer < ctr.PlayerData.LowJumpTime && ctr.Rb.velocity.y > 0)
            {
                ctr.Rb.velocity = new Vector2(ctr.Rb.velocity.x, ctr.PlayerData.LowJumpForce);
            }
        }
    }

    public override void StateFixedUpdate()
    {
        Move();
        if (ctr.Rb.velocity.y < 0) { machine.ChangeState(ctr.PlayerState.Fall); return; }
    }
}
