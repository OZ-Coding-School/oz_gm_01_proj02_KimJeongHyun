using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using playerAnimation;
public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(PlayerController ctr, StateMachine machine) : base(ctr, machine) { }

    public override void Enter()
    {
        ctr.aniHash.PlayAni(PlayerAnimation.Idle);
    }

    public override void StateUpdate()
    {
    }
    public override void HandleInput()
    {
        if (ctr.InputX != 0) { }
    }

}
