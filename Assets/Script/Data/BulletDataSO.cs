using System.Collections;
using System.Collections.Generic;
using effectType;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Data/Weapon")]
public class BulletDataSO : ScriptableObject
{
    [SerializeField] private float damage = 1f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float fireRate = 0.15f;
    [SerializeField] private float bulletCount = 1f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private EffectType shootEffect;
    [SerializeField] private EffectType hitEffect;

    [SerializeField] private float exDamage = 1f;
    [SerializeField] private float exSpeed = 10f;
    [SerializeField] private float eXfireRate = 2f;
    [SerializeField] private float exBulletCount = 1f;
    [SerializeField] private GameObject exBulletPrefab;
    [SerializeField] private EffectType exShootEffect;
    [SerializeField] private EffectType exHitEffect;

    public float Damage => damage;
    public float Speed => speed;
    public float FireRate => fireRate;
    public float BulletCount => bulletCount;
    public GameObject BulletPrefab => bulletPrefab;
    public EffectType ShootEffect => shootEffect;
    public EffectType HitEffect => hitEffect;

    public float ExDamage => exDamage;
    public float ExSpeed => exSpeed;
    public float EXFireRate => eXfireRate;
    public float ExBulletCount => exBulletCount;
    public GameObject ExBulletPrefab => exBulletPrefab;
    public EffectType ExShootEffect => exShootEffect;
    public EffectType ExHItEffect => exHitEffect;
}
