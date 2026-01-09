using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Data/Weapon")]
public class BulletDataSO : ScriptableObject
{
    public float fireRate = 0.01f;
    public int bulletCount = 1;
    public float speadAngle = 10f;

    public GameObject PeashooterStart;
    public GameObject PeashooterBullet;
    public GameObject PeashooterDestroy;
}
