using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolItem : MonoBehaviour
{
    public GameObject prefab { get; private set; }
    public IPoolable poolalbeComponent { get; private set; }
    private ObjectPoolManager poolManager;

    public void Setup(GameObject obj, ObjectPoolManager poolManager)
    {
        this.poolManager = poolManager;
        this.prefab = obj;
        poolalbeComponent = GetComponent<IPoolable>();
    }

    public void Despawn()
    {
        poolManager.Despawn(this);
    }
}
