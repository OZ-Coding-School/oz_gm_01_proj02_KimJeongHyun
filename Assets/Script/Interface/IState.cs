using System.Numerics;

public interface IState
{    
    void Enter();
    void Exit();
    void HandleInput();
    void StateUpdate();
    void StateFixedUpdate();
    void OnHit(Vector2 dir) { }
}
