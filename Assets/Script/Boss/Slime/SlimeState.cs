using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeState : BaseState<SlimeController>
{
    public SlimeState(SlimeController ctr, StateMachine machine) : base(ctr, machine) { }


    public override void StateUpdate()
    {
        base.StateUpdate();
        CheckPageTrans();
        CheckIsDead();
    }


    private void CheckIsDead()
    {
        if (ctr.IsDead)
        {
            ctr.StopAllCoroutines(); ;
            machine.ChangeState(ctr.SlimeState.Die); 
            return;
        }
    }

    private void CheckPageTrans()
    {
        if (ctr.IsGround() && !ctr.isChange)
        {
            float temp = ctr.curHp / ctr.maxHp;
            if (ctr.page == 1 && temp <= ctr.page2per)
            {
                ctr.StopAllCoroutines();
                ctr.jumpCount = 0;
                machine.ChangeState(ctr.SlimeState.Change); return;
            }
            if (ctr.page == 2 && temp <= ctr.page3per)
            {
                ctr.StopAllCoroutines();
                machine.ChangeState(ctr.SlimeState.Change); return;
            }
        }
    }

    public override void OnHit(bool isDead, Vector2 dir)
    {
                
    }
}
