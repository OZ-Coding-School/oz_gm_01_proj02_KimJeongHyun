using System;
using System.Threading;
using UnityEngine;

public abstract class BaseState<T> : IState where T : Entity
{
    protected T ctr;
    protected StateMachine machine;
    protected float timer;

    public BaseState(T ctr, StateMachine machine)
    {
        this.ctr = ctr;
        this.machine = machine;
    }

    public virtual void Enter() { timer = 0; }
    public virtual void Exit() { }
    public virtual void HandleInput() { }
    public virtual void StateUpdate() { timer += Time.deltaTime; }
    public virtual void StateFixedUpdate() { }
    public virtual void OnHit(bool isDead, Vector2 dir) { }
}
