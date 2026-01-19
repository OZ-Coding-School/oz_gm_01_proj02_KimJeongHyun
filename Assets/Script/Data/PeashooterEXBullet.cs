using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeashooterEXBullet : Bullet
{
    [SerializeField] private float damageInterval = 0.12f; 

    private Dictionary<IDamageable, float> lastHitTimes = new Dictionary<IDamageable, float>();

    public override void OnSpawn()
    {
        base.OnSpawn();
        lastHitTimes.Clear();
    }

    protected override void OnTriggerEnter2D(Collider2D col) { }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (((1 << col.gameObject.layer) & targetLayer) != 0)
        {
            if (col.attachedRigidbody != null && col.attachedRigidbody.TryGetComponent(out IDamageable target))
            {
                if (!lastHitTimes.ContainsKey(target) || Time.time - lastHitTimes[target] >= damageInterval)
                {
                    Vector2 hitPoint = col.ClosestPoint(transform.position);

                    PlayHitEffects(hitPoint);
                    target.OnDamage((int)data.ExDamage, hitPoint);

                    lastHitTimes[target] = Time.time;
                }
            }
        }
    }

    private void PlayHitEffects(Vector2 hitPoint)
    {
        var fx = pool.SpawnObj<BulletEffect>(data.BulletEffectPrefab, hitPoint, Quaternion.identity);
        if (fx != null)
        {
            EffectHelper.SetRandomEffect(fx);
            fx.PlayEffect(hitType);
        }
        AudioManager.Instance.PlaySFX(SFXType.PeashooterEXHit);
    }
}
