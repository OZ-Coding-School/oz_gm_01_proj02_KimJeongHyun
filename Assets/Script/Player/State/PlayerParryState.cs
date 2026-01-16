using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using playerAnimation;

public class PlayerParryState : PlayerState
{
    public PlayerParryState(PlayerController ctr, StateMachine machine) : base(ctr, machine, PlayerAnimation.Jump) { }
}
