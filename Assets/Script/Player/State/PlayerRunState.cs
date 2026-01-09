using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using playerAnimation;

public class PlayerRunState : PlayerState
{
    public PlayerRunState(PlayerController ctr, StateMachine machine) : base(ctr, machine) { }

    public override void Enter()
    {
        if (ctr.InputShoot) ctr.aniHash.PlayAni(PlayerAnimation.RunShot);
        else ctr.aniHash.PlayAni(PlayerAnimation.Run);
    }

    public override void HandleInput()
    {
        if (ctr.InputX == 0) { machine.ChangeState(ctr.state.Idle); return; }
        if (ctr.InputJump) { machine.ChangeState(ctr.state.Jump); return; }
        if (ctr.InputDuck) { machine.ChangeState(ctr.state.Duck); return; }
        if (ctr.InputLock) {  machine.ChangeState(ctr.state.Lock); return; }
        if (ctr.InputShoot) 
        {
            if (ctr.InputY != 0) ctr.aniHash.PlayAni(PlayerAnimation.RunDiagonalUpShot);  
            else ctr.aniHash.PlayAni(PlayerAnimation.RunShot);
            StateShoot();
        }
        else
        {
            ctr.aniHash.PlayAni(PlayerAnimation.Run);
        }
    }


    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();    
    }
}
