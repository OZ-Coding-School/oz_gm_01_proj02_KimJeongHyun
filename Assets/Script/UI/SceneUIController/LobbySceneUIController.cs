using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbySceneUIController : BaseSceneUIController
{
    private Dictionary<UIType, BaseUIController> uiList = new Dictionary<UIType, BaseUIController>();
    private Stack<UIType> uiStack = new Stack<UIType>();
    private BaseUIController[] baseCnt;
    private const float SLIDER_MAX_AUDIO_VAL = 10f;
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
        OpenUI(UIType.LobbyMainUI);
    }   

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) CloseUI();
    }
    
    private void ButtonEvent(UIType uiType, ButtonTypeE btnType)
    {
        switch (uiType, btnType)
        {
            case (UIType.LobbyMainUI, ButtonTypeE.Start): OpenUI(UIType.GameStartUI); break;
            case (UIType.LobbyMainUI, ButtonTypeE.Option): OpenUI(UIType.OptionUI); break;
            case (UIType.OptionUI, ButtonTypeE.Sound): OpenUI(UIType.AudioUI); break;
            case (UIType.OptionUI, ButtonTypeE.keySetting): OpenUI(UIType.KeySettingUI); break;
            case (UIType.GameStartUI, ButtonTypeE.Start): OpenUI(UIType.TutorialUI); break;


            case (_, ButtonTypeE.Back): CloseUI(); break;
            case (_, ButtonTypeE.Exit): Application.Quit(); break;
                
        }
        //gamestartui의 버튼들 눌렀을때 게임시작 ( 데이터정보를 json에서 비교해서 map씬에서 해당 진행도에서 시작되어야함 (진행도에따른 스폰지역 설정)
    }

    private void SliderEvent(UIType uiType ,SliderTypeE sldType, float val)
    {
        float result = val / SLIDER_MAX_AUDIO_VAL;
        AudioManager.Instance.SetVolume(sldType, result);
    }

    private void OpenUI(UIType type)
    {
        if (uiStack.Count > 0) uiList[uiStack.Peek()].gameObject.SetActive(false);
        uiStack.Push(type);
        uiList[type].gameObject.SetActive(true);
    }

    private void CloseUI()
    {
        if (uiStack.Count <= 1) return;

        UIType curType = uiStack.Peek();

        if (curType == UIType.TutorialUI) return;

        uiList[uiStack.Pop()].gameObject.SetActive(false);
        uiList[uiStack.Peek()].gameObject.SetActive(true);

    }
}
