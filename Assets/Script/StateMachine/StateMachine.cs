
public class StateMachine
{
    public State CurState { get; private set; }

    public void Init(State startState)
    {
        CurState = startState;
        CurState.Enter();
    }

    public void ChangeState(State newState)
    {
        CurState.Exit();
        CurState = newState;
        CurState.Enter();
    }
}
