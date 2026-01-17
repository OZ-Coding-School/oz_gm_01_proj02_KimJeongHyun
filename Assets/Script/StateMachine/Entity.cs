using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Entity: MonoBehaviour
{
    public Rigidbody2D Rb { get; private set; }
    public SpriteRenderer Sr { get; private set; }
    public Animator Anim { get; private set; }
    public StateMachine SMachine {  get; private set; }

    private void Awake()
    {
        Init();
    }
    protected virtual void Init()
    {
        Rb = GetComponent<Rigidbody2D>();
        Sr = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
        SMachine = new StateMachine();
        
    }

    protected virtual void Start() { }

    protected virtual void Update()
    {
        SMachine.CurState.HandleInput();
        SMachine.CurState.StateUpdate();      
    }

    protected virtual void FixedUpdate()
    {
        SMachine.CurState.StateFixedUpdate();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision) { }        
    protected virtual void OnCollisionEnter2D(Collision2D collision) { }
}
