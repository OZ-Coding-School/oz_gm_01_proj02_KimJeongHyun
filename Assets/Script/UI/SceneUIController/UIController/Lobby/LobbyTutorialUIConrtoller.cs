using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyTutorialUIConrtoller : BaseUIController
{
    public PlayerController player;
    protected override void OnEnable()
    {
        AudioManager.Instance.PlayBGM(BGMType.Tutorial);        
    }
    protected override void Start()
    {
        player.PlayerStatus.AddEnergy(5f);
    }
}
