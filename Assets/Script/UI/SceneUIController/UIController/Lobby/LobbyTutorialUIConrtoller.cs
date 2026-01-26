using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyTutorialUIConrtoller : BaseUIController
{
    public PlayerController player;
    protected override void Start()
    {
        base.Start();
        player.PlayerStatus.AddEnergy(5f);
        AudioManager.Instance.PlayBGM(BGMType.Tutorial);
    }
}
