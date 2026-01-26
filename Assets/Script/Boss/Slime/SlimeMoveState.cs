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
        ctr.jumpConutCheck++;

        yield return null;
        yield return new WaitUntil(() => ctr.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.37f);

        ctr.Rb.velocity = new Vector2(ctr.curDir * 5f, 17f);
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => ctr.IsGround());
        GameObject dust = Object.Instantiate(ctr.slimeJumpDust, ctr.transform.position, Quaternion.identity);
        Object.Destroy(dust,0.24f);
        audio.PlaySFX(ctr.page == 1 ? SFXType.SlimeJump : SFXType.BigJump);
        ctr.StartCoroutine(ctr.cam.Shake(0.2f, 0.1f));
        machine.ChangeState(ctr.SlimeState.Idle);
    }
}
