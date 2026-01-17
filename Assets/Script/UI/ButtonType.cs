using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonType : MonoBehaviour
{
    public int ID = 0;
    public ButtonTypeE btnType;
    public CusKey btnKey;
    public TextMeshProUGUI text;

    public void RefreshText()
    {
        if (btnType == ButtonTypeE.keySetting) text.text = InputManager.Instance.GetKeyCode(btnKey).ToString();
    }

    public void SetText(string str)
    {
        text.text = str;
    }
}
