using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeTombAttackState : SlimeState
{
    public SlimeTombAttackState(SlimeController ctr, StateMachine machine) : base(ctr, machine) { }

    public override void Enter()
    {
        base.Enter();
        ctr.StartCoroutine(AttackCo());
    }

    private IEnumerator AttackCo()
    {
        ctr.AniHash.PlayAni(SlimeAnimation.TombAttack);
        yield return null;
        yield return new WaitUntil(() => ctr.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.65f);
        ctr.StartCoroutine(ctr.cam.Shake(0.2f, 0.3f));
        //먼지이펙트
        //공격 사운드
        yield return new WaitUntil(() => ctr.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
        machine.ChangeState(ctr.SlimeState.TombMove);       
    }
}
