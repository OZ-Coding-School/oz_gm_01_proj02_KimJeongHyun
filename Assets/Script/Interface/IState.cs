using UnityEngine;

public interface IState
{    
    void Enter();
    void Exit();
    void HandleInput();
    void StateUpdate();
    void StateFixedUpdate();
    void OnHit(bool isDead, Vector2 hitDir);
}
