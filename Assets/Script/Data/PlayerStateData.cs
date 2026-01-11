
public class PlayerStateData
{
    public PlayerIdleState Idle { get; private set; }
    public PlayerRunState Run { get; private set; }
    public PlayerJumpState Jump { get; private set; }
    public PlayerFallState Fall { get; private set; }
    public PlayerDuckState Duck { get; private set; }
    public PlayerDashState Dash { get; private set; }
    public PlayerLockState Lock { get; private set; }
    public PlayerHitState Hit { get; private set; }
    public PlayerParryState Parry { get; private set; }
    public PlayerSuperAttackState Super {  get; private set; }

    public PlayerStateData(PlayerController ctr, StateMachine machine)
    {
        Idle = new PlayerIdleState(ctr, machine);
        Run = new PlayerRunState(ctr, machine);
        Jump = new PlayerJumpState(ctr, machine);
        Fall = new PlayerFallState(ctr, machine);
        Duck = new PlayerDuckState(ctr, machine);
        Dash = new PlayerDashState(ctr, machine);
        Lock = new PlayerLockState(ctr, machine);
        Hit = new PlayerHitState(ctr, machine);
        Parry = new PlayerParryState(ctr, machine);
        Super = new PlayerSuperAttackState(ctr, machine);
    }
}
