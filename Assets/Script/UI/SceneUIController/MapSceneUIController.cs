using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using UnityEngine;
using static UnityEngine.UI.Slider;

public class MapSceneUIController : BaseSceneUIController
{
    private Dictionary<UIType, BaseUIController> uiList = new Dictionary<UIType, BaseUIController>();
    private Stack<UIType> uiStack = new Stack<UIType>();
    private BaseUIController[] baseCnt;
    public TopDownPlayerController player;

    protected override void Init()
    {
        base.Init();
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
        }
    }

    protected override void OnDisable()
    {

        foreach (var cnts in baseCnt)
        {
            cnts.ButtonClicked -= ButtonEvent;
        }
    }

    protected override void Start()
    {        
        OpenUI(UIType.MapWorldUI);
        OpenUIStack(UIType.MapActiveUI);
        player.SetCanMove(true);
        AudioManager.Instance.PlayBGM(BGMType.MapOne);
    }

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) CloseUIStack();

    }

    private void ButtonEvent(UIType uiType, ButtonTypeE btnType, int btnID)
    {
        switch (uiType, btnType)
        {
            case (UIType.MapActiveUI, ButtonTypeE.BossSlime): OpenUIStack(UIType.MapBossSlimeUI); break;
            case (UIType.MapActiveUI, ButtonTypeE.Shop): OpenUIStack(UIType.MapShopUI); break;
            case (UIType.MapBossSlimeUI, ButtonTypeE.Start): SceneLoader.Instance.LoadScene(SceneType.BossSlime); break;
        }
    }

    private void OpenUI(UIType type)
    {
        uiList[type].gameObject.SetActive(true);
    }

    private void CloseUI(UIType type)
    {
        uiList[type].gameObject.SetActive(false);
    }

    private void OpenUIStack(UIType type)
    {
        if (uiStack.Count > 0) uiList[uiStack.Peek()].gameObject.SetActive(false);
        uiStack.Push(type);
        uiList[type].gameObject.SetActive(true);
        player.SetCanMove(false);
    }

    private void CloseUIStack()
    {
        if (uiStack.Count <= 1) return;

        UIType curType = uiStack.Peek();

        if (curType == UIType.TutorialUI) return;

        uiList[uiStack.Pop()].gameObject.SetActive(false);
        uiList[uiStack.Peek()].gameObject.SetActive(true);
        player.SetCanMove(true);
    }

}
