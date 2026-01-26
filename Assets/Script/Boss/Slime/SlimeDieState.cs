using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDieState : SlimeState
{
    public SlimeDieState(SlimeController ctr, StateMachine machine) : base(ctr, machine){ }



    public override void Enter()
    {
        ctr.gameObject.layer = LayerMask.NameToLayer("DeadBody");
        ctr.AniHash.PlayAni(SlimeAnimation.Die);
        ctr.SlimeDie();
    }
}
