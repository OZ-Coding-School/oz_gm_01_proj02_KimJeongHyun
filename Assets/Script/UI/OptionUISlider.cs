using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class OptionUISlider : MonoBehaviour, ISelectHandler, IDeselectHandler
{

    [SerializeField] private Slider mySlider;
    [SerializeField] private TextMeshProUGUI titleText;


    [SerializeField] private Color normalColor;
    [SerializeField] private Color selectedColor;

    private void Awake()
    {

        if (mySlider == null) mySlider = GetComponent<Slider>();

        mySlider.minValue = 0.0001f;
        mySlider.maxValue = 10f;
        mySlider.wholeNumbers = true;
        mySlider.value = 10f;
    }

    private void OnEnable()
    {
        titleText.color = normalColor;
    }

    public void OnSelect(BaseEventData eventData)
    {
        titleText.color = selectedColor;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        titleText.color = normalColor;
    }
}