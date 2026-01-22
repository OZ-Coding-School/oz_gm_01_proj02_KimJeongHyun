using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeChangeState : SlimeState
{
    public SlimeChangeState(SlimeController ctr, StateMachine machine) : base(ctr, machine) { }

    public override void Enter()
    {
        base.Enter();
        ctr.StartCoroutine(ChangeCo());
    }

    private IEnumerator ChangeCo()
    {
        ctr.isChange = true;
        if (ctr.page == 1)
        {
            ctr.AniHash.PlayFirstFrame(SlimeAnimation.ChangePageTwo);
            SetRandomJumpConut();
            yield return null;
            yield return new WaitUntil(() => ctr.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
        }
        else
        {
            ctr.AniHash.PlayFirstFrame(SlimeAnimation.ChangePageThree);
            //yield return new WaitForSeconds(1f);
            float posX = ctr.transform.position.x;
            float posY = ctr.transform.position.y + 90f;
            Vector3 spawn = new Vector3(posX, posY, 0);

            GameObject tomb = Object.Instantiate(ctr.tombfall, spawn, Quaternion.identity);
            yield return new WaitForSeconds(2.5f);
            ctr.AniHash.PlayAni(SlimeAnimation.SlimeExplode);
            GameObject tombDust = Object.Instantiate(ctr.tombIntroDust, new Vector3(ctr.transform.position.x, ctr.transform.position.y - 2f, ctr.transform.position.z), Quaternion.identity);

            ctr.StartCoroutine(ctr.cam.Shake(0.3f, 0.5f));


            yield return new WaitForSeconds(1.8f);
            Object.Destroy(tomb);
            Object.Destroy(tombDust);
        }
        ctr.page++;
        machine.ChangeState(ctr.page == 2 ? ctr.SlimeState.Move : ctr.SlimeState.TombMove);
        ctr.isChange = false;
    }
}
