using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    [Header("Movement Stats")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float dashSpeed = 20f;
    public float dashTime = 0.5f;
    public float dashColldown = 0.5f;

    [Header("Collision Size")]
    public Vector2 standSize = new Vector2(1f, 2f);
    public Vector2 standOffset = new Vector2(0, 1f);
    public Vector2 duckSize = new Vector2(1f, 1f);
    public Vector2 duckOffset = new Vector2(0, 0.5f);

    [Header("Health & Hit")]
    public int maxHealth = 5;
    public float hitStunTime = 0.5f;
    public float invincibilityTime = 2.0f;
    public Vector2 knockbackForce = new Vector2(5f, 5f);
}
