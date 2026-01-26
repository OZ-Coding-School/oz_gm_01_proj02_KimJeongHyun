
public enum BulletAniType
{
    Peashooter,
    PeashooterEX,
    Spreadshooter,
    SpreadshooterEX,
}

public enum BulletEffectAniType
{
    PeashooterShootEffect,
    PeashooterHitEffect,
    PeashooterEXHitEffect,
    SpreadshooterShootEffect,
    SpreadshooterShootHitEffect,
    SpreadshooterEXHitEffect,
}

public enum PlayerEffectAniType
{
    DashDust,
    JumpDust,
    ParrySpark,
    EXStart
}

public enum SlimeEffectAniType
{
    SlimeDust,
    SlimeDust2,
    SlimeLight,
    SlimeStar,
    SlimeStar2,
    SlimeTombIntroDust,
    SlimeTombIntroDust2,
    SlimeTombSmashDust,
    SlimeTombMoveDust,
}

public enum PlayerAnimation
{
    Idle,
    Intro,
    Ghost,
    Death,
    Jump,
    JumpDust,
    ParryNormal,
    ParrySuccess,
    AimDiagonalDown,
    AimDiagonalUp,
    AimDown,
    AimStraight,
    AimUp,
    DashAir,
    DashGround,
    HitAir,
    HitGround,
    DuckEntry,
    DuckIdle,
    DuckExit,
    DuckShot,
    ParrySpark,
    Run,
    RunDiagonalUpShot,
    RunShot,
    ShotDiagonalDown,
    ShotDiagonalUp,
    ShotDown,
    ShotStraight,
    ShotUp,
    SuperBeamFX,
    SuperBeamWater,
    SuperBeam,
    SuperGroundUp,
    SuperAirUp,
    SuperAirDiagonalDown,
    SuperGroundDiagonalDown,
    SuperAirDiagonalUp,
    SuperGroundDiagonalUp,
    SuperAirDown,
    SuperGroundDown,
    SuperAirStraight,
    SuperGroundStraight,
}

public enum SlimeAnimation
{
    Intro,
    Idle,
    Attack,
    Jump,
    ChangePageTwo,
    BigIdle,
    BigJump,
    BigAttack,
    ChangePageThree,
    TombIdle,
    TombMove,
    TombAttack,
    Die,
    SlimeExplode,
}

public enum CusKey
{
    Up,
    Down,
    Left,
    Right,
    Jump,
    Shoot,
    Dash,
    Lock,
    ShotEX,
    Super,
    ChangeWeapon,
}

public enum SceneType
{
    Data,
    Title,
    Lobby,
    Map,
    BossSlime,
}

public enum UIType
{
    None,
    LobbyMainUI,
    GameStartUI,
    OptionUI,
    AudioUI,
    KeySettingUI,
    TutorialUI,
    MapActiveUI,
    MapShopUI,
    MapBossSlimeUI,
    MapWorldUI,
    BossField,
    BossStatusUI,
    BossLose,
    BossWin,
}

public enum SpriteType
{
    Setting,
    Tutorial,
}
public enum ButtonTypeE
{
    None,
    Start,
    Option,
    Exit,
    Back,
    Close,
    Select,
    Resume,
    Retry,
    Visual,
    Sound,
    Done,
    Default,
    keySetting,
    Shop,
    BossSlime,
    BackScene,
}

public enum SliderTypeE
{
    None,
    MasterVol,
    BGMVol,
    SFXVol,
}

public enum BGMType
{
    Title,
    Tutorial,
    MapOne,
    BossSlime,
    GameOver,
    GameWin,

    COUNT
}

public enum SFXType
{
    PlayerDash,    
    PlayerParry,
    PlayerDeath,
    PlayerHit,
    PlayerJump,
    SlimeChangeBig,
    SlimeJump,
    SlimePunch,
    TombAttack,
    TombMove,
    TombSmile,
    BigChangeTomb,
    BigJump,
    BigPunch,
    PeashooterLoop,
    PeashooterEXHit,
    KnockOut,
    NarratorOne,
    NarratorTwo,
    DropTomb,
    KnockOutBell,
    BossStartBtn,
    COUNT
}
