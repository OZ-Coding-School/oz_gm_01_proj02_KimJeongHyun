using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
    PoolItem PoolItemPre { get; set; }
    public void OnSpawn();
    public void OnDespawn();
}
