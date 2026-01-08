using animationName;
using System;
using playerAction;

public interface State
{
    void Enter();
    void Exit();
    void StateUpdate();
    void StateFixedUpdate();
}

public abstract class BaseState<T> : State where T : BaseController
{
    protected T ctr;
    protected StateMachine machine;
    protected PlayerAction aniName;


    public BaseState(T ctr, StateMachine machine, PlayerAction aniName)
    {
        this.ctr = ctr;
        this.machine = machine;
        this.aniName = aniName;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void StateUpdate() { }
    public virtual void StateFixedUpdate() { }
}
