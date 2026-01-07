using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public bool isReady { get; protected set; } = false;
    protected bool isDistroyOnLoad = false;

    protected static T instance;

    public static T Instance {get { return instance; }}

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        if(instance == null)
        {
            instance = (T)this;

            if(!isDistroyOnLoad)
            {
                DontDestroyOnLoad(this);
            }            
        }
        else
        {
            Destroy(this);
        }
    }

    protected virtual void OnDestroy()
    {
        Dispose();
    }

    protected virtual void Dispose()
    {
        instance = null;
    }
}
