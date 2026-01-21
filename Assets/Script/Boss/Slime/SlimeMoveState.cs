using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class SlimeMoveState : SlimeState
{
    public SlimeMoveState(SlimeController ctr, StateMachine machine) : base(ctr, machine) { }

    public override void Enter()
    {
        base.Enter();
        ctr.StartCoroutine(JumpCO());
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        ctr.CheckWallFlip();
    }

    private IEnumerator JumpCO()
    {
        ctr.AniHash.PlayFirstFrame(ctr.page == 1 ? SlimeAnimation.Jump : SlimeAnimation.BigJump);
        ctr.jumpCount++;

        yield return null;
        yield return new WaitUntil(() => ctr.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.37f);

        ctr.Rb.velocity = new Vector2(ctr.curDir * 5f, 17f);
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => ctr.IsGround());
        machine.ChangeState(ctr.SlimeState.Idle);
    }
}
