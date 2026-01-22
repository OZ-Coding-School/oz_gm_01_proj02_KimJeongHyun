using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeBossSceneUIController : BaseSceneUIController
{
    private Dictionary<UIType, BaseUIController> uiList = new Dictionary<UIType, BaseUIController>();
    private BaseUIController[] baseCnt;
    public PlayerController player;
    public SlimeController boss;

    protected override void Init()
    {
        base.Init();
        baseCnt = GetComponentsInChildren<BaseUIController>(true);

        foreach (var cnt in baseCnt)
        {
            uiList[cnt.uiType] = cnt;
            cnt.gameObject.SetActive(false);
        }
    }

    protected override void OnEnable()
    {
        foreach (var cnts in baseCnt)
        {
            cnts.ButtonClicked += ButtonEvent;
        }
    }


    protected override void OnDisable()
    {
        player.PlayerStatus.OnPlayerDie -= OpenLoseUI;
        boss.OnSlimeDie -= OpenWinUI;
        foreach (var cnts in baseCnt)
        {
            cnts.ButtonClicked -= ButtonEvent;
        }
    }

    protected override void Start()
    {        
        OpenUI(UIType.BossField);
        OpenUI(UIType.BossStatusUI);
        AudioManager.Instance.PlayBGM(BGMType.BossSlime);
        player.PlayerStatus.OnPlayerDie += OpenLoseUI;
        boss.OnSlimeDie += OpenWinUI;
    }

    private void ButtonEvent(UIType uiType, ButtonTypeE btnType, int btnID)
    {
        switch (uiType, btnType)
        {
            case (UIType.BossLose, ButtonTypeE.Retry): SceneLoader.Instance.ReloadScene();  break;
            case (UIType.BossLose, ButtonTypeE.BackScene): SceneLoader.Instance.LoadScene(SceneType.Map); break;
            case (_, ButtonTypeE.Exit): Application.Quit(); break;
        }
    }

    private void OpenUI(UIType type)
    {
        uiList[type].gameObject.SetActive(true);
    }

    private void CloseUI(UIType type)
    {
        uiList[type].gameObject.SetActive(false);
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
