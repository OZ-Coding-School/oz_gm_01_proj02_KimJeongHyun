using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    private Dictionary<CusKey, KeyCode> keyMapping;
    private Dictionary<CusKey, KeyCode> defaultKey;

    protected override void Init()
    {
        base.Init();
        InitKey();
    }

    private void InitKey()
    {
        keyMapping = new Dictionary<CusKey, KeyCode>();
        defaultKey = new Dictionary<CusKey, KeyCode>();

        void AddKey(CusKey key, KeyCode code)
        {
            keyMapping[key] = code;
            defaultKey[key] = code;
        }

        AddKey(CusKey.Up, KeyCode.UpArrow);
        AddKey(CusKey.Down, KeyCode.DownArrow);
        AddKey(CusKey.Left, KeyCode.LeftArrow);
        AddKey(CusKey.Right, KeyCode.RightArrow);
        AddKey(CusKey.Lock, KeyCode.Z);
        AddKey(CusKey.Shoot, KeyCode.X);
        AddKey(CusKey.Dash, KeyCode.C);
        AddKey(CusKey.Jump, KeyCode.V);
        AddKey(CusKey.Super, KeyCode.Space);
        AddKey(CusKey.ChangeWeapon, KeyCode.Tab);
    }

    public void SetKey(CusKey key, KeyCode code)
    {
        keyMapping[key] = code;
    }

    public void SetDefault()
    {
        foreach (var reset in defaultKey)
        {
            keyMapping[reset.Key] = reset.Value;
        }
    }

    public KeyCode GetKeyCode(CusKey key)
    {
        return keyMapping[key];
    }

    public bool GetKeyDown(CusKey key)
    {
        return Input.GetKeyDown(keyMapping[key]);
    }

    public bool GetKey(CusKey key)
    {
        return Input.GetKey(keyMapping[key]);
    }

    public bool GetKeyUp(CusKey key)
    {
        return Input.GetKeyUp(keyMapping[key]);
    }

    public float GetHorizontal()
    {
        float val = 0f;
        if (GetKey(CusKey.Left)) val -= 1f;
        if (GetKey(CusKey.Right)) val += 1f;
        return val;
    }

    public float GetVertical()
    {
        float val = 0f;
        if (GetKey(CusKey.Up)) val += 1f;
        if (GetKey(CusKey.Down)) val -= 1f;
        return val;
    }
}
