using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using gunEffect;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] BulletDataSO bullet;
    private float nextFireTime = 0;
    
    public void Shoot(Transform firepoint, Vector2 dir)
    {
        if (Time.time < nextFireTime) return;
        nextFireTime = Time.time + bullet.fireRate;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion spawnRotation = Quaternion.Euler(0, 0, angle);

        GameObject peaEffect = Instantiate(bullet.PeashooterStart, firepoint.position, spawnRotation);
        float randomScale = Random.Range(0.8f, 1.2f);
        peaEffect.transform.localScale *= randomScale;


        peaEffect.transform.Rotate(0, 0, Random.Range(-30f, 30f));

        if (Random.value > 0.5f)
        {
            Vector3 s = peaEffect.transform.localScale;
            s.y *= -1;
            peaEffect.transform.localScale = s;
        }

        Destroy(peaEffect, 0.1f);
        Instantiate(bullet.PeashooterBullet, firepoint.position, spawnRotation);
    }
}
