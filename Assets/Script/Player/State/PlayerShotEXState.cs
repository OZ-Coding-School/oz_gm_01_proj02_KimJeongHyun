using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotEXState : PlayerState
{
    private bool isShot;
    public PlayerShotEXState(PlayerController ctr, StateMachine machine) : base(ctr, machine, PlayerAnimation.SuperGroundStraight) { }

    public override void Enter()
    {
        base.Enter();
        isShot = true;
        ctr.StartCoroutine(ShootingEX());
    }


    public override void StateUpdate()
    {
        base.StateUpdate();
        if (timer > 0.8 && !isShot)
        {
            if (ctr.PlayerCollision.IsGround) { machine.ChangeState(ctr.PlayerState.Idle); return; }
            else { machine.ChangeState(ctr.PlayerState.Fall); return; }
        }
    }

    private IEnumerator ShootingEX()
    {        
        float temp = ctr.Rb.gravityScale;
        ctr.Rb.velocity = Vector2.zero;
        ctr.Rb.gravityScale = 0;
        Vector3 pos = ctr.transform.position + new Vector3(-0.2f, 0.7f, 0);
        var fx = pool.SpawnObj<PlayerEffect>(ctr.PlayerData.PlayerEffect, pos, Quaternion.identity);
        EffectHelper.SetRandomEffect(fx);
        fx.PlayEffect(PlayerEffectAniType.EXStart);

        yield return new WaitForSeconds(0.25f);

        ctr.PlayerShooter.ShootEX(new Vector2(ctr.PlayerMovement.CurrentDir, 0));
        float knockback = 5f;
        ctr.Rb.velocity = new Vector2(ctr.PlayerMovement.CurrentDir * -1 * knockback, 0);

        yield return new WaitForSeconds(0.1f);
        ctr.Rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.525f);
        ctr.Rb.gravityScale = temp;
        isShot = false;
    }
}
