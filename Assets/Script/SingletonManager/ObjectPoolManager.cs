using System.Collections;
using System.Collections.Generic;
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
            if (!ins.TryGetComponent(out item))
            {
                item = ins.AddComponent<PoolItem>();
            }
            item.Setup(prefab, this);

            if (item.PoolalbeComponent != null)
            {
                item.PoolalbeComponent.PoolItemPre = item;
            }
        }
        else
        {
            item = pool[prefab].Dequeue();
        }



        if (TryGetComponent(out Rigidbody2D rb))
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        item.transform.localScale = Vector3.one;
        item.transform.localRotation = Quaternion.identity;


        item.transform.SetPositionAndRotation(pos, rot);
        item.gameObject.SetActive(true);

        item.PoolalbeComponent?.OnSpawn();

        return item.gameObject;
    }

    public T SpawnObj<T>(GameObject prefab, Vector3 pos, Quaternion rot) where T : Component
    {
        GameObject go = SpawnObj(prefab, pos, rot);

        if (go == null) return null;

        if (go.TryGetComponent(out T component))
        {
            return component;
        }
        return null;
    }


    public void Despawn(PoolItem item)
    {
        if (item == null) return;

        item.PoolalbeComponent?.OnDespawn();
        item.gameObject.SetActive(false);

        pool[item.Prefab].Enqueue(item);
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

    public void ClearAllPool()
    {
        pool.Clear();
    }
}
