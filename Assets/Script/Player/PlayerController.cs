using CustomKeyMapping;
using playerAnimation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : BaseController
{
    //@인풋
    #region
    public float InputX { get; private set; }
    public float InputY { get; private set; }
    public bool InputJump { get; private set; }
    public bool InputJumpUp { get; private set; }
    public bool InputDash { get; private set; }
    public bool InputLock { get; private set; }
    public bool InputShoot { get; private set; }
    public bool InputDuck { get; private set; }
    public bool InputSuper { get; private set; }
    #endregion

    //@데이터
    [SerializeField] private PlayerDataSO _data;
    public PlayerDataSO data => _data;


    //@컴포넌트
    public AnimationHash<PlayerAnimation> aniHash { get; private set; }
    public PlayerShooting shooter { get; private set; }
    public PlayerStateData state { get; private set; }
    [SerializeField] private BoxCollider2D bodyCol;
    [SerializeField] private BoxCollider2D footCol;
    [SerializeField] private BoxCollider2D hitboxCol;
    public Collider2D currentPlatform { get; private set; }
    private Collider2D ignorePlatform;
    private InputManager input;

    //@인게임데이터
    public float lastDashTime { get; private set; }
    public float currentEnergy { get; private set; }
   
    public bool useDashInAir { get; private set; }
    public bool canDash => (Time.time >= lastDashTime + data.dashCooldown) && (isGround || !useDashInAir);
    public bool HasEnergy => currentEnergy >= 1;
    public bool isEnergyFull => currentEnergy >= data.maxEnergy;

    //트랜스폼
    [SerializeField] private Transform[] _firePoint;
    public Transform[] firePoint => _firePoint;

    protected override void Init()
    {
        base.Init();
        CurrentDir = 1;
        aniHash = new AnimationHash<PlayerAnimation>(animator);
        state = new PlayerStateData(this, machine);
        shooter = GetComponentInChildren<PlayerShooting>();               

    }
    protected override void Start()
    {
        base.Start();
        machine.Init(state.Idle);
        input = InputManager.Instance;
    }

    protected override void Update()
    {
        GetInput();
        base.Update();
        if (isGround) useDashInAir = false;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void OnTriggerEnter2D(Collider2D Enemy)
    {
        if (Enemy.gameObject.CompareTag("enemy")) { OnDamage(1, Enemy.transform.position); }
    }

    public override void CheckGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast(footCol.bounds.center, footCol.bounds.size, 0f, Vector2.down, groundCheckDis, groundAndPlatform);

        if (hit.collider != null)
        {
            currentPlatform = hit.collider;
            if (hit.collider == ignorePlatform)
            {
                isGround = false;
            }
            else
            {
                isGround = true;
            }
        }
        else
        {
            isGround = false;
            currentPlatform = null;
        }
    }

    public void TryDropDown()
    {
        if (currentPlatform.gameObject.layer == LayerMask.NameToLayer("platform"))
        {
            Physics2D.IgnoreCollision(bodyCol, currentPlatform, true);
            StartCoroutine(DropDownCo(currentPlatform));
        }
    }

    IEnumerator DropDownCo(Collider2D platform)
    {
        ignorePlatform = platform;
        rb.velocity = new Vector2(rb.velocity.x, -5f);
        Physics2D.IgnoreCollision(footCol, platform, true);
        yield return new WaitForFixedUpdate();
        while(footCol.bounds.Intersects(platform.bounds)) { yield return null; }
        Physics2D.IgnoreCollision(footCol, platform, false);
        ignorePlatform = null;
    }

    public override void OnDamage(int dmg, Vector2 hitPoint)
    {        
        float hitX = hitPoint.x;

        hitDir = transform.position.x < hitX ? -1 : 1;
        machine.ChangeState(state.Hit);
    }

    public override void Flip()
    {
        if (CurrentDir != InputX)
        {
            base.Flip();
        }
    }

    private void OnDrawGizmos()
    {
        if (footCol == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(footCol.bounds.center, footCol.bounds.size);

        Gizmos.color = Color.yellow;
        Vector3 endCenter = (Vector2)footCol.bounds.center + Vector2.down * groundCheckDis;
        Gizmos.DrawLine(footCol.bounds.center, endCenter);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(endCenter, footCol.bounds.size);
    }

    public void AddEnergy(float val)
    {
        currentEnergy = Mathf.Clamp(currentEnergy + val, 0, data.maxEnergy);
    }

    public void UseEnergy(float val)
    {
        currentEnergy = Mathf.Max(0, currentEnergy - val);
    }    

    public void SetLastDashTime(float time)
    {
        lastDashTime = time;
    }

    public void SetUseDashInAir(bool temp)
    {
        useDashInAir = temp;
    }

    public void MovementX()
    {
        float x = InputX;
        rb.velocity = new Vector2(x * data.moveSpeed, rb.velocity.y);
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

