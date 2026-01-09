using System;

public abstract class BaseState<T> : IState where T : BaseController
{
    protected T ctr;
    protected StateMachine machine;

    public BaseState(T ctr, StateMachine machine)
    {
        this.ctr = ctr;
        this.machine = machine;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void HandleInput() { }
    public virtual void StateUpdate() { }
    public virtual void StateFixedUpdate() { }
}
