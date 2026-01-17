using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler
{
    private InputManager input => InputManager.Instance;

    public float InputX { get; private set; }
    public float InputY { get; private set; }
    public bool InputJump { get; private set; }
    public bool InputJumpUp { get; private set; }
    public bool InputDash { get; private set; }
    public bool InputLock { get; private set; }
    public bool InputShoot { get; private set; }
    public bool InputDuck { get; private set; }
    public bool InputSuper { get; private set; }
    public Vector2 InputDir { get; private set; }

    public void InputUpdate()
    {
        InputX = input.GetHorizontal();
        InputY = input.GetVertical();
        InputJump = input.GetKeyDown(CusKey.Jump);
        InputJumpUp = input.GetKeyUp(CusKey.Jump);
        InputDash = input.GetKeyDown(CusKey.Dash);
        InputLock = input.GetKey(CusKey.Lock);
        InputShoot = input.GetKey(CusKey.Shoot);
        InputDuck = input.GetKey(CusKey.Down);
        InputSuper = input.GetKeyDown(CusKey.Super);
        InputDir = new Vector2(InputX, InputY);
    }
}
