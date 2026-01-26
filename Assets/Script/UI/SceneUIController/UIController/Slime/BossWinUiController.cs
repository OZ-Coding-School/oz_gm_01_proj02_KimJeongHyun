using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;

public class BossWinUiController : BaseUIController
{
    public PlayerController player;
    public Animator knockout;
    public Animator fadeout;
    public GameObject winUI;
    public GameObject background;
    public GameObject field;
    public TextMeshProUGUI time;
    public TextMeshProUGUI parry;
    public TextMeshProUGUI hpBonus;
    public TextMeshProUGUI grade;


    public SlimeBossSceneUIController scenectr;

    protected override void OnEnable()
    {
        StartCoroutine(WinCo());
    }

    private IEnumerator WinCo()
    {      
        knockout.gameObject.SetActive(true);
        AudioManager.Instance.PlaySFX(SFXType.KnockOut);
        AudioManager.Instance.StopSFX(SFXType.PeashooterLoop);
        Time.timeScale = 0f;
        yield return null;

        yield return new WaitForSecondsRealtime(knockout.GetCurrentAnimatorStateInfo(0).length);
        Time.timeScale = 1.0f;
        knockout.gameObject.SetActive(false);
        fadeout.gameObject.SetActive(true);
        yield return null;

        yield return new WaitUntil(() => fadeout.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        float clearTime = scenectr.time;
        int parryCount = player.PlayerStatus.ParryCount;
        int currentHp = player.PlayerStatus.CurrentHp;
        GameManager.Instance.ProcessBossClear("Slime", clearTime, parryCount, currentHp);

        var result = GameManager.Instance.LastBossResult;

        winUI.SetActive(true);
        field.SetActive(false);
        background.SetActive(true);

        time.text = $"{((int)result.clearTime / 60):00}:{((int)result.clearTime % 60):00}";
        parry.text = $"{result.parryCount}/3";
        hpBonus.text = $"{result.remainingHp}/5";
        grade.text = result.grade;

        AudioManager.Instance.PlayBGM(BGMType.GameWin);
        AudioManager.Instance.StopSFX(SFXType.PeashooterLoop);
        fadeout.gameObject.SetActive(false);
        
        scenectr.time = 0;

        yield return new WaitForSeconds(8f);
        SceneLoader.Instance.LoadScene(SceneType.Map);
    }
}
