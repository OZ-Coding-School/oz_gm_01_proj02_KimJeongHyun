using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct BossBattleResult
{
    public string bossId;
    public float clearTime;
    public int parryCount;
    public int remainingHp;
    public string grade;
}

public class GameManager : Singleton<GameManager>
{ 
    public BossBattleResult LastBossResult;    
    protected override void Init()
    {
        base.Init();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    public void SaveMapPosition()
    {
        DataManager.Instance.SaveUserData();
    }

    public void ProcessBossClear(string bossName, float time, int parry, int hp)
    {
        string grade = CalculateGrade(time, parry, hp);

        LastBossResult = new BossBattleResult
        {
            bossId = bossName,
            clearTime = time,
            parryCount = parry,
            remainingHp = hp,
            grade = grade
        };

        var userData = DataManager.Instance.GetUserData<UserGameData>();
        if (userData != null)
        {
            GameData data = userData.Data;

            data.lastClearBoss = bossName;
            data.lastClearTime = time;
            data.lastClearRank = grade;

            LevelRecord record = data.levelProgressList.Find(x => x.levelID == bossName);
            if (record == null)
            {
                record = new LevelRecord { levelID = bossName, isCleared = true, bestTime = time, rank = grade };
                data.levelProgressList.Add(record);
            }
            else
            {
                if (time < record.bestTime || record.bestTime == 0)
                {
                    record.bestTime = time;
                    record.rank = grade;
                }
            }


            DataManager.Instance.SaveUserData();
        }
    }

    public void ProcessTutorialClear()
    {
        var userData = DataManager.Instance.GetUserData<UserGameData>();
        if (userData != null)
        {
            GameData data = userData.Data;
            data.isTutorialClear = true;
        }
        DataManager.Instance.SaveUserData();
    }

    private string CalculateGrade(float time, int parry, int hp)
    {
        int score = 0;
        score += parry;
        score += hp;    

        if (time < 110f) score += 2;
        else score += 1;

        return score switch
        {
            >= 10 => "S",
            9 => "A",
            8 => "B",
            _ => "C"
        };
    }
}