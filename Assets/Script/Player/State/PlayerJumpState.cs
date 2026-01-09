using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using playerAnimation;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(PlayerController ctr, StateMachine machine) : base(ctr, machine) { }

    public override void Enter()
    {
        ctr.rb.velocity = new Vector2(ctr.rb.velocity.x, ctr.Data.jumpForce);
        ctr.aniHash.PlayAni(PlayerAnimation.Jump);
    }

    public override void HandleInput()
    {
        if (ctr.rb.velocity.y <=0 && ctr.isGround)
        {
            if (ctr.InputX != 0) machine.ChangeState(ctr.state.Run);
            if (ctr.InputDuck) machine.ChangeState(ctr.state.Duck);
            else machine.ChangeState(ctr.state.Idle);
            return;
        }

        if (ctr.InputShoot) StateShoot();
    }

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();
    }
}
