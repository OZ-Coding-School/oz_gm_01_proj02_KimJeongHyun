using System;
using UnityEngine;

public class PlayerState : BaseState<PlayerController>
{    
    public PlayerState(PlayerController ctr, StateMachine machine) : base(ctr, machine) { }
    public override bool canMove => base.canMove;

    public override void StateFixedUpdate()
    {
        float xSpeed = ctr.InputX * ctr.Data.moveSpeed;
        ctr.SetVelocityX(xSpeed);
    }

    public override void HandleInput()
    {
        if (ctr.InputShoot)
        {
            StateShoot();
        }
    }

    protected virtual void StateShoot()
    {
        Vector2 dir = new Vector2(ctr.InputX, ctr.InputY).normalized;
        if (dir == Vector2.zero) { dir = new Vector2(ctr.curDir, 0); }

        Transform firePoint = GetFirePoint(dir);
        ctr.PlayerShoot(firePoint, dir);
    }

    protected virtual Transform GetFirePoint(Vector2 dir)
    {
        bool isInputX = dir.x != 0;
        int index = 0;

        if (dir.y > 0) index = isInputX ? 2 : 4;
        else if (dir.y < 0) index = isInputX ? 1 : 3;
        else index = 0;
        return ctr.firePoint[index];
    }
}
