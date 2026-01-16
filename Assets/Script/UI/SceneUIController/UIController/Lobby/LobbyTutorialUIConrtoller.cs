using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyTutorialUIConrtoller : BaseUIController
{
    protected override void OnEnable()
    {
        AudioManager.Instance.PlayBGM(EnumData.BGM.Tutorial);
    }
}
