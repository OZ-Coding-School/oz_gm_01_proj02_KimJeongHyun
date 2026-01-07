using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseSceneUIController : MonoBehaviour
{
    public event Action temp;
    
    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    protected virtual void OnEnable() { }
    protected virtual void OnDisable() { }
    protected virtual void Start() { }
    protected virtual void Update() { }
}
