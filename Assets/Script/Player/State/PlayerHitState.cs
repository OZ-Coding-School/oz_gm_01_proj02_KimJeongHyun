using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using playerAnimation;

public class PlayerHitState : PlayerState
{
    public PlayerHitState(PlayerController ctr, StateMachine machine) : base(ctr, machine, PlayerAnimation.HitGround, PlayerAnimation.HitAir) { }
}
