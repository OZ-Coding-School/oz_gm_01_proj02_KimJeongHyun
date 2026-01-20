using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSettingData : IUserData
{
    public bool Sound {  get; private set; }

    public bool LoadData()
    {
        bool result = false;

        try
        {
            Sound = PlayerPrefs.GetInt("Sound") == 1 ? true : false;
            result = true;
        }
        catch { }
        return result;
    }

    public bool SaveData()
    {
        bool result = false;

        try
        {
            PlayerPrefs.SetInt("Sound", Sound ? 1 : 0);
            PlayerPrefs.Save();
            result = true;
        }
        catch { }

        return result;
    }

    public void SetDefaultData()
    {
        Sound = true;
    }
}
