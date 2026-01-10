using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using playerAnimation;

public class PlayerHitState : PlayerState
{
    private int dir;
    public PlayerHitState(PlayerController ctr, StateMachine machine) : base(ctr, machine) { }
    protected override bool canMove => false;
    protected override bool canFlip => false;

    public override void Enter()
    {
        ctr.rb.velocity = new Vector2(dir * ctr.data.knockbackForceX, ctr.data.knockbackForceY);
        ctr.aniHash.PlayAni(PlayerAnimation.HitGround);
    }

    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();
        if (ctr.rb.velocity.y <= 0.05 && ctr.isGround)
        {
            if (ctr.InputX != 0) { machine.ChangeState(ctr.state.Run); }
            else { machine.ChangeState(ctr.state.Idle); }
        }
    }

    public void SetDir(int dir)
    {
        this.dir = dir;
    }

    public override void Exit()
    {
        ctr.rb.velocity = Vector2.zero;
    }
}
