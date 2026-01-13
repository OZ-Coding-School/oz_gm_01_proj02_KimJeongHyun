using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSuperAttackState : PlayerState
{
    public PlayerSuperAttackState(PlayerController ctr, StateMachine machine) : base(ctr, machine) { }
    protected virtual bool canMove => false;
    protected virtual bool canFlip => false;
    protected virtual bool canGravity => false;
    protected virtual bool canAttack => false;
    protected virtual bool canJump => false;
    protected virtual bool canDash => false;
    protected virtual bool canDuck => false;
    protected virtual bool canLock => false;
    protected virtual bool canParry => false;
    protected virtual bool canSuperAttack => false;
}
