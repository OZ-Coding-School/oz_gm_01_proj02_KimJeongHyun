using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseUIController : MonoBehaviour
{
    public EnumData.UIType uiType;
    public event Action<EnumData.UIType, EnumData.ButtonType> ButtonClicked;
    public event Action<EnumData.UIType, EnumData.SliderType, float> SliderValueChanged;

    [SerializeField] protected Selectable firstSelect;
    private GameObject lastSelectedObj;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        SetEvent();
    }

    protected virtual void OnEnable()
    {
        StartCoroutine(SetFirstSelectBtn());
    }

    private void Update()
    {

        GameObject current = EventSystem.current.currentSelectedGameObject;

        if (current != null)
        {
            if (current.GetComponent<Selectable>() != null && current.transform.IsChildOf(this.transform))
            {
                lastSelectedObj = current;
            }
        }

        bool isNothing = (current == null);
        bool isNotInteractive = (current != null && current.GetComponent<Selectable>() == null);

        if ((isNothing || isNotInteractive) && lastSelectedObj != null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelectedObj);
        }
    }
    protected virtual void OnDisable()
    {

    }

    private IEnumerator SetFirstSelectBtn()
    {

        yield return null;

        GameObject targetToSelect = null;

        if (lastSelectedObj != null && lastSelectedObj.activeInHierarchy)
        {
            targetToSelect = lastSelectedObj;
        }

        else if (firstSelect != null)
        {
            targetToSelect = firstSelect.gameObject;
        }


        if (targetToSelect != null)
        {
            EventSystem.current.SetSelectedGameObject(targetToSelect);

            lastSelectedObj = targetToSelect;
        }
    }

    private void SetEvent()
    {

        Button[] buttons = GetComponentsInChildren<Button>(true);

        foreach (var btn in buttons)
        {
            if (btn.TryGetComponent(out ButtonType temp))
            {
                EnumData.ButtonType btnType = temp.btnType;
                btn.onClick.AddListener(() => OnButtonClick(uiType, btnType));
                //오디오추가
            }
        }

        Slider[] sliders = GetComponentsInChildren<Slider>(true);

        foreach (var sld in sliders)
        {
            if (sld.TryGetComponent(out SliderType temp))
            {
                EnumData.SliderType sldType = temp.sldType;
                sld.onValueChanged.AddListener((val) => OnSliderValueChanged(uiType, sldType, val));
            }
        }
    }

    protected virtual void OnButtonClick(EnumData.UIType uiType,EnumData.ButtonType btnType)
    {
        ButtonClicked?.Invoke(uiType, btnType);
    }
    protected virtual void OnSliderValueChanged(EnumData.UIType uiType, EnumData.SliderType btnType, float val)
    {
        float normalize = val / 10f;
        SliderValueChanged?.Invoke(uiType, btnType, val);
    }
}
