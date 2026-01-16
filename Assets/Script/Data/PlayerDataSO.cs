using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    [Header("Movement Stats")]
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float lowJumpForce = 5f;
    [SerializeField] private float lowJumpTime = 0.15f;

    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashTime = 0.5f;
    [SerializeField] private float dashCooldown = 0.5f;

    [SerializeField] private float gravityJump = 2.6f;
    [SerializeField] private float gravityFall = 3.9f;

    [Header("Collision Size")]
    [SerializeField] private Vector2 standSize = new Vector2(1f, 2f);
    [SerializeField] private Vector2 standOffset = new Vector2(0, 1f);
    [SerializeField] private Vector2 duckSize = new Vector2(1f, 1f);
    [SerializeField] private Vector2 duckOffset = new Vector2(0, 0.5f);

    [Header("Health & Hit")]
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private float maxEnergy = 5;
    [SerializeField] private float energyGainPerHit = 0.1f;
    [SerializeField] private float invincibilityTime = 2f;
    [SerializeField] private float knockbackForceX = 1f;
    [SerializeField] private float knockbackForceY = 1f;    

    public float MoveSpeed => moveSpeed;
    public float JumpForce => jumpForce;
    public float LowJumpForce => lowJumpForce;
    public float LowJumpTime => lowJumpTime;
    public float DashSpeed => dashSpeed;
    public float DashTime => dashTime;
    public float DashCooldown => dashCooldown;
    public float GravityJump => gravityJump;
    public float GravityFall => gravityFall;

    public Vector2 StandSize => standSize;
    public Vector2 StandOffset => standOffset;
    public Vector2 DuckSize => duckSize;
    public Vector2 DuckOffset => duckOffset;

    public int MaxHealth => maxHealth;
    public float MaxEnergy => maxEnergy;
    public float EnergyGainPerHit => energyGainPerHit;
    public float InvincibilityTIme => invincibilityTime;
    public float KnockbackForceX => knockbackForceX;
    public float KnockbackForceY => knockbackForceY;

}
