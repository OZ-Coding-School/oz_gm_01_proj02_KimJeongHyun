using System.Threading;
using UnityEngine;


public class PlayerShooting
{
    private readonly PlayerController controller;
    private readonly Transform[] firePoint;

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
    }



    public void Shoot(Vector2 dir, bool isDucking = false)
    {
        if (Time.time < nextFireTime) return;
        nextFireTime = Time.time + bullet.FireRate;

        Transform position = GetFirePoint(dir, isDucking);
        Quaternion rotation = ShotRotation(dir);
    }

    public void ShootEX(Vector2 dir)
    {
        nextFireTime = Time.time + bullet.EXFireRate;
        Transform position = GetFirePoint(dir, false);
        Quaternion rotation = ShotRotation(dir);
        //Object.Instantiate EX프리팹추가
    }

    public void ShootSuper()
    {
        Vector2 dir = new Vector2(controller.PlayerMovement.CurrentDir, 0);
        Transform position = GetFirePoint(dir, false);  
        //Object.Instantiate Super프리팹추가
    }

    public void AddEnergy()
    {
        CurrentEnergy = Mathf.Clamp(CurrentEnergy + controller.PlayerData.EnergyGainPerHit, 0, controller.PlayerData.MaxEnergy);
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
