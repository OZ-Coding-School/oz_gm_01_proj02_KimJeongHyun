using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class PoolItem : MonoBehaviour
{
    public GameObject Prefab { get; private set; }
    public IPoolable PoolalbeComponent { get; private set; }
    private ObjectPoolManager pool;    

    public void Setup(GameObject obj, ObjectPoolManager poolManager)
    {
        this.pool = poolManager;
        this.Prefab = obj;
        PoolalbeComponent = GetComponent<IPoolable>();
    }

    public void Despawn()
    {
        if (pool == null)
        {
            pool = ObjectPoolManager.Instance;
        }
         pool.Despawn(this);
    }
}
