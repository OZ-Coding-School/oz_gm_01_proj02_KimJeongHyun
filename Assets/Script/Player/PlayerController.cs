using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using playerAnimation;
using System;
public class PlayerController : BaseController
{
    [SerializeField] private PlayerDataSO data;
    public PlayerDataSO Data => data;

    public PlayerStateData state {  get; private set; }
    public AnimationHash<PlayerAnimation> aniHash { get; private set; }

    [SerializeField] public bool isRight { get; private set; } = true;
    [SerializeField] public float MoveSpeed = 5f;

    protected override void Init()
    {
        base.Init();
        aniHash = new AnimationHash<PlayerAnimation>(animator);
        state = new PlayerStateData(this, machine);
        machine.Init(state.Idle);
    }

    protected override void Update()
    {
        base.Update();

    }

    private void Shooting()
    {

        if (machine.CurState == state.Dash || machine.CurState == state.Hit) return;

    }
}

