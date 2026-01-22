using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class PlayerHitState : PlayerState
{
    private float flashTime = 0.4f;
    private WaitForSeconds flashTimeWFS = new WaitForSeconds(0.4f);
    public PlayerHitState(PlayerController ctr, StateMachine machine) : base(ctr, machine, PlayerAnimation.HitGround, PlayerAnimation.HitAir) { }

    public override void Enter()
    {
        base.Enter();
        ctr.PlayerStatus.SetInvincible(true);        
        ctr.StartCoroutine(FlashCo(ctr.PlayerData.InvincibilityTIme));
        audio.PlaySFX(SFXType.PlayerHit);
        audio.StopSFX(SFXType.PeashooterLoop);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (timer > 0.5f)
        {
            if (ctr.PlayerCollision.IsGround) { machine.ChangeState(ctr.PlayerState.Idle); return; }
            else { machine.ChangeState(ctr.PlayerState.Fall); return; }
        }
    }

    private IEnumerator FlashCo(float inviTime)
    {
        float time = 0;
        while (time < inviTime)
        {
            ctr.EntityFlasah.PlayFlash();
            yield return flashTimeWFS;
            time += flashTime;
        }
        ctr.PlayerStatus.SetInvincible(false);
    }
    public override void Exit()
    {
        base.Exit();
        if (ctr.PlayerInputHandler.InputShoot) { audio.PlaySFX(SFXType.PeashooterLoop); }
    }
}
