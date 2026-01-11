using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    [Header("Movement Stats")]

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _lowJumpForce = 5f;
    [SerializeField] private float _lowJumpTime = 0.15f;
    [SerializeField] private float _dashSpeed = 10f;
    [SerializeField] private float _dashTime = 0.5f;
    [SerializeField] private float _dashCooldown = 0.5f;
    [SerializeField] private float _gravityVal = 2.6f;

    [Header("Collision Size")]
    [SerializeField] private Vector2 _standSize = new Vector2(1f, 2f);
    [SerializeField] private Vector2 _standOffset = new Vector2(0, 1f);
    [SerializeField] private Vector2 _duckSize = new Vector2(1f, 1f);
    [SerializeField] private Vector2 _duckOffset = new Vector2(0, 0.5f);

    [Header("Health & Hit")]
    [SerializeField] private int _maxHealth = 5;
    [SerializeField] private float _maxEnergy = 5;
    [SerializeField] private float _invincibilityTime = 2f;
    [SerializeField] private float _knockbackForceX = 1f;
    [SerializeField] private float _knockbackForceY = 1f;    

    public float moveSpeed => _moveSpeed;
    public float jumpForce => _jumpForce;
    public float lowJumpForce => _lowJumpForce;
    public float lowJumpTime => _lowJumpTime;
    public float dashSpeed => _dashSpeed;
    public float dashTime => _dashTime;
    public float dashCooldown => _dashCooldown;
    public float gravityVal => _gravityVal;

    public Vector2 standSize => _standSize;
    public Vector2 standOffset => _standOffset;
    public Vector2 duckSize => _duckSize;
    public Vector2 duckOffset => _duckOffset;

    public int maxHealth => _maxHealth;
    public float maxEnergy => _maxEnergy;
    public float invincibilityTIme => _invincibilityTime;
    public float knockbackForceX => _knockbackForceX;
    public float knockbackForceY => _knockbackForceY;

}
