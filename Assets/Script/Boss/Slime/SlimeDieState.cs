using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDieState : SlimeState
{
    public SlimeDieState(SlimeController ctr, StateMachine machine) : base(ctr, machine){ }



    public override void Enter()
    {
        ctr.StartCoroutine(DieCo());
        ctr.gameObject.layer = LayerMask.NameToLayer("DeadBody");
    }

    private IEnumerator DieCo()
    {
        // 화면 잠깐 멈춤
        //넉아웃 사운드
        //브금 멈추면서 페이드 아웃
        // 승리브금
        ctr.AniHash.PlayAni(SlimeAnimation.Die);
        yield return null;
        ctr.SlimeDie();

    }
}
