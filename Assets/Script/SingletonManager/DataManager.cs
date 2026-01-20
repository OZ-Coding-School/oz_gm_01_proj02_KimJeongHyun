using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{

    public List<IUserData> UserDataList { get; private set; } = new List<IUserData>();

    public bool ExistsSaveData { get; private set; }
    public int CurrentSlotIndex { get; private set; } = 0;

    protected override void Init()
    {
        base.Init();

        UserDataList.Add(new UserSettingData());
        UserDataList.Add(new UserGameData());
    }

    public void SetDefaultUserData()
    {
        for (int i = 0; i < UserDataList.Count; i++)
        {
            UserDataList[i].SetDefaultData();
        }
    }

    public void LoadUserData()
    {
        ExistsSaveData = PlayerPrefs.GetInt("ExistsSaveData") == 1 ? true : false;

        if (ExistsSaveData)
        {
            for (int i = 0; i < UserDataList.Count; i++)
            {
                UserDataList[i].LoadData();
            }
        }
    }

    public void SaveUserData()
    {
        bool hasError = false;

        for (int i = 0; i < UserDataList.Count; i++)
        {
            bool isSuccess = UserDataList[i].SaveData();
            if (!isSuccess)
            {
                hasError = true;
            }
        }

        if (!hasError)
        {
            ExistsSaveData = true;
            PlayerPrefs.SetInt("ExistsSaveData", 1);
            PlayerPrefs.Save();
        }
    }

    public void SelectSlot(int index)
    {
        var gameData = GetUserData<UserGameData>();
        
        if (gameData != null)
        {
            gameData.CurrentSlotIndex = index;

            bool isSuccess = gameData.LoadData();

            if (!isSuccess)
            {
                gameData.SetDefaultData();
            }
        }
    }

    public T GetUserData<T>() where T : class, IUserData
    {
        return UserDataList.OfType<T>().FirstOrDefault();
    }
}

