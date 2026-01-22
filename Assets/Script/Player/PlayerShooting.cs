using System.Threading;
using UnityEngine;

public class PlayerShooting
{
    private readonly PlayerController controller;
    private readonly Transform[] firePoint;
    private readonly ObjectPoolManager pool;

    private BulletDataSO bullet;
    private float nextFireTime = 0;
    private float nextEXFireTime = 0;
    public bool UseEXTime => Time.time >= nextEXFireTime;


    public PlayerShooting(PlayerController controller, Transform[] firePoint,
        BulletDataSO bullet)
    {
        this.controller = controller;
        this.firePoint = firePoint;
        this.bullet = bullet;
        pool = ObjectPoolManager.Instance;
    }



    public void Shoot(Vector2 dir, bool isDucking = false)
    {
        if (Time.time < nextFireTime) return;
        nextFireTime = Time.time + bullet.FireRate;
        Transform pos = GetFirePoint(dir, isDucking);
        Quaternion rot = ShotRotation(dir);        
        pool.SpawnObj(bullet.BulletPrefab, pos.position, rot);
        var fx = pool.SpawnObj<BulletEffect>(bullet.BulletEffectPrefab, pos.position, rot);
        EffectHelper.SetRandomEffect(fx);
        fx.PlayEffect(BulletEffectAniType.PeashooterShootEffect);
    }

    public void ShootEX(Vector2 dir)
    {
        nextEXFireTime = Time.time + bullet.EXFireRate;
        Transform pos = GetFirePoint(dir, false);
        Quaternion rot = ShotRotation(dir);
        pool.SpawnObj(bullet.ExBulletPrefab, pos.position, rot);
    }

    public void ShootSuper()
    {
        Vector2 dir = new Vector2(controller.PlayerMovement.CurrentDir, 0);
        Transform position = GetFirePoint(dir, false);  
        //Object.Instantiate Super프리팹추가
    }

    private Transform GetFirePoint(Vector2 dir, bool isDucking)
    {
        if (isDucking) return firePoint[5];

        bool isInputX = Mathf.Abs(dir.x) > 0.01f;
        int index = 0;

        if (dir.y > 0) index = isInputX ? 2 : 4;
        else if (dir.y < 0) index = isInputX ? 1 : 3;
        else index = 0;
        return firePoint[index];
    }
    private Quaternion ShotRotation(Vector2 dir)
    {
        if (dir == Vector2.zero) dir = new Vector2(controller.PlayerMovement.CurrentDir, 0);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, angle);
    }
}
