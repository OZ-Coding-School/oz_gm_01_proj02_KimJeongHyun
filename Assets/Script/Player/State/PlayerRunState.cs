using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using playerAnimation;

public class PlayerRunState : PlayerState
{
    public PlayerRunState(PlayerController ctr, StateMachine machine) : base(ctr, machine) { }

    public override void Enter()
    {
        base.Enter();
        if (ctr.InputShoot) ctr.aniHash.PlayAni(PlayerAnimation.RunShot);
        else ctr.aniHash.PlayAni(PlayerAnimation.Run);
    }

    public override void HandleInput()
    {
        base.HandleInput();

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

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (!ctr.isGround) { machine.ChangeState(ctr.state.Fall); return; }
    }
}
