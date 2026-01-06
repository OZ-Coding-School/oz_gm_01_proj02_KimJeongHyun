using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSceneUIController : MonoBehaviour
{
    public event Action temp;

    private void Awake()
    {
        Init();
    }

    protected virtual void Start() { }
    protected virtual void Update() { }
    protected virtual void Init() { }

}
