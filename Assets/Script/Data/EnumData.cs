namespace playerAnimation
{
    public enum PlayerAnimation
    {
        Idle,
        Intro,
        Ghost,
        Death,
        Jump,
        JumpDust,
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
}
namespace gunEffect
{
    public enum GunEffect
    {
        PeashooterStart,
        PeashooterBullet,
        PeashooterDestroy,
    }
}
namespace CustomKeyMapping
{
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
        Super,
    }
}
public class EnumData
{
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
        VisualUI,
    }
    public enum ButtonType
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
    }

    public enum SliderType
    {
        None,
        MasterVol,
        BGMVol,
        SFXVol,        
    }

    public enum BGM
    {
        Title,
        Tutorial,
        MapOne,
        BossSlime,
        COUNT
    }

    public enum SFX
    {
        COUNT
    }
}
