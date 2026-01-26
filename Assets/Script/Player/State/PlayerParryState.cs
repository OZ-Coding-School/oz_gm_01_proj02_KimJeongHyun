using UnityEngine;
using System.Collections;

public class PlayerParryState : PlayerState
{
    private bool parrying;
    private bool firstParrying;
    public PlayerParryState(PlayerController ctr, StateMachine machine) : base(ctr, machine, PlayerAnimation.ParryNormal) { }

    public override void Enter()
    {        
        base.Enter();
        SetAirColSize();        
        firstParrying = true;
        ctr.PlayerMovement.SetGravity(ctr.PlayerData.GravityFall);
        Parry();
        audio.StopSFX(SFXType.PeashooterLoop);
    }


    public override void HandleInput()
    {
        if (ctr.PlayerInputHandler.ParryInputBuffer && ctr.PlayerCollision.CanParry && !parrying)
        {
            if (timer > 0.2)
            {
                ctr.PlayerInputHandler.UseParryBuffer();
                Parry();
                return;
            }
        }
        if (ctr.PlayerInputHandler.InputShoot) { Shooting(); }
        if (TryShotEX)
        {
            ctr.PlayerStatus.UseEXEnergy();
            machine.ChangeState(ctr.PlayerState.ShotEX); return;
        }
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        Flip();
        if (timer > 0.35f) { ctr.AniHash.PlayAniSync(PlayerAnimation.Jump); }

        if (ctr.PlayerCollision.IsGround) { machine.ChangeState(ctr.PlayerState.Idle); return; }
    }

    public override void StateFixedUpdate()
    {
        Move();
    }

    //패링효과 함수
    private void Parry()
    {
        Collider2D hitCol = ctr.PlayerCollision.CheckParry();
        if (hitCol != null)
        {
            if (hitCol.TryGetComponent<Iparryable>(out var parryable))
            {
                parryable.OnParry();
            }
        }
        ctr.PlayerStatus.AddParryCount();
        parrying = true;
        timer = 0;
        ctr.Rb.velocity = new Vector2(ctr.Rb.velocity.x, ctr.PlayerData.ParryJumpForce);
        if (!firstParrying) { ctr.AniHash.PlayAniSync(PlayerAnimation.ParrySuccess); }
        ctr.StartCoroutine(HitStop(0.3f));      
    }

    private IEnumerator HitStop(float time)
    {
        float temp = Time.timeScale;
        Time.timeScale = 0;
        audio.PlaySFX(SFXType.PlayerParry);
        ctr.PlayerStatus.AddEnergy(1f);
        Vector2 pos = ctr.PlayerCollision.ParryPoint;
        PlayerEffect fx = pool.SpawnObj<PlayerEffect>(ctr.PlayerData.PlayerEffect, pos, Quaternion.identity);
        EffectHelper.SetRandomEffect(fx);
        fx.PlayEffect(PlayerEffectAniType.ParrySpark);     
        yield return new WaitForSecondsRealtime(time);
        parrying = false;
        Time.timeScale = temp;
        firstParrying = false;
    }

    public override void Exit()
    {
        ctr.PlayerCollision.SetGroundColSize();
        if (ctr.PlayerInputHandler.InputShoot) { audio.PlaySFX(SFXType.PeashooterLoop); }
    }

    protected override void Shooting()
    {
        ctr.PlayerShooter.Shoot(new Vector2(ctr.PlayerMovement.CurrentDir, 0));
    }
}


