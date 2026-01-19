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
    public bool InputShotEX { get; private set; }
    public Vector2 InputDir { get; private set; }
    public bool ParryInputBuffer => parryBufferTimer > 0;
    private float parryBufferTimer;
    private const float MAX_BUFFER_TIME = 0.15f;

    public void InputUpdate()
    {
        if (parryBufferTimer > 0) parryBufferTimer -= Time.unscaledDeltaTime;
        InputX = input.GetHorizontal();
        InputY = input.GetVertical();
        InputJump = input.GetKeyDown(CusKey.Jump);
        if (InputJump) parryBufferTimer = MAX_BUFFER_TIME;
        InputJumpUp = input.GetKeyUp(CusKey.Jump);
        InputDash = input.GetKeyDown(CusKey.Dash);
        InputLock = input.GetKey(CusKey.Lock);
        InputShoot = input.GetKey(CusKey.Shoot);
        InputDuck = input.GetKey(CusKey.Down);
        InputShotEX = input.GetKeyDown(CusKey.ShotEX);
        InputSuper = input.GetKeyDown(CusKey.Super);
        InputDir = new Vector2(InputX, InputY);
    }

    public void UseParryBuffer() => parryBufferTimer = 0;
}
