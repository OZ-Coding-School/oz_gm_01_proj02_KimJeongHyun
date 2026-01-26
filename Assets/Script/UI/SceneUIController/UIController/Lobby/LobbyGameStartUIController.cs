using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class GameStartUIController : BaseUIController
{
    protected override void OnEnable()
    {
        base.OnEnable();
        RefreshSlotText();
    }

    private void RefreshSlotText()
    {
        foreach (var btn in UIButton)
        {
            if (btn.TryGetComponent(out SelectBtnTextData data))
            {
                UpdataSlotView(btn.ID, btn, data);
            }
        }
    }

    private void UpdataSlotView(int id, ButtonType btn, SelectBtnTextData select)
    {
        string filePath = Path.Combine(Application.persistentDataPath, $"save_{id}.json");

        if (File.Exists(filePath))
        {
            try
            {
                string json = File.ReadAllText(filePath);
                GameData gameData = JsonUtility.FromJson<GameData>(json);

                btn.SetTextActive(false);
                select.SetSlotTextActive(true);

                string clearBoss = gameData.lastClearBoss;
                float tempTime = gameData.lastClearTime;
                string tempRank = gameData.lastClearRank;
                int minutes = (int)(tempTime / 60);
                int seconds = (int)(tempTime % 60);

                string timeStr = $"{minutes:D2}분 {seconds:D2}초";

                string gradeTime = $"등급 : {tempRank}  클리어시간 : {timeStr}";
                string playerName = $"플레이어 {(id + 1).ToString()}";
                string lastPlay = $"마지막 접속일 : {gameData.lastSaveTime}";

                if (string.IsNullOrEmpty(clearBoss))
                {
                    clearBoss = "";
                    gradeTime = "";
                }
                select.SetSlotText(playerName, lastPlay, gradeTime, clearBoss);
            }
            catch { }
        }
        else
        {
            btn.SetTextActive(true);
            btn.SetText("NEW");
            select.SetSlotTextActive(false);
        }
    }
}

