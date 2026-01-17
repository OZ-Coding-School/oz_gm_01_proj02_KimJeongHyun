using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Data/Weapon")]
public class BulletDataSO : ScriptableObject
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject exBulletPrefab;
    [SerializeField] private GameObject bulletEffectPrefab;

    [SerializeField] private float damage = 1f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float fireRate = 0.15f;
    [SerializeField] private float bulletCount = 1f;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private BulletEffectAniType shootEffect;
    [SerializeField] private BulletEffectAniType hitEffect;

    [SerializeField] private float exDamage = 1f;
    [SerializeField] private float exSpeed = 10f;
    [SerializeField] private float eXfireRate = 2f;
    [SerializeField] private float exBulletCount = 1f;
    [SerializeField] private BulletEffectAniType exShootEffect;
    [SerializeField] private BulletEffectAniType exHitEffect;

    public GameObject BulletPrefab => bulletPrefab;
    public GameObject ExBulletPrefab => exBulletPrefab;
    public GameObject BulletEffectPrefab => bulletEffectPrefab;

    public float Damage => damage;
    public float Speed => speed;
    public float FireRate => fireRate;
    public float BulletCount => bulletCount;
    public float LifeTime => lifeTime;
    public BulletEffectAniType ShootEffect => shootEffect;
    public BulletEffectAniType HitEffect => hitEffect;

    public float ExDamage => exDamage;
    public float ExSpeed => exSpeed;
    public float EXFireRate => eXfireRate;
    public float ExBulletCount => exBulletCount;
    public BulletEffectAniType ExShootEffect => exShootEffect;
    public BulletEffectAniType ExHItEffect => exHitEffect;
}
