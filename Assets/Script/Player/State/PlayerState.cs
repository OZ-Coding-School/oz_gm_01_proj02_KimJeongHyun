using UnityEngine;

public class PlayerState : BaseState<PlayerController>
{
    protected PlayerAnimation groundAni;
    protected PlayerAnimation airAni;
    protected ObjectPoolManager pool;
    protected AudioManager audio;
    public PlayerState(PlayerController ctr, StateMachine machine, PlayerAnimation groundAni) : base(ctr, machine)
    {
        this.groundAni = groundAni;
        this.airAni = groundAni;
    }
    public PlayerState(PlayerController ctr, StateMachine machine, PlayerAnimation groundAni, PlayerAnimation airAni) : base(ctr, machine)
    {
        this.groundAni = groundAni;
        this.airAni = airAni;
    }

    public override void Enter()
    {
        base.Enter();
        PlayEnterAni();
        pool = ObjectPoolManager.Instance;
        audio = AudioManager.Instance;
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        if (ctr.PlayerStatus.IsDead) { machine.ChangeState(ctr.PlayerState.Die); return; }
        if (ctr.PlayerInputHandler.InputShootDown)
        {
            audio.PlaySFX(SFXType.PeashooterLoop);
        }
        if (ctr.PlayerInputHandler.InputShootUp)
        {
            audio.StopSFX(SFXType.PeashooterLoop);
        }
    }

    private void PlayEnterAni()
    {
        if (ctr.PlayerCollision.IsGround) ctr.AniHash.PlayAni(groundAni);
        else ctr.AniHash.PlayAni(airAni);
    }

    protected void PlayAni(PlayerAnimation ani)
    {
        ctr.AniHash.PlayAni(ani);
    }

    protected void PlayAniSync(PlayerAnimation ani)
    {
        ctr.AniHash.PlayAniSync(ani);
    }

    protected void Move()
    {
        ctr.PlayerMovement.Move(ctr.PlayerInputHandler.InputX);
    }

    protected void Flip()
    {
        ctr.PlayerMovement.CheckFlip(ctr.PlayerInputHandler.InputX);
    }

    protected virtual void Shooting()
    {        
        ctr.PlayerShooter.Shoot(ctr.PlayerInputHandler.InputDir);
    }

    public override void OnHit(bool isDead, Vector2 dir)
    {
        if (isDead) { /*machine.ChangeState(ctr.PlayerState.Die); return;*/ }
        else
        {
            float hitDir = ctr.transform.position.x < dir.x ? 1 : -1;
            if (hitDir != ctr.PlayerMovement.CurrentDir)
            {
                ctr.transform.Rotate(0, 180, 0);
                ctr.PlayerMovement.SetCurDir((int)hitDir);
            }
            ctr.Rb.velocity = new Vector2(-hitDir * ctr.PlayerData.KnockbackForceX, ctr.PlayerData.KnockbackForceY);
            machine.ChangeState(ctr.PlayerState.Hit);
        } 
    }      

    protected void SetAirColSize()
    {
        ctr.PlayerCollision.SetJumpColSize();
    }

    protected bool TryJump => ctr.PlayerInputHandler.InputJump && ctr.PlayerMovement.CanJump;
    protected bool TryDash => ctr.PlayerInputHandler.InputDash && ctr.PlayerMovement.CanDash;
    protected bool TryShotEX => ctr.PlayerInputHandler.InputShotEX && ctr.PlayerStatus.CanUseEX && ctr.PlayerShooter.UseEXTime;
    protected bool TryParry => ctr.PlayerInputHandler.ParryInputBuffer && ctr.PlayerCollision.CanParry;
}
