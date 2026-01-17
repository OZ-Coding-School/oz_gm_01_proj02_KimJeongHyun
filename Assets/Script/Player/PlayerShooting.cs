using System.Threading;
using UnityEngine;

public class PlayerShooting
{
    private readonly PlayerController controller;
    private readonly Transform[] firePoint;
    private readonly ObjectPoolManager pool;

    private BulletDataSO bullet;
    private float nextFireTime = 0;

    public float CurrentEnergy { get; private set; } = 0;
    public bool CanSuper => CurrentEnergy >= 1f;
    
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
        GameObject fx = pool.SpawnObj(bullet.BulletEffectPrefab, pos.position, rot);
        if (fx.TryGetComponent(out BulletEffect effect))
        {
            effect.PlayEffect(bullet.ShootEffect);
        }
    }

    public void ShootEX(Vector2 dir)
    {
        nextFireTime = Time.time + bullet.EXFireRate;
        Transform pos = GetFirePoint(dir, false);
        Quaternion rot = ShotRotation(dir);
    }

    public void ShootSuper()
    {
        Vector2 dir = new Vector2(controller.PlayerMovement.CurrentDir, 0);
        Transform position = GetFirePoint(dir, false);  
        //Object.Instantiate Super프리팹추가
    }

    public void AddEnergy() // 공격성공시 조금씩차는 용
    {
        CurrentEnergy = Mathf.Clamp(CurrentEnergy + controller.PlayerData.EnergyGainPerHit, 0, controller.PlayerData.MaxEnergy);
    }

    public void AddEnergy(float val)
    {
        CurrentEnergy = Mathf.Clamp(CurrentEnergy + val, 0 , controller.PlayerData.MaxEnergy);
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
