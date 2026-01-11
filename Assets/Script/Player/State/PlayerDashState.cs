using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using playerAnimation;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(PlayerController ctr, StateMachine machine) : base(ctr, machine) { }
    protected override bool canDash => false;
    protected override bool canFlip => false;
    protected override bool canMove => false;
    protected override bool canGravity => false;
    protected override bool canDuck => false;
    protected override bool canLock => false;
    protected override bool canJump => false;
    protected override bool canAttack => false;

    public override void Enter()
    {
        base.Enter();
        ctr.SetCanDash(false);
        ctr.SetLastDashTime(Time.time);
        ctr.rb.velocity = new Vector2(ctr.curDir * ctr.data.dashSpeed, 0);
        if (ctr.isGround) { ctr.aniHash.PlayAni(PlayerAnimation.DashGround); }
        else ctr.aniHash.PlayAni(PlayerAnimation.DashAir);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        if (timer >= ctr.data.dashTime)
        {
            if (ctr.isGround)
            {
                if (ctr.InputX != 0) { machine.ChangeState(ctr.state.Run); return; }
                if (ctr.InputJump) {  machine.ChangeState(ctr.state.Jump); return; }
                else machine.ChangeState(ctr.state.Idle); return;
            }
            else machine.ChangeState(ctr.state.Fall); return;
        }
    }
}
