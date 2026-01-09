using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomKeyMapping;
using playerAnimation;
using System;

public class PlayerController : BaseController
{
    [SerializeField] private PlayerDataSO data;
    public PlayerDataSO Data => data;
    
    public Transform[] firePoint;
    private Transform curFirePoint;

    public PlayerStateData state { get; private set; }
    public AnimationHash<PlayerAnimation> aniHash { get; private set; }
    public PlayerShooting shooter { get; private set; }

    public float InputX { get; private set; }
    public float InputY { get; private set; }
    public bool InputJump { get; private set; }
    public bool InputJumpUp { get; private set; }
    public bool InputDash { get; private set; }
    public bool InputLock { get; private set; }
    public bool InputShoot { get; private set; }
    public bool InputDuck { get; private set; }

    private Vector2 shootDir;


    protected override void Init()
    {
        base.Init();
        aniHash = new AnimationHash<PlayerAnimation>(animator);
        state = new PlayerStateData(this, machine);
        shooter = GetComponent<PlayerShooting>();
    }
    protected override void Start()
    {
        base.Start();
        machine.Init(state.Idle);
    }

    protected override void Update()
    {
        InputX = InputManager.Instance.GetHorizontal();
        InputY = InputManager.Instance.GetVertical();
        InputJump = InputManager.Instance.GetKeyDown(CusKey.Jump);
        InputJumpUp = InputManager.Instance.GetKeyUp(CusKey.Jump);
        InputDash = InputManager.Instance.GetKeyDown(CusKey.Dash);
        InputLock = InputManager.Instance.GetKey(CusKey.Lock);
        InputShoot = InputManager.Instance.GetKey(CusKey.Shoot);
        InputDuck = InputManager.Instance.GetKey(CusKey.Down);

        base.Update();
        if (InputX != 0)
        {
            Flip((int)InputX);
            curDir = (int)InputX;
        }
        machine.CurState?.HandleInput();
        shootDir = (InputX == 0 && InputY == 0) ? new Vector2(curDir, 0) : new Vector2(InputX, InputY);
        curFirePoint = SetFirePoint(shootDir);
        shooter.Shoot(curFirePoint, shootDir);
        Debug.Log(GetCurState().ToString());
    }

    private Transform SetFirePoint(Vector2 dir)
    {
        if (GetCurState() == state.Duck) return firePoint[5];

        bool isInputX = dir.x != 0f;
        int index = dir.y switch
        {
            1f => isInputX ? 2 : 4,
            -1f => isInputX ? 1 : 3,
            _ => 0
        };
        return firePoint[index];
    }
}

