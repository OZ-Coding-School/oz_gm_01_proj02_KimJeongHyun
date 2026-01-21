using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SlimeController : BaseBossController, IDamageable
{
    public TextMeshProUGUI stateText;
    public SlimeStateData SlimeState { get; private set; }
    public AnimationHash<SlimeAnimation> AniHash { get; private set; }
    public GameObject tombfall;
    public CameraShake cam;


    public int page = 1;
    public float page2per = 0.7f;
    public float page3per = 0.4f;
    public int jumpCount = 0;
    public int moveCount = 0;
    public float maxHp = 600f;
    public float curHp;    

    public float[] jumpPower = { 8f, 4f, 12f };
    public float tombSpeed = 5f;
    public bool isChange = false;
    public bool IsDead { get; private set; } = false;

    public event Action OnSlimeDie;

    protected override void Init()
    {
        base.Init();
        AniHash = new AnimationHash<SlimeAnimation>(Anim);
        SlimeState = new SlimeStateData(this, SMachine);
        curHp = maxHp;
    }

    protected override void Start()
    {
        SMachine.Init(SlimeState.Intro);
    }

    protected override void Update()
    {
        stateText.text = $"{SMachine.CurState.ToString()}, HP : {curHp.ToString()}";        
        base.Update();

    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        CheckWallFlip();

    }

    public void OnDamage(float dmg, Vector2 hitDir)
    {
        bool temp = CheckIsDie(dmg);
        SMachine.CurState.OnHit(temp, hitDir);
    }

    public bool CheckIsDie(float dmg)
    {
        curHp = Mathf.Max(0, curHp - (int)dmg);
        if (curHp <= 0)
        {
            OnSlimeDie?.Invoke();   
            return IsDead;
        }
        return false;
    }

    public void CheckWallFlip()
    {
        RaycastHit2D hit = Physics2D.Raycast(wallCheckTrs.position, Vector2.right * curDir, wallCheckRadius, wallLayer);
        if (hit.collider != null)
        {
            curDir *= -1;
            transform.Rotate(0, 180, 0);
            float temp = Mathf.Abs(Rb.velocity.x);
            if (temp < 0.1f) temp = 5f;
            Rb.velocity = new Vector2(curDir * temp, Rb.velocity.y);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(wallCheckTrs.position, Vector2.right * curDir * wallCheckRadius);
    }
}
