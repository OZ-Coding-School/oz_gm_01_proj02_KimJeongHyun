using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Slider;

public class BaseSceneUIController : MonoBehaviour
{
    public SceneType sceneType;
    private Dictionary<UIType, BaseUIController> uiList = new Dictionary<UIType, BaseUIController>();
    private Stack<UIType> uiStack = new Stack<UIType>();
    private BaseUIController[] baseCnt;
    private const float SLIDER_MAX_AUDIO_VAL = 10f;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        baseCnt = GetComponentsInChildren<BaseUIController>(true);

        foreach (var cnt in baseCnt)
        {
            uiList[cnt.uiType] = cnt;
            cnt.gameObject.SetActive(false);
        }

    }

    protected virtual void OnEnable()
    {
        foreach (var cnts in baseCnt)
        {
            cnts.ButtonClicked += ButtonEvent;
            cnts.SliderValueChanged += SliderEvent;
        }
    }
    protected virtual void OnDisable()
    {
        foreach (var cnts in baseCnt)
        {
            cnts.ButtonClicked -= ButtonEvent;
            cnts.SliderValueChanged -= SliderEvent;
        }
    }

    protected virtual void Start() { }
    protected virtual void Update() { }



    protected virtual void SliderEvent(UIType uiType, SliderTypeE sldType, float val)
    {
        if(uiType == UIType.AudioUI)
        {
            float result = val / SLIDER_MAX_AUDIO_VAL;
            AudioManager.Instance.SetVolume(sldType, result);
        }
    }

    protected virtual void ButtonEvent(UIType uiType, ButtonTypeE btnType, int btnID) { }



    protected virtual void OpenUI(UIType type)
    {
        uiList[type].gameObject.SetActive(true);
    }

    protected virtual void CloseUI(UIType type)
    {
        uiList[type].gameObject.SetActive(false);
    }

    protected virtual void OpenUIStack(UIType type)
    {
        if (uiStack.Count > 0) uiList[uiStack.Peek()].gameObject.SetActive(false);
        uiStack.Push(type);
        uiList[type].gameObject.SetActive(true);
    }

    protected virtual void CloseUIStack()
    {
        if (uiStack.Count <= 1) return;

        UIType curType = uiStack.Peek();

        if (curType == UIType.TutorialUI) return;

        uiList[uiStack.Pop()].gameObject.SetActive(false);
        uiList[uiStack.Peek()].gameObject.SetActive(true);
    }
}
