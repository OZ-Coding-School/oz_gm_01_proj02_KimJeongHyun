using UnityEngine;
using System.IO;

public class UserGameData : IUserData
{
    public GameData Data { get; private set; } = new GameData();
    public int CurrentSlotIndex { get; set; } = 0;

    private string GetPath()
    {
        return Path.Combine(Application.persistentDataPath, $"save_{CurrentSlotIndex}.json");
    }

    public void SetDefaultData()
    {
        Data = new GameData();
    }

    public bool LoadData()
    {
        string path = GetPath();
        if (!File.Exists(path))
        {
            return false;
        }

        try
        {
            string json = File.ReadAllText(path);

            Data = JsonUtility.FromJson<GameData>(json);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool SaveData()
    {
        try
        {
            Data.lastSaveTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm");

            var player = Object.FindObjectOfType<TopDownPlayerController>();
            if (player != null)
            {
                Data.mapPosX = player.transform.position.x;
                Data.mapPosY = player.transform.position.y;
            }

            string json = JsonUtility.ToJson(Data, true);

            File.WriteAllText(GetPath(), json);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
