using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class PlayerStatus
{
    private readonly PlayerController controller;
    private readonly PlayerDataSO data;

    public int CurrentHp { get; private set; }
    public float CurrentEnergy {  get; private set; }
    public int ParryCount { get; private set; }

    public bool IsDead => CurrentHp <= 0;
    public bool CanUseEX => CurrentEnergy >= 1f;
    public bool CanUseSuper => CurrentEnergy >= data.MaxEnergy;
    public bool IsInvincible { get; private set; }

    public event Action OnPlayerDie;
    

    public PlayerStatus(PlayerController controller, PlayerDataSO data)
    {
        this.controller = controller;
        this.data = data;
        Init();
    }

    private void Init()
    {
        CurrentHp = data.MaxHealth;
        ParryCount = 0;
        CurrentEnergy = 0;
        IsInvincible = false;
    }

    public void AddEnergy(float val)
    {
        CurrentEnergy = Mathf.Clamp(CurrentEnergy + val, 0, data.MaxEnergy);
    }
    public void UseEXEnergy()
    {
        CurrentEnergy -= 1f;
    }

    public void UseSuper()
    {
        CurrentEnergy = 0f;
    }

    public bool CheckIsDead(float dmg)
    {
        CurrentHp = Mathf.Max(0, CurrentHp - (int)dmg);
        if (CurrentHp <= 0)
        {
            OnPlayerDie?.Invoke();
            return IsDead;
        }
        return false;
    }
    public void AddParryCount()
    {
        ParryCount++;
    }


    public void SetInvincible(bool isInvi)
    {
        IsInvincible = isInvi;
    }
}
