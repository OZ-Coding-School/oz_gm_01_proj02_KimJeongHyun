using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBossSlime : MonoBehaviour
{
    public GameObject targetUI;
    public TopDownPlayerController player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            targetUI.SetActive(true);
            player.SetCanMove(false);
        }
    }
}
