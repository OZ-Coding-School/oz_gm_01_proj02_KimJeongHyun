using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using playerAnimation;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(PlayerController ctr, StateMachine machine) : base(ctr, machine) { }
    
    private bool isShort;

    public override void Enter()
    {
        base.Enter();
        isShort = false;     
        ctr.rb.velocity = new Vector2(ctr.rb.velocity.x, ctr.data.jumpForce);
        ctr.aniHash.PlayAni(PlayerAnimation.Jump);
    }

    public override void HandleInput()
    {
        if (ctr.InputShoot) StateShoot();
        if (ctr.InputDash && ctr.canDash) { machine.ChangeState(ctr.state.Dash); return; }

    }
    public override void StateUpdate()
    {
        base.StateUpdate();
        if (ctr.InputX != 0 && ctr.CurrentDir != (int)ctr.InputX) { ctr.Flip(); }
        if (!isShort && ctr.InputJumpUp)
        {
            if (timer < ctr.data.lowJumpTime && ctr.rb.velocity.y > 0)
            {
                ctr.rb.velocity = new Vector2(ctr.rb.velocity.x, ctr.data.lowJumpForce);
                isShort = true;
            }
        }        
    }
    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();
        ctr.MovementX();
        if (ctr.rb.velocity.y <= 0) { machine.ChangeState(ctr.state.Fall); return; }
    }
}
