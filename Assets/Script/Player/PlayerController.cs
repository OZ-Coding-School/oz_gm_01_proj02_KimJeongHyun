using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomKeyMapping;
using playerAnimation;
using System;
using TMPro;

public class PlayerController : BaseController
{
    [SerializeField] private PlayerDataSO data;
    public PlayerDataSO Data => data;
    public AnimationHash<PlayerAnimation> aniHash { get; private set; }
    public PlayerShooting shooter { get; private set; }
    public PlayerStateData state { get; private set; }

    public float InputX { get; private set; }
    public float InputY { get; private set; }
    public bool InputJump { get; private set; }
    public bool InputJumpUp { get; private set; }
    public bool InputDash { get; private set; }
    public bool InputLock { get; private set; }
    public bool InputShoot { get; private set; }
    public bool InputDuck { get; private set; }

    public Transform[] firePoint;

    public TextMeshProUGUI text;

    protected override void Init()
    {
        base.Init();
        aniHash = new AnimationHash<PlayerAnimation>(animator);
        state = new PlayerStateData(this, machine);
        shooter = GetComponentInChildren<PlayerShooting>();
    }
    protected override void Start()
    {
        base.Start();
        machine.Init(state.Idle);
    }

    protected override void Update()
    {
        base.Update();
        GetInput();

        if (InputX != 0)
        {
            Flip((int)InputX);
            curDir = (int)InputX;
        }

        machine.CurState?.HandleInput();
        machine.CurState?.StateUpdate();
        text.text = GetCurState().ToString();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        machine.CurState?.StateFixedUpdate();
    }

    public void SetVelocityX(float x)
    {
        rb.velocity = new Vector2(x, rb.velocity.y);
    }

    public void PlayerShoot(Transform trs, Vector2 dir)
    {
        shooter.Shoot(trs, dir);
    }

    private void GetInput()
    {
        InputX = InputManager.Instance.GetHorizontal();
        InputY = InputManager.Instance.GetVertical();
        InputJump = InputManager.Instance.GetKeyDown(CusKey.Jump);
        InputJumpUp = InputManager.Instance.GetKeyUp(CusKey.Jump);
        InputDash = InputManager.Instance.GetKeyDown(CusKey.Dash);
        InputLock = InputManager.Instance.GetKey(CusKey.Lock);
        InputShoot = InputManager.Instance.GetKey(CusKey.Shoot);
        InputDuck = InputManager.Instance.GetKey(CusKey.Down);
    }
}

