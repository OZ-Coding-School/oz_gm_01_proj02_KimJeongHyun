using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbySceneUIController : BaseSceneUIController
{
    private Dictionary<EnumData.UIType, BaseUIController> uiList = new Dictionary<EnumData.UIType, BaseUIController>();
    private Stack<EnumData.UIType> uiStack = new Stack<EnumData.UIType>();
    private BaseUIController[] baseCnt;

    private void Awake()
    {
        baseCnt = GetComponentsInChildren<BaseUIController>(true);

        foreach (var cnt in baseCnt)
        {
            uiList[cnt.uiType] = cnt;
            cnt.gameObject.SetActive(false);
        }
    }

    protected override void OnEnable()
    {
        
        foreach (var cnts in baseCnt)
        {
            cnts.ButtonClicked += ButtonEvent;
            cnts.SliderValueChanged += SliderEvent;
        }
    }

    protected override void OnDisable()
    {
        
        foreach (var cnts in baseCnt)
        {
            cnts.ButtonClicked -= ButtonEvent;
            cnts.SliderValueChanged -= SliderEvent;
        }
    }

    protected override void Start()
    {
        base.Start();
        OpenUI(EnumData.UIType.LobbyMainUI);
    }   

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) CloseUI();
    }
    
    private void ButtonEvent(EnumData.UIType uiType ,EnumData.ButtonType btnType)
    {
        switch (uiType, btnType)
        {
            case (EnumData.UIType.LobbyMainUI, EnumData.ButtonType.Start): OpenUI(EnumData.UIType.GameStartUI); break;
            case (EnumData.UIType.LobbyMainUI, EnumData.ButtonType.Option): OpenUI(EnumData.UIType.OptionUI); break;
            case (EnumData.UIType.OptionUI, EnumData.ButtonType.Sound): OpenUI(EnumData.UIType.AudioUI); break;
            case (EnumData.UIType.OptionUI, EnumData.ButtonType.Visual): OpenUI(EnumData.UIType.VisualUI); break;

            case (_, EnumData.ButtonType.Back): CloseUI(); break;
            case (_, EnumData.ButtonType.Exit): Application.Quit(); break;
                
        }
        //gamestartui의 버튼들 눌렀을때 게임시작 ( 데이터정보를 json에서 비교해서 map씬에서 해당 진행도에서 시작되어야함 (진행도에따른 스폰지역 설정)
    }

    private void SliderEvent(EnumData.UIType uiType ,EnumData.SliderType sldType, float val)
    {

    }

    private void OpenUI(EnumData.UIType type)
    {
        if (uiStack.Count > 0) uiList[uiStack.Peek()].gameObject.SetActive(false);
        uiStack.Push(type);
        uiList[type].gameObject.SetActive(true);
    }

    private void CloseUI()
    {
        if (uiStack.Count <= 1) return;
        {
            uiList[uiStack.Pop()].gameObject.SetActive(false);
            uiList[uiStack.Peek()].gameObject.SetActive(true);
        }
    }
}
