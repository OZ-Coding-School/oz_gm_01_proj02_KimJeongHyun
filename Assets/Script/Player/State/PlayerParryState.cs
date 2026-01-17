using UnityEngine;
using System.Collections;
//최초 진입시 함수1번호출 input에서 jump키와 canparry체크해서 함수계속 호출 패링 실패시 fall전환
public class PlayerParryState : PlayerState
{
    public PlayerParryState(PlayerController ctr, StateMachine machine) : base(ctr, machine, PlayerAnimation.ParryNormal) { }

    public override void Enter()
    {
        base.Enter();
        Parry();
    }


    public override void HandleInput()
    {
        if (ctr.PlayerInputHandler.InputJump && ctr.PlayerCollision.CanParry) { Parry(); return; }

    }

    public override void StateFixedUpdate()
    {
        Move();
    }

    //패링효과 함수
    private void Parry()
    {
        ctr.Rb.velocity = new Vector2(ctr.Rb.velocity.x, ctr.PlayerData.ParryJumpForce);
        ctr.StartCoroutine(HitStop(0.1f));

    }

    private IEnumerator HitStop(float time)
    {
        float temp = Time.timeScale;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = temp;
    }
}


