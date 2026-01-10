using System;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerState : BaseState<PlayerController>
{    
    public PlayerState(PlayerController ctr, StateMachine machine) : base(ctr, machine) { }

    public override void StateUpdate()
    {
        if (canFlip && ctr.InputX != 0)
        {
            if (ctr.curDir != (int)ctr.InputX)
            {
                ctr.FlipX();
                ctr.transform.Rotate(0, 180, 0);
            }
        }
    }    

    public override void StateFixedUpdate()
    {
        if (canMove == true)
        {
            float xSpeed = ctr.InputX * ctr.data.moveSpeed;
            ctr.SetVelocityX(xSpeed);
        }
    }

    public override void OnHit(int dir)
    {
        if (ctr.isInvincible) return;
        ctr.state.Hit.SetDir(dir);
        machine.ChangeState(ctr.state.Hit);
    }

    protected virtual void StateShoot()
    {
        Vector2 dir = new Vector2(ctr.InputX, ctr.InputY).normalized;
        if (dir == Vector2.zero) { dir = new Vector2(ctr.curDir, 0); }

        UnityEngine.Transform firePoint = GetFirePoint(dir);
        ctr.PlayerShoot(firePoint, dir);
    }

    protected virtual UnityEngine.Transform GetFirePoint(Vector2 dir)
    {
        bool isInputX = dir.x != 0;
        int index = 0;

        if (dir.y > 0) index = isInputX ? 2 : 4;
        else if (dir.y < 0) index = isInputX ? 1 : 3;
        else index = 0;
        return ctr.firePoint[index];
    }
}
