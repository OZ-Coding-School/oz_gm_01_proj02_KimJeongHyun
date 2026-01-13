using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParryState : PlayerState
{
    public PlayerParryState(PlayerController ctr, StateMachine machine) : base(ctr, machine) { }

    public override void Enter()
    {
        base.Enter();
        ctr.SetUseDashInAir(false);
    }
    public override void StateUpdate()
    {
        base.StateUpdate();
        if (ctr.InputX != 0 && ctr.CurrentDir != (int)ctr.InputX) { ctr.Flip(); }
    }
}
