public interface IState
{
    void Enter();
    void Exit();
    void StateUpdate();
    void StateFixedUpdate();
}
