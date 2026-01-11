using CustomKeyMapping;
using playerAnimation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : BaseController
{
    [SerializeField] private PlayerDataSO _data;
    public PlayerDataSO data => _data;

    public AnimationHash<PlayerAnimation> aniHash { get; private set; }
    public PlayerShooting shooter { get; private set; }
    public PlayerStateData state { get; private set; }

    private InputManager input;
    public float InputX { get; private set; }
    public float InputY { get; private set; }
    public bool InputJump { get; private set; }
    public bool InputJumpUp { get; private set; }
    public bool InputDash { get; private set; }
    public bool InputLock { get; private set; }
    public bool InputShoot { get; private set; }
    public bool InputDuck { get; private set; }
    public bool InputSuper { get; private set; }

    public bool isInvincible { get; private set; } = false;
    public int hitDir { get; private set; }
    public bool _canDash { get; private set; } = true;
    public float lastDashTime { get; private set; }
    public float currentEnergy { get; private set; }
    public bool HasEnergy => currentEnergy >= 1;
    public bool isEnergyFull => currentEnergy >= data.maxEnergy;
    
    [SerializeField] private Transform[] _firePoint;
    public Transform[] firePoint => _firePoint;


    
    //temp
    public TextMeshProUGUI text;

    protected override void Init()
    {
        base.Init();
        aniHash = new AnimationHash<PlayerAnimation>(animator);
        state = new PlayerStateData(this, machine);
        shooter = GetComponentInChildren<PlayerShooting>();
        input = InputManager.Instance;

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

        machine.CurState.HandleInput();
        machine.CurState.StateUpdate();
        text.text = GetCurState().ToString();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        machine.CurState.StateFixedUpdate();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            float hitX = collision.GetContact(0).point.x;
            int dir = (transform.position.x < hitX) ? -1 : 1;
            machine.CurState.OnHit(dir);
        }      
    }

    public void AddEnergy(float val)
    {
        currentEnergy = Mathf.Clamp(currentEnergy + val, 0, data.maxEnergy);
    }

    public void UseEnergy(float val)
    {
        currentEnergy = Mathf.Max(0, currentEnergy - val);
    }    

    public void SetCanDash(bool dash)
    {
        _canDash = dash;
    }

    public void SetLastDashTime(float time)
    {
        lastDashTime = time;
    }

    public void PlayerShoot(Transform trs, Vector2 dir)
    {
        shooter.Shoot(trs, dir);
    }

    private void GetInput()
    {
        InputX = input.GetHorizontal();
        InputY = input.GetVertical();
        InputJump = input.GetKeyDown(CusKey.Jump);
        InputJumpUp = input.GetKeyUp(CusKey.Jump);
        InputDash = input.GetKeyDown(CusKey.Dash);
        InputLock = input.GetKey(CusKey.Lock);
        InputShoot = input.GetKey(CusKey.Shoot);
        InputDuck = input.GetKey(CusKey.Down);
        InputSuper = input.GetKey(CusKey.Super);
    }
}

