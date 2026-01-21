using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapActiveBtn : MonoBehaviour
{
    private MapActiveUIController ctr;

    private void Awake()
    {
        ctr = GetComponentInParent<MapActiveUIController>();
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(this.gameObject);
    }
}
