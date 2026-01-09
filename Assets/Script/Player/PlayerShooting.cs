using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private BulletDataSO curBullet;
    private float fireTimer;

    public void SetBullet(BulletDataSO bullet)
    {
        curBullet = bullet;
        fireTimer = curBullet.fireRate;
    }
    
    public void Shoot(Transform firepoint, Vector2 dir)
    {

    }
}
