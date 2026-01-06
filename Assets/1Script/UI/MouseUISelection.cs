using UnityEngine;
using UnityEngine.EventSystems;

public class MouseUISelection : MonoBehaviour, IPointerMoveHandler
{
    public void OnPointerMove(PointerEventData eventData)
    {
        if (eventData.delta.sqrMagnitude > 0.1f)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
    }
}
