using UnityEngine;

public class PlayerState : BaseState<PlayerController>
{
    protected PlayerAnimation groundAni;
    protected PlayerAnimation airAni;

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
    protected bool TryJump => ctr.PlayerInputHandler.InputJump && ctr.PlayerMovement.CanJump;
    protected bool TryDash => ctr.PlayerInputHandler.InputDash && ctr.PlayerMovement.CanDash;
    protected bool TrySuper => ctr.PlayerInputHandler.InputSuper && ctr.PlayerShooter.CanSuper;
    protected bool TryParry => ctr.PlayerInputHandler.InputJump && ctr.PlayerCollision.CanParry;
}
