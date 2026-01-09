using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Data/Weapon")]
public class BulletDataSO : ScriptableObject
{
    public GameObject BulletPrefab;
    public float fireRate = 0f;
    public int bulletCount = 1;
    public float speadAngle = 10f;

}
