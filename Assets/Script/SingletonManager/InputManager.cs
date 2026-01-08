using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using playerAction;

[System.Serializable]
public class KeyMapping
{
    public PlayerAction action;
    public KeyCode keyCode;
}

public class InputManager : Singleton<InputManager>
{

    private Dictionary<PlayerAction, KeyCode> keyMappingDic = new Dictionary<PlayerAction, KeyCode>();
    [SerializeField] private List<KeyMapping> defaultKeySet;

    public event Action OnKeyChange;

    protected override void Init()
    {
        base.Init();

        foreach (var key in defaultKeySet)
        {
            if (!keyMappingDic.ContainsKey(key.action))
            {
                keyMappingDic.Add(key.action, key.keyCode);
            }
        }
    }

    public bool GetAction(PlayerAction action)
    {
        if (keyMappingDic.TryGetValue(action, out KeyCode key)) return Input.GetKey(key);
        return false;
    }
    public bool GetActionDown(PlayerAction action)
    {
        if (keyMappingDic.TryGetValue(action,out KeyCode key)) return Input.GetKeyDown(key);
        return false;
    }
    public bool GetActionUp(PlayerAction action)
    {
        if (keyMappingDic.TryGetValue (action,out KeyCode key)) return Input.GetKeyUp(key);
        return false;
    }
    public float GetMovementDir()
    {
        float x = 0f;

        if (GetAction(PlayerAction.Right)) x += 1f;
        if (GetAction(PlayerAction.Left)) x -= 1f;

        return x;
    }
    /*public Vector2 GetAimDir()
    {
        float x = 0f;
        float y = 0f;

        if (GetAction(PlayerAction.Right)) x += 1f;
        if (GetAction(PlayerAction.Left)) x -= 1f;
        if (GetAction(PlayerAction.Up)) y += 1f;
        if (GetAction(PlayerAction.Down)) y -= 1f;

    }
    */
}
