using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using playerAnimation;
using UnityEditor.Experimental.GraphView;
using UnityEngine.Rendering;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(PlayerController ctr, StateMachine machine) : base(ctr, machine) { }

    public override void Enter()
    {
        base.Enter();
        ctr.SetLastDashTime(Time.time);
        ctr.rb.gravityScale = 0;
        ctr.rb.velocity = new Vector2(ctr.CurrentDir * ctr.data.dashSpeed, 0);
        if (ctr.isGround) { ctr.aniHash.PlayAni(PlayerAnimation.DashGround); }
        else ctr.aniHash.PlayAni(PlayerAnimation.DashAir);
    }

    public override void HandleInput()
    {
        if (ctr.InputJump)
        {
            if (ctr.InputDuck && ctr.GetcurPlatform() != null)
            {
                ctr.IgnoreCurPlatform();
                machine.ChangeState(ctr.state.Fall);                
                return;
            }
            if (ctr.isGround) { machine.ChangeState(ctr.state.Jump); return; }
        }
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (timer >= ctr.data.dashTime)
        {
            if (ctr.isGround)
            {
                if (ctr.InputDuck) { machine.ChangeState(ctr.state.Duck); return; }
                if (ctr.InputX != 0) { machine.ChangeState(ctr.state.Run); return; }
                if (ctr.InputX == 0) { machine.ChangeState(ctr.state.Idle); return; }
                if (ctr.InputLock) {  machine.ChangeState(ctr.state.Lock); return; }
            }
            if (!ctr.isGround) { machine.ChangeState(ctr.state.Fall); return; }
        }
    }

    public override void StateFixedUpdate() { }          
    public override void Exit()
    {
        ctr.rb.gravityScale = ctr.data.gravityVal;
    }

}
