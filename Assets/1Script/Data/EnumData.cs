using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Master,
        SFX,
        BGM
    }

    public enum BGM
    {
        COUNT
    }

    public enum SFX
    {
        COUNT
    }
}
