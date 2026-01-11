using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomKeyMapping;


public class InputManager : Singleton<InputManager>
{
    private Dictionary<CusKey, KeyCode> keyMapping;

    protected override void Init()
    {
        base.Init();
        InitKey();
    }

    private void InitKey()
    {
        keyMapping = new Dictionary<CusKey, KeyCode>();

        keyMapping[CusKey.Up] = KeyCode.UpArrow;
        keyMapping[CusKey.Down] = KeyCode.DownArrow;
        keyMapping[CusKey.Left] = KeyCode.LeftArrow;
        keyMapping[CusKey.Right] = KeyCode.RightArrow;

        keyMapping[CusKey.Lock] = KeyCode.Z;
        keyMapping[CusKey.Shoot] = KeyCode.X;
        keyMapping[CusKey.Dash] = KeyCode.C;
        keyMapping[CusKey.Jump] = KeyCode.V;
        keyMapping[CusKey.Super] = KeyCode.Space;
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
