using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using playerAnimation;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(PlayerController ctr, StateMachine machine) : base(ctr, machine) { }
    protected override bool canJump => false;
    protected override bool canParry => true;
    protected override bool canDuck => false;
    
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
        base.HandleInput();

        if (ctr.rb.velocity.y < 0.01 && ctr.isGround)
        {
            if (ctr.InputX != 0) { machine.ChangeState(ctr.state.Run); return; }
            if (ctr.InputDuck) { machine.ChangeState(ctr.state.Duck); return; }
            else machine.ChangeState(ctr.state.Idle); return;            
        }
        if (ctr.InputDash && ctr._canDash) { machine.ChangeState(ctr.state.Dash); }
        if (ctr.InputShoot) StateShoot();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        if (!isShort && ctr.InputJumpUp)
        {
            if (timer < ctr.data.lowJumpTime)
            {
                ctr.rb.velocity = new Vector2(ctr.rb.velocity.x, ctr.data.lowJumpForce);
                isShort = true;
            }
        }

        if (ctr.rb.velocity.y <= 0) { machine.ChangeState(ctr.state.Fall); return; }
    }
}
