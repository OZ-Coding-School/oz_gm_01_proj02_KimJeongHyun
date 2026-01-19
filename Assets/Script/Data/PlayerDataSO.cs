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
    [SerializeField] private float parryJumpForce = 5f;

    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashTime = 0.5f;
    [SerializeField] private float dashCooldown = 0.5f;

    [SerializeField] private float gravityJump = 2.6f;
    [SerializeField] private float gravityFall = 3.9f;

    [Header("Health & Hit")]
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private float maxEnergy = 5;
    [SerializeField] private float energyGainPerHit = 0.1f;
    [SerializeField] private float invincibilityTime = 2f;
    [SerializeField] private float knockbackForceX = 1f;
    [SerializeField] private float knockbackForceY = 1f;

    [SerializeField] private GameObject playerEffect;

    public float MoveSpeed => moveSpeed;
    public float JumpForce => jumpForce;
    public float LowJumpForce => lowJumpForce;
    public float LowJumpTime => lowJumpTime;
    public float ParryJumpForce => parryJumpForce;
    public float DashSpeed => dashSpeed;
    public float DashTime => dashTime;
    public float DashCooldown => dashCooldown;
    public float GravityJump => gravityJump;
    public float GravityFall => gravityFall;

    public int MaxHealth => maxHealth;
    public float MaxEnergy => maxEnergy;
    public float EnergyGainPerHit => energyGainPerHit;
    public float InvincibilityTIme => invincibilityTime;
    public float KnockbackForceX => knockbackForceX;
    public float KnockbackForceY => knockbackForceY;

    public GameObject PlayerEffect => playerEffect;

}
