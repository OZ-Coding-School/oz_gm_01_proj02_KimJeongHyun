using System;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerState : BaseState<PlayerController>
{    
    public PlayerState(PlayerController ctr, StateMachine machine) : base(ctr, machine) { }

    protected virtual void StateShoot()
    {
        Vector2 dir = new Vector2(ctr.InputX, ctr.InputY).normalized;
        if (dir == Vector2.zero) { dir = new Vector2(ctr.CurrentDir, 0); }

        UnityEngine.Transform firePoint = GetFirePoint(dir);
        ctr.PlayerShoot(firePoint, dir);
    }

    public override void StateFixedUpdate()
    {
        float maxFallSpeed = -15f;
        if (ctr.rb.velocity.y < maxFallSpeed) { ctr.rb.velocity = new Vector2(ctr.rb.velocity.x, maxFallSpeed); }
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
