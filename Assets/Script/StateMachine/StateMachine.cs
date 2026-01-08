
public class StateMachine
{
    public IState CurState { get; private set; }

    public void Init(IState startState)
    {
        CurState = startState;
        CurState.Enter();
    }

    public void ChangeState(IState newState)
    {
        CurState.Exit();
        CurState = newState;
        CurState.Enter();
    }
}
