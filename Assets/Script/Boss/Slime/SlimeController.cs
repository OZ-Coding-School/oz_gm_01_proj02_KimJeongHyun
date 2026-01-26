using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SlimeController : BaseBossController, IDamageable
{
    public TextMeshProUGUI stateText;
    public SlimeStateData SlimeState { get; private set; }
    public GameObject slimeEffectPre;
    public AnimationHash<SlimeAnimation> AniHash { get; private set; }
    public GameObject tombfall;
    public GameObject tombIntroDust;
    public GameObject slimeJumpDust;
    public CameraShake cam;
    
    public float page2per = 0.7f;
    public float page3per = 0.4f;
    public int jumpCount = 0;
    public int jumpConutCheck = 0;
    public int moveCount = 0;

    public float[] jumpPower = { 8f, 4f, 12f };
    public float tombSpeed = 25f;
    public bool isChange = false;
    public bool IsDead { get; private set; } = false;

    public event Action OnSlimeDie;

    protected override void Init()
    {
        base.Init();
        AniHash = new AnimationHash<SlimeAnimation>(Anim);
        SlimeState = new SlimeStateData(this, SMachine);
        maxHp = 1200;
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
            IsDead = true;
            return IsDead;
        }
        return IsDead;
    }

    public void SlimeDie()
    {
        OnSlimeDie?.Invoke();
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
