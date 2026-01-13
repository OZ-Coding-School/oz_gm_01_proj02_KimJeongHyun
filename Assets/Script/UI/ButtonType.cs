using CustomKeyMapping;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonType : MonoBehaviour
{
    public int ID = 0;
    public EnumData.ButtonType btnType;
    public CusKey btnKey;
    public TextMeshProUGUI text;

    public void RefreshText()
    {
        if (btnType == EnumData.ButtonType.keySetting) text.text = InputManager.Instance.GetKeyCode(btnKey).ToString();
    }

    public void SetText(string str)
    {
        text.text = str;
    }
}
