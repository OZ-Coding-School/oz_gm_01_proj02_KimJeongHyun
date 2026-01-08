using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class BaseController: MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public Collider2D col {  get; private set; }
    public SpriteRenderer sr { get; private set; }
    public Animator animator { get; private set; }
    public StateMachine machine {  get; private set; }

    public int curDir { get; private set; } = 1;

    [SerializeField] protected Transform groundCheckTrs;
    [SerializeField] protected Transform wallCheckTrs;
    [SerializeField] protected float groundCheckRaius = 0.2f;
    [SerializeField] protected float wallCheckRaius = 0.2f;
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask wall;

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
        machine.CurState?.StateUpdate();
    }

    protected virtual void FixedUpdate()
    {
        machine.CurState?.StateFixedUpdate();
    }

    protected virtual void Flip(int x)
    {
        if (x != 0 && x != curDir)
        {
            curDir *= -1;
            transform.Rotate(0, 180, 0);
        }
    }

    public bool IsGround => Physics2D.OverlapCircle(groundCheckTrs.position, groundCheckRaius, ground);
    public bool IsWall => Physics2D.OverlapCircle(wallCheckTrs.position, wallCheckRaius, wall);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheckTrs.position, groundCheckRaius);
        Gizmos.DrawWireSphere(wallCheckTrs.position, wallCheckRaius);
    }





    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {

    }

}
