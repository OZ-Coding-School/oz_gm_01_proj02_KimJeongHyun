using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable
{
    [SerializeField] private BulletDataSO data;
    [SerializeField] private BulletAniType aniType;
    private Rigidbody2D rb;
    private Animator anim;
    private AnimationHash<BulletAniType> aniHash;
    public PoolItem PoolItemPre { get; set; }
    private ObjectPoolManager pool;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pool = ObjectPoolManager.Instance;
        aniHash = new AnimationHash<BulletAniType>(anim);
    }

    public virtual void OnSpawn()
    {
        aniHash.PlayFirstFrame(aniType);
        rb.velocity = transform.right * data.Speed;
        Invoke(nameof(ReturnToPool), data.LifeTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out IDamageable target))
        {
            target.OnDamage((int)data.Damage, transform.position);
            ReturnToPool();
        }
        else if (col.CompareTag("ground"))
        {
            ReturnToPool();
        }
    }

    protected virtual void ReturnToPool()
    {
        CancelInvoke();
        GameObject fx = pool.SpawnObj(data.BulletEffectPrefab, transform.position, Quaternion.identity);
        if (fx.TryGetComponent(out BulletEffect effect))
        {
            effect.PlayEffect(data.HitEffect);
        }
        PoolItemPre.Despawn();
    }

    public void OnDespawn() => rb.velocity = Vector2.zero;
}
