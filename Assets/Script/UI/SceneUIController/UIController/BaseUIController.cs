using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BaseUIController : MonoBehaviour
{    
    public UIType uiType;
    public event Action<UIType, ButtonTypeE, int> ButtonClicked;
    public event Action<UIType, SliderTypeE, float> SliderValueChanged;

    protected List<ButtonType> UIButton = new List<ButtonType>();

    protected bool hasBtn = true;

    [SerializeField] public Selectable firstSelect;
    protected RectTransform firstSelectRect;
    protected ButtonType firstSelectBtn;
    protected GameObject lastSelectedObj;
    protected RectTransform lastSelectedRect;
    protected ButtonType lastSelectedBtn;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        if (hasBtn)
        {
            SetEvent();
        }
    }

    protected virtual void Start() { }

    protected virtual void OnEnable()
    {
        if (EventSystem.current == null) return;
        StartCoroutine(SetFirstSelectBtn());
    }

    protected virtual void Update()
    {
        if (hasBtn)
        {
            GameObject current = EventSystem.current.currentSelectedGameObject;

            if (current != null && lastSelectedObj != current)
            {
                if (current.transform.IsChildOf(this.transform))
                {
                    if (current.TryGetComponent(out ButtonType btn))
                    {
                        lastSelectedObj = current;
                        lastSelectedBtn = btn;
                        lastSelectedRect = current.GetComponent<RectTransform>();
                    }
                }
            }

            bool isNothing = (current == null);
            bool isNotInteractive = (current != null && current.GetComponent<Selectable>() == null);

            if ((isNothing || isNotInteractive) && lastSelectedObj != null)
            {
                EventSystem.current.SetSelectedGameObject(lastSelectedObj);
            }
        }
    }

    protected virtual void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator SetFirstSelectBtn()
    {

        yield return new WaitForSeconds(0.1f);

        GameObject targetToSelect = null;

        if (lastSelectedObj != null && lastSelectedObj.activeInHierarchy && uiType != UIType.OptionUI)
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

        UIButton.Clear();

        foreach (var btn in buttons)
        {
            if (btn.TryGetComponent(out ButtonType temp))
            {
                UIButton.Add(temp);
                ButtonTypeE btnType = temp.btnType;
                int btnID = temp.ID;

                btn.onClick.AddListener(() => OnButtonClick(uiType, btnType, btnID));
                //오디오추가
            }
        }

        Slider[] sliders = GetComponentsInChildren<Slider>(true);

        foreach (var sld in sliders)
        {
            if (sld.TryGetComponent(out SliderType temp))
            {
                SliderTypeE sldType = temp.sldType;
                sld.onValueChanged.AddListener((val) => OnSliderValueChanged(uiType, sldType, val));
            }
        }
        if (firstSelect != null)
        {
            firstSelectRect = firstSelect.gameObject.GetComponent<RectTransform>();
            firstSelectBtn = firstSelect.gameObject.GetComponent<ButtonType>();
        }
        else
        {
            return;
        }

    }

    protected virtual void OnButtonClick(UIType uiType, ButtonTypeE btnType, int id)
    {
        ButtonClicked?.Invoke(uiType, btnType, id);
    }
    protected virtual void OnSliderValueChanged(UIType uiType, SliderTypeE type, float val)
    {
        float normalize = val;
        SliderValueChanged?.Invoke(uiType, type, val);
    }
}
