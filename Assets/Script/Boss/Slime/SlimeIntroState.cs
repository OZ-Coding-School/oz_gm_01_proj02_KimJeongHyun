using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeIntroState : SlimeState
{
    public SlimeIntroState(SlimeController ctr, StateMachine machine) : base(ctr, machine) { }

    public override void Enter()
    {
        base.Enter();
        SetRandomJumpConut();
        ctr.AniHash.PlayAni(SlimeAnimation.Intro);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (timer > ctr.Anim.GetCurrentAnimatorStateInfo(0).length)
        {
            machine.ChangeState(ctr.SlimeState.Move); return;
        }
    }
}
