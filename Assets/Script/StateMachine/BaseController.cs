
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class BaseController: MonoBehaviour, IDamageable
{
    //@컴포넌트
    public Rigidbody2D rb { get; private set; }
    public Collider2D col {  get; private set; }
    public SpriteRenderer sr { get; private set; }
    public Animator animator { get; private set; }
    public StateMachine machine {  get; private set; }   

    //@인게임 데이터
    [SerializeField] protected Transform groundCheckTrs;
    [SerializeField] protected Transform wallCheckTrs;
    [SerializeField] protected Vector2 boxSize;    
    [SerializeField] protected LayerMask ground;
    [SerializeField] protected LayerMask platform;
    [SerializeField] protected LayerMask groundAndPlatform;    
    public bool isInvincible { get; protected set; } = false;
    public int currentHP { get; protected set; }
    public int CurrentDir { get; protected set; }
    public bool isGround { get; protected set; }
    public int hitDir { get; protected set; }

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        machine = new StateMachine();
        
    }

    protected virtual void Start() { }

    protected virtual void Update()
    {
        machine.CurState.HandleInput();
        machine.CurState.StateUpdate();      
    }

    protected virtual void FixedUpdate()
    {
        CheckGround();
        machine.CurState.StateFixedUpdate();
    }    

    public virtual void CheckGround()
    {
        if (groundCheckTrs == null) return;
        isGround = Physics2D.OverlapBox(groundCheckTrs.position, boxSize, 0, groundAndPlatform);
    }

    public virtual void Flip()
    {
        CurrentDir *= -1;
        transform.Rotate(0, 180, 0);
    }

    public virtual void OnDamage(int dmg, Vector2 hitPoint)
    {
        if (isInvincible) { return; }
        currentHP -= dmg;
        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
            return;
        }
    }
    public virtual void Die() { }

    protected virtual void OnDrawGizmos() { if (groundCheckTrs != null) { Gizmos.DrawWireCube(groundCheckTrs.position, boxSize); } }

    protected virtual void OnTriggerEnter2D(Collider2D collision) { }        
    protected virtual void OnCollisionEnter2D(Collision2D collision) { }
}
