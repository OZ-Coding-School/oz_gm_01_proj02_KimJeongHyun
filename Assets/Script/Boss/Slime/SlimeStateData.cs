using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeStateData
{
    public SlimeIdleState Idle {  get; private set; }
    public SlimeMoveState Move { get; private set; }
    public SlimeAttackState Attack { get; private set; }
    public SlimeIntroState Intro { get; private set; }
    public SlimeChangeState Change { get; private set; }
    public SlimeTombMoveState TombMove { get; private set; }
    public SlimeTombAttackState TombAttack { get; private set; }
    public SlimeDieState Die { get; private set; }

    public SlimeStateData(SlimeController ctr, StateMachine machine)
    {
        Idle = new SlimeIdleState(ctr, machine);
        Move = new SlimeMoveState(ctr, machine);
        Attack = new SlimeAttackState(ctr, machine);
        Intro = new SlimeIntroState(ctr, machine);
        Change = new SlimeChangeState(ctr, machine);
        TombMove = new SlimeTombMoveState(ctr, machine);
        TombAttack = new SlimeTombAttackState(ctr, machine);
        Die = new SlimeDieState(ctr, machine);
    }
}
