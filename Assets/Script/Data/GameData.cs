using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelRecord
{
    public string levelID;      
    public bool isCleared;      
    public string rank;         
    public float bestTime;
}

[System.Serializable]
public class GameData
{
    public string lastSaveTime;
    public string lastClearBoss;
    public bool isTutorialClear;

    public int coinCount;
    public float mapPosX;       
    public float mapPosY;      
    public int worldIndex;     

    public List<string> unlockedItems;

    public string equippedShotA; 
    public string equippedShotB;
    public string equippedSuper;
    public string equippedCharm;

    public List<LevelRecord> levelProgressList;

    public GameData()
    {
        coinCount = 0;
        mapPosX = 0;
        mapPosY = 0;
        worldIndex = 0;
        lastSaveTime = "";
        lastClearBoss = "";
        isTutorialClear = false;

        unlockedItems = new List<string>() { "Peashooter" };
        equippedShotA = "Peashooter";
        equippedShotB = "";
        equippedSuper = "";
        equippedCharm = "";

        levelProgressList = new List<LevelRecord>();
    }
}