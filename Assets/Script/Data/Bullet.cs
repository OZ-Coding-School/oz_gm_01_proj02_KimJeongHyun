using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable
{
    [SerializeField] protected BulletDataSO data;
    [SerializeField] protected BulletAniType aniType;
    [SerializeField] protected BulletEffectAniType hitType;
    [SerializeField] protected LayerMask targetLayer;

    protected Rigidbody2D rb;
    protected Animator anim;
    protected AnimationHash<BulletAniType> aniHash;
    protected ObjectPoolManager pool;

    public PoolItem PoolItemPre { get; set; }

    protected virtual void Awake()
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
        StartCoroutine(LifeTIme());
    }

    protected IEnumerator LifeTIme()
    {
        yield return new WaitForSeconds(data.LifeTime);
        ReturnToPool(false);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (((1 << col.gameObject.layer) & targetLayer) != 0)
        {
            Vector2 hitpoint = col.ClosestPoint(transform.position);
            if (col.attachedRigidbody != null && col.attachedRigidbody.TryGetComponent(out IDamageable target))
            {
                target.OnDamage(data.Damage, hitpoint);
            }
            ReturnToPool(true, hitpoint);
        }
    }

    protected virtual void ReturnToPool(bool playEffect, Vector2 pos = default)
    {
        if (playEffect)
        {
            var fx = pool.SpawnObj<BulletEffect>(data.BulletEffectPrefab, pos, Quaternion.identity);
            fx.PlayEffect(hitType);
        }
        PoolItemPre.Despawn();
    }

    public void OnDespawn()
    {
        StopAllCoroutines();
        rb.velocity = Vector2.zero;
    }
}
