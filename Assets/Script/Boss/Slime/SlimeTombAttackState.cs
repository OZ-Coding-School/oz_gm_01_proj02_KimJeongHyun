using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeTombAttackState : SlimeState
{
    public SlimeTombAttackState(SlimeController ctr, StateMachine machine) : base(ctr, machine) { }

    public override void Enter()
    {
        base.Enter();
        ctr.AniHash.PlayAni(SlimeAnimation.TombAttack);
    }
}
