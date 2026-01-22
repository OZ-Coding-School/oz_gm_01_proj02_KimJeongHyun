using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeIdleState : SlimeState
{
    public SlimeIdleState(SlimeController ctr, StateMachine machine) : base(ctr, machine) { }

    public override void Enter()
    {
        base.Enter();
        switch (ctr.page)
        {
            case 1: ctr.AniHash.PlayAni(SlimeAnimation.Idle); break;
            case 2: ctr.AniHash.PlayAni(SlimeAnimation.BigIdle); break;
            case 3: ctr.AniHash.PlayAni(SlimeAnimation.TombIdle); break;
        }

        if (ctr.page < 3)
        {
            if (ctr.jumpConutCheck == ctr.jumpCount)
            {
                SetRandomJumpConut();   
                machine.ChangeState(ctr.SlimeState.Attack);
                return;
            }
            else
            {
                machine.ChangeState(ctr.SlimeState.Move); return;
            }
        }
        else
        {
            if (ctr.moveCount >= 3)
            {
                ctr.moveCount = 0;
                machine.ChangeState(ctr.SlimeState.TombAttack);
                return;
            }
            else
            {
                machine.ChangeState(ctr.SlimeState.TombMove); return;
            }

        }
    }
}
