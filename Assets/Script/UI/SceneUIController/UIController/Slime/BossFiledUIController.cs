using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFiledUIController : BaseUIController   
{
    public Animator anim;
    protected override void Init()
    {
        hasBtn = false;
    }

    protected override void OnEnable()
    {
        StartCoroutine(BossIntroCo());
    }

    private IEnumerator BossIntroCo()
    {
        yield return new WaitForSeconds(0.35f);
        AudioManager.Instance.PlaySFX(SFXType.NarratorOne);
        float temp = AudioManager.Instance.GetSFXLength(SFXType.NarratorOne);
        yield return new WaitForSeconds(0.2f);

        anim.gameObject.SetActive(true);
        anim.Play("Ready");
        yield return new WaitForSeconds(temp - 0.4f);
        AudioManager.Instance.PlaySFX(SFXType.NarratorTwo);
        yield return new WaitForSeconds(0.3f);
        anim.gameObject.SetActive(false);
        //사운드
    }
    
}
