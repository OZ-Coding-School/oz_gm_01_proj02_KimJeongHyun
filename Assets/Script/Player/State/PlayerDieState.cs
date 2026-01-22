using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieState : PlayerState
{
    public PlayerDieState(PlayerController ctr, StateMachine machine) : base(ctr, machine, PlayerAnimation.Death) { }

    public override void Enter()
    {
        base.Enter();
        audio.PlaySFX(SFXType.PlayerDeath);
        audio.StopSFX(SFXType.PeashooterLoop);
        ctr.PlayerMovement.Stop();
        ctr.StartCoroutine(DieCo());
    }
    public override void StateUpdate() { }

    private IEnumerator DieCo()
    {   
        yield return new WaitForSeconds(0.4f);
        ctr.PlayerMovement.SetGravity(0);
        ctr.AniHash.PlayAni(PlayerAnimation.Ghost);
        ctr.Rb.velocity = new Vector2(0, 2f);
        yield return new WaitUntil(() => ctr.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
        yield return new WaitForSeconds(2f);
        ctr.gameObject.SetActive(false);
    }

}
