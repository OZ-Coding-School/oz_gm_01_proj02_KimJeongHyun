namespace effectType
{
    public enum EffectType
    {
        // 1000번 부터 호출시 반전 X
        // Flip O==============================================

        //Player
        PlayerDashDust,



        //Bullet





        // Flip X===============================================

        //Player
        PlayerJumpDust = 1000,



        //Bullet
        PeashooterShootEffect = 2000,
        PeashooterHitEffect,
    }
}




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
        ChangeWeapon,
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
        KeySettingUI,
        TutorialUI
    }

    public enum SpriteType
    {
        Setting,
        Tutorial,
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
        Done,
        Default,
        keySetting,
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
