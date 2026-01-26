using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossLoseUIController : BaseUIController
{
    public PlayerController player;
    public BaseBossController boss;

    public GameObject page1;
    public GameObject page2;
    public GameObject page3;
    public GameObject youDieText;
    public GameObject board;
    public Slider runCupHead;

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(GameOverCo());        
    }

    private IEnumerator GameOverCo()
    {
        yield return new WaitForSeconds(1f);
        youDieText.SetActive(true);
        AudioManager.Instance.PlayBGM(BGMType.GameOver);
        AudioManager.Instance.PlaySFX(SFXType.TombSmile);
        yield return new WaitForSeconds(2f);

        youDieText.SetActive(false);
        board.SetActive(true);
        switch (boss.page)
        {
            case 1: page1.gameObject.SetActive(true); break;
            case 2: page2.gameObject.SetActive(true); break;
            case 3: page3.gameObject.SetActive(true); break;
        }
        float target = Mathf.Clamp01((boss.maxHp - boss.curHp) / boss.maxHp);
        runCupHead.value = 0f;
        yield return new WaitForSeconds(1f);
        while (runCupHead.value < target)
        {
            runCupHead.value += Time.unscaledDeltaTime * 0.3f;
            yield return null;
        }
    }
}
