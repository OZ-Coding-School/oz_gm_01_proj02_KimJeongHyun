using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class LobbyKeySettingUIController : BaseUIController
{
    [SerializeField] private RectTransform viewContent;
    [SerializeField] private RectTransform visualAction;
    [SerializeField] private float scrollSpeed = 10f;

    private List<ButtonType> keySettingBtn = new List<ButtonType>();

    private float targetY;
    private bool isRebinding = false;

    protected override void Init()
    {
        base.Init();
        targetY = visualAction.anchoredPosition.y;

        ButtonType[] bts = GetComponentsInChildren<ButtonType>(true);
        foreach (var val in bts)
        {
            keySettingBtn.Add(val);
            val.RefreshText();
        }            
    }
    protected override void OnEnable()
    {
        lastSelectedObj = null;
        lastSelectedBtn = null;
        lastSelectedRect = null;

        visualAction.anchoredPosition = new Vector2(visualAction.anchoredPosition.x, 73f);
        targetY = 73f;

        base.OnEnable();
    }
    protected override void Update()
    {
        if (isRebinding)
        {
            EventSystem.current.SetSelectedGameObject(lastSelectedObj);
            return;
        }
        base.Update();
        if (lastSelectedRect != null && lastSelectedRect.IsChildOf(visualAction)) HandleAutoScroll(lastSelectedRect);
 
        float newY = Mathf.Lerp(visualAction.anchoredPosition.y, targetY, Time.deltaTime * scrollSpeed);
        visualAction.anchoredPosition = new Vector2(visualAction.anchoredPosition.x, newY);
    }

    protected override void OnDisable()
    {
        if (isRebinding) lastSelectedBtn.RefreshText();
        isRebinding = false;
        base.OnDisable();
    }


    protected override void OnButtonClick(UIType uiType, ButtonTypeE btnType)
    {
        if (isRebinding || lastSelectedBtn == null) return;        

        if (btnType == ButtonTypeE.Default)
        {
            InputManager.Instance.SetDefault();
            RefreshAll();
        }
        else if (btnType == ButtonTypeE.keySetting)
        {
            StartCoroutine(RebindKey(lastSelectedBtn));
        }

        base.OnButtonClick(uiType, btnType);
    }

    private IEnumerator EnableScrollAfterFrame()
    {
        yield return null; 
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator RebindKey(ButtonType btn)
    {
        isRebinding = true;
        btn.SetText("키를 입력하세요...");
        yield return null;

        while (isRebinding)
        {
            if (Input.anyKeyDown)
            {
                foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(key))
                    {
                        if (key != KeyCode.Escape)
                        {
                            InputManager.Instance.SetKey(btn.btnKey, key);
                        }
                        isRebinding = false;
                        break;
                    }
                }
            }
            yield return null;
        }
        yield return null;
        btn.RefreshText();
        isRebinding = false;        
    }


    private void RefreshAll()
    {
        foreach (var btn in keySettingBtn)
        {
            btn.RefreshText();
        }
    }

    private void HandleAutoScroll(RectTransform rtrs)
    {
        Vector3 localPos = viewContent.InverseTransformPoint(rtrs.position);
        float top = localPos.y + (rtrs.rect.height / 2);
        float bottom = localPos.y - (rtrs.rect.height / 2);
        float half = viewContent.rect.height / 2;

        if (top > half)
        {
            float offset = top - half;
            targetY = visualAction.anchoredPosition.y - (offset + 20f);
        }
        else if (bottom < -half)
        {
            float offset = -half - bottom;
            targetY = visualAction.anchoredPosition.y + (offset + 20f);
        }
    }
}
