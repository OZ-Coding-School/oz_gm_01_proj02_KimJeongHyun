using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SlimeTombMoveState : SlimeState
{
    private float atkCooldown;
    public SlimeTombMoveState(SlimeController ctr, StateMachine machine) : base(ctr, machine) { }

    public override void Enter()
    {
        base.Enter();
        atkCooldown = Random.Range(2f, 5f);
        if (Mathf.Sign(ctr.curDir) != Mathf.Sign(ctr.Rb.velocity.x) && ctr.Rb.velocity.x != 0)
        {
            ctr.transform.Rotate(0, 180, 0);
        }
        ctr.AniHash.PlayAni(SlimeAnimation.TombMove);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (timer >= atkCooldown && Mathf.Abs(ctr.transform.position.x - ctr.playerTrs.position.x) < 0.6f)
            machine.ChangeState(ctr.SlimeState.TombAttack);

    }

    public override void StateFixedUpdate()
    {
        ctr.Rb.velocity = new Vector2(ctr.curDir * ctr.tombSpeed, ctr.Rb.velocity.y);
    }

    public override void Exit()
    {
        ctr.Rb.velocity = Vector2.zero;
    }
}
