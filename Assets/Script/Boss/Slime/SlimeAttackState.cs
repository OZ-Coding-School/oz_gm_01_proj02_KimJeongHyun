using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttackState : SlimeState
{
    public SlimeAttackState(SlimeController ctr, StateMachine machine) : base(ctr, machine) { }

    public override void Enter()
    {
        base.Enter();
        ctr.FlipToPlayer();
        ctr.StartCoroutine(AttackCo());
    }

    private IEnumerator AttackCo()
    {
        if (ctr.page == 1)
        {
            ctr.AniHash.PlayAni(SlimeAnimation.Attack);
            yield return null;
            audio.PlaySFX(SFXType.SlimePunch);
            yield return new WaitUntil(() => ctr.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
            machine.ChangeState(ctr.SlimeState.Idle);
        }
        else
        {
            ctr.AniHash.PlayAni(SlimeAnimation.BigAttack);
            yield return null;
            audio.PlaySFX(SFXType.BigPunch);
            yield return new WaitUntil(() => ctr.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
            machine.ChangeState(ctr.SlimeState.Idle);
        }
    }
}
