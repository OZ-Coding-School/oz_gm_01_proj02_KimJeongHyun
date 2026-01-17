using System;
using TMPro;
using UnityEngine;
[Serializable]
public struct PlayerCollisions
{
    public BoxCollider2D footCol;
    public BoxCollider2D bodyCol;
    public BoxCollider2D hitboxCol;
    public LayerMask groundAndPlatform;    
}

public class PlayerController : Entity
{
    //@테스트
    public TextMeshProUGUI stateText;


    //@ 인스펙터 할당 데이터
    [SerializeField] private Transform[] firePoint;
    [SerializeField] private PlayerCollisions playerCollisions;
    [SerializeField] private BulletDataSO bulletdata;
    [SerializeField] private PlayerDataSO playerdata;

    public BulletDataSO BulletData => bulletdata;
    public PlayerDataSO PlayerData => playerdata;

    public AnimationHash<PlayerAnimation> AniHash { get; private set; }
    public PlayerStateData PlayerState { get; private set; }
    public PlayerInputHandler PlayerInputHandler { get; private set; }
    public PlayerMovement PlayerMovement { get; private set; }
    public PlayerStatus PlayerStatus { get; private set; }
    public PlayerShooting PlayerShooter { get; private set; }
    public PlayerCollision PlayerCollision { get; private set; }

    protected override void Init()
    {
        base.Init();
        PlayerInputHandler = new PlayerInputHandler();
        PlayerMovement = new PlayerMovement(this, Rb, playerdata);
        PlayerStatus = new PlayerStatus(this, playerdata);
        PlayerCollision = new PlayerCollision(this, Rb, playerCollisions);
        PlayerShooter = new PlayerShooting(this, firePoint, bulletdata);

        AniHash = new AnimationHash<PlayerAnimation>(Anim);
        PlayerState = new PlayerStateData(this, SMachine);
    }
    protected override void Start()
    {
        base.Start();
        SMachine.Init(PlayerState.Idle);
    }

    protected override void Update()
    {
        PlayerInputHandler.InputUpdate();
        if(PlayerCollision.IsGround && Rb.velocity.y < 0.01) PlayerMovement.ResetJumpDash();
        base.Update();
        stateText.text = $"{SMachine.CurState.ToString()}, isground : {PlayerCollision.IsGround.ToString()}, {PlayerCollision.CanParry.ToString()}";
    }

    protected override void FixedUpdate()
    {
        PlayerCollision.CheckGround();
        PlayerCollision.CheckParryOverlap();
        base.FixedUpdate();
    }

    private void OnDrawGizmos()
    {
        if (PlayerCollision != null)
        PlayerCollision.DrawGizmos();
    }
}