public interface IState
{    
    void Enter();
    void Exit();
    void HandleInput();
    void StateUpdate();
    void StateFixedUpdate();
    
    bool canMove { get; }
}
