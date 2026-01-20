using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectBtnTextData : MonoBehaviour
{
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI lastPlayTime;
    public TextMeshProUGUI worldMap;
    public TextMeshProUGUI lastClearBoss;

    public void SetSlotText(string name, string lastTime, string mapName,string lastBoss)
    {
        playerName.text = name;
        lastPlayTime.text = lastTime;
        worldMap.text = mapName;
        lastClearBoss.text = lastBoss;
    }

    public void SetSlotTextActive(bool bol)
    {
        playerName.gameObject.SetActive(bol);
        worldMap.gameObject.SetActive(bol);
        lastClearBoss.gameObject.SetActive(bol);
        lastPlayTime.gameObject.SetActive(bol);
    }
}
