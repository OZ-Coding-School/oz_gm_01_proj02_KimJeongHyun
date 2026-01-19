using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSuperBeamState : PlayerState
{
    public PlayerSuperBeamState(PlayerController ctr, StateMachine machine) : base(ctr, machine, PlayerAnimation.SuperBeam) { }

}
