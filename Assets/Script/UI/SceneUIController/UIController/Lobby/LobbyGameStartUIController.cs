using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

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

                string playerName = "Player";
                string lastPlay = gameData.lastSaveTime;
                string world = gameData.worldIndex.ToString();
                string clearBoss = gameData.lastClearBoss;

                select.SetSlotText(playerName, lastPlay, world, clearBoss);
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

