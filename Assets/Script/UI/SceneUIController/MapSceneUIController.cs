using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using UnityEngine;
using static UnityEngine.UI.Slider;

public class MapSceneUIController : BaseSceneUIController
{
    public TopDownPlayerController player;

    protected override void Start()
    {        
        base.Start();
        OpenUI(UIType.MapWorldUI);
        player.SetCanMove(true);
        AudioManager.Instance.PlayBGM(BGMType.MapOne);
    } 

    protected override void ButtonEvent(UIType uiType, ButtonTypeE btnType, int btnID)
    {
        switch (uiType, btnType)
        {
            case (UIType.MapBossSlimeUI, ButtonTypeE.Start):
                GameManager.Instance.SaveMapPosition();
                AudioManager.Instance.PlaySFX(SFXType.BossStartBtn);
                SceneLoader.Instance.LoadScene(SceneType.BossSlime);              
                break;
            case (UIType.MapBossSlimeUI, ButtonTypeE.Close):
                CloseUI(UIType.MapBossSlimeUI);
                break;

        }
    }

    protected override void OpenUI(UIType type)
    {
        base.OpenUI(type);
        player.SetCanMove(false);
    }

    protected override void CloseUI(UIType type)
    {
        base.CloseUI(type);
        player.SetCanMove(true);
    }

    protected override void OpenUIStack(UIType type)
    {
        base.OpenUIStack(type);
        player.SetCanMove(false);
    }

    protected override void CloseUIStack()
    {
        base.CloseUIStack();
        player.SetCanMove(false);
    }

}
