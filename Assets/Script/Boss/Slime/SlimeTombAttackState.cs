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
        audio.PlaySFX(SFXType.TombAttack);
        yield return new WaitUntil(() => ctr.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.65f);
        ctr.StartCoroutine(ctr.cam.Shake(0.2f, 0.3f));
        yield return new WaitUntil(() => ctr.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
        machine.ChangeState(ctr.SlimeState.TombMove);       
    }
}
