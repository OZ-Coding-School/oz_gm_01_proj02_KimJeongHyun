using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIntroState : PlayerState
{
    public PlayerIntroState(PlayerController ctr, StateMachine machine) : base(ctr, machine, PlayerAnimation.Intro) { }

    public override void Enter()
    {
        base.Enter();
        ctr.StartCoroutine(GoToIdleCo());
    }
    public override void StateUpdate()
    {
        
    }

    private IEnumerator GoToIdleCo()
    {
        yield return null;
        yield return new WaitUntil(() => ctr.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
        machine.ChangeState(ctr.PlayerState.Idle);
    }

    public override void Exit()
    {
        base.Exit();
        if (ctr.PlayerInputHandler.InputShoot) { audio.PlaySFX(SFXType.PeashooterLoop); }
    }
}
