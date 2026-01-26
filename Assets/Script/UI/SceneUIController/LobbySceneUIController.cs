using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using TMPro;
using UnityEngine;

public class LobbySceneUIController : BaseSceneUIController
{
    protected override void Start()
    {
        base.Start();
        OpenUIStack(UIType.LobbyMainUI);
    }   

    protected override void ButtonEvent(UIType uiType, ButtonTypeE btnType, int btnID)
    {
        switch (uiType, btnType)
        {
            case (UIType.LobbyMainUI, ButtonTypeE.Start): OpenUIStack(UIType.GameStartUI); break;
            case (UIType.LobbyMainUI, ButtonTypeE.Option): OpenUIStack(UIType.OptionUI); break;
            case (UIType.OptionUI, ButtonTypeE.Sound): OpenUIStack(UIType.AudioUI); break;
            case (UIType.OptionUI, ButtonTypeE.keySetting): OpenUIStack(UIType.KeySettingUI); break;
            case (UIType.GameStartUI, ButtonTypeE.Start): OnGameSlotSelected(btnID); break;

            case (_, ButtonTypeE.Back): CloseUIStack(); break;
            case (_, ButtonTypeE.Exit): Application.Quit(); break;
                
        }
    }

    private void OnGameSlotSelected(int slotID)
    {
        DataManager.Instance.SelectSlot(slotID);
        DataManager.Instance.SaveUserData();
        var userGameData = DataManager.Instance.GetUserData<UserGameData>();
        GameData data = userGameData.Data;

        bool tutorialclear = data.isTutorialClear;

        if (tutorialclear)
        {
            SceneLoader.Instance.LoadScene(SceneType.Map);
        }
        else
        {
            OpenUIStack(UIType.TutorialUI);
        }
    }
}
