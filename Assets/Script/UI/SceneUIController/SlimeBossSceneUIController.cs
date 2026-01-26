using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeBossSceneUIController : BaseSceneUIController
{
    //사실상 basebossscene
    public PlayerController player;
    public SlimeController boss;
    public float time;

    protected override void Init()
    {
        base.Init();
        time = 0;
    }

    protected override void Start()
    {        
        base.Start();
        OpenUI(UIType.BossField);
        OpenUI(UIType.BossStatusUI);        
        AudioManager.Instance.PlayBGM(BGMType.BossSlime);
        player.PlayerStatus.OnPlayerDie += OpenLoseUI;
        boss.OnSlimeDie += OpenWinUI;
    }

    protected override void Update()
    {
        base.Update();
        time += Time.deltaTime;
    }


    protected override void ButtonEvent(UIType uiType, ButtonTypeE btnType, int btnID)
    {
        switch (uiType, btnType)
        {
            case (UIType.BossLose, ButtonTypeE.Retry): SceneLoader.Instance.ReloadScene();  break;
            case (UIType.BossLose, ButtonTypeE.BackScene): SceneLoader.Instance.LoadScene(SceneType.Map); break;
            case (_, ButtonTypeE.Exit): Application.Quit(); break;
        }
    }

    private void OpenLoseUI()
    {
        OpenUI(UIType.BossLose);
    }

    private void OpenWinUI()
    {
        OpenUI(UIType.BossWin);
    }
}
