using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapShop : MonoBehaviour
{
    public Transform btnTrs;
    public GameObject targetBtn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            targetBtn.transform.position = btnTrs.position;
            targetBtn.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            targetBtn.SetActive(false);
        }
    }
}
