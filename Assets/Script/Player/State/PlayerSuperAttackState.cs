using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSuperAttackState : PlayerState
{
    public PlayerSuperAttackState(PlayerController ctr, StateMachine machine) : base(ctr, machine, PlayerAnimation.SuperBeam) { }
}
