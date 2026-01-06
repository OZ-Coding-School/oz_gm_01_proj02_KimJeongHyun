using System;
using UnityEngine;
using UnityEngine.UI;

public class BaseUIController : MonoBehaviour
{
    public EnumData.UIType uiType;
    public event Action<EnumData.UIType, EnumData.ButtonType> ButtonClicked;
    public event Action<EnumData.UIType, EnumData.SliderType, float> SliderValueChanged;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        SetEvent();
    }

    protected virtual void OnEnable() { }
    protected virtual void OnDisable() { }

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
        SliderValueChanged?.Invoke(uiType, btnType, val);
    }
}
