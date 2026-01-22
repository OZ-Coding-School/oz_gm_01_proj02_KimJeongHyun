using System;
using UnityEngine;

public class BaseSceneUIController : MonoBehaviour
{
    //public event Action temp;
    public SceneType sceneType;
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
