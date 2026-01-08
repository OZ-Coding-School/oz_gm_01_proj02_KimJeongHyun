using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using playerAction;
using System;
public class PlayerController : BaseController
{

   [SerializeField] public bool isRight { get; private set; } = true;
   [SerializeField] public float MoveSpeed = 5f;

    protected override void Init()
    {
        base.Init();
        aniHash.InitAniHash<PlayerAction>();
    }
}

