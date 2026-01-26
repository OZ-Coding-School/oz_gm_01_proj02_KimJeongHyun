using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectBtnTextData : MonoBehaviour
{
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI lastPlayTime;
    public TextMeshProUGUI gradeAndtime;
    public TextMeshProUGUI lastClearBoss;

    public void SetSlotText(string name, string lastTime, string gradeAndTime, string lastBoss)
    {
        playerName.text = name;
        lastPlayTime.text = lastTime;
        gradeAndtime.text = gradeAndTime;
        lastClearBoss.text = lastBoss;
    }

    public void SetSlotTextActive(bool bol)
    {
        playerName.gameObject.SetActive(bol);
        gradeAndtime.gameObject.SetActive(bol);
        lastClearBoss.gameObject.SetActive(bol);
        lastPlayTime.gameObject.SetActive(bol);
    }
}
