using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapShopBtn : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public TextMeshProUGUI text;

    private void Awake()
    {
        text.gameObject.SetActive(false);
    }

    public void OnSelect(BaseEventData eventData)
    {
        text.gameObject.SetActive(true);
    }
    public void OnDeselect(BaseEventData eventData)
    {
        text.gameObject.SetActive(false);
    }
}
