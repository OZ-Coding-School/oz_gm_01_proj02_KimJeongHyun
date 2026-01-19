using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialExitDoor : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI exitText;
    private bool playerIn;
    private void Update()
    {
        if (InputManager.Instance.GetKeyDown(CusKey.Shoot) && playerIn)
        {
            SceneLoader.Instance.LoadScene(SceneType.Map);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIn = true;
            exitText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIn = false;
            exitText.gameObject.SetActive(false);
        }   
    }
}
