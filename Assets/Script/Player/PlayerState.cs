using animationName;
using playerAction;
using System;
public class PlayerState : BaseState<PlayerController>
{
    public PlayerState(PlayerController ctr, StateMachine machine, PlayerAction aniName) : base(ctr, machine, aniName) { }

    public override void Enter()
    {
        ctr.aniHash.PlayAni(aniName);
    }
}
