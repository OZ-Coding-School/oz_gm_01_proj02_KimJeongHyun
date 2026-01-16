using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    private Dictionary<GameObject, Queue<PoolItem>> pool = new Dictionary<GameObject, Queue<PoolItem>>();

    private Transform poolRoot;

    protected override void Init()
    {
        base.Init();
        poolRoot = new GameObject("PoolRoot").transform;
    }

    public GameObject SpawnObj(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        if (prefab == null) return null;

        if (!pool.ContainsKey(prefab))
        {
            pool.Add(prefab, new Queue<PoolItem>());
        }

        PoolItem item;

        if (pool[prefab].Count == 0)
        {
            GameObject ins = Instantiate(prefab, poolRoot);
            item = ins.AddComponent<PoolItem>();
            item.Setup(prefab, this);
        }
        else
        {
            item = pool[prefab].Dequeue();
        }

        item.transform.SetPositionAndRotation(pos, rot);
        item.gameObject.SetActive(true);

        item.poolalbeComponent?.OnSpawn();

        return item.gameObject;
    }

    public void Despawn(PoolItem item)
    {
        if (item == null) return;

        item.poolalbeComponent?.OnDespawn();
        item.gameObject.SetActive(false);

        pool[item.prefab].Enqueue(item);
    }

    public void Despawn(GameObject obj, float delay)
    {
        if (obj.TryGetComponent(out PoolItem item))
        {
            StartCoroutine(DespawnCo(item, delay));
        }
        else
        {
            Destroy(obj, delay); 
        }
    }

    private IEnumerator DespawnCo(PoolItem item, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (item.gameObject.activeSelf)
        {
            Despawn(item);
        }
    }
}
