using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class PlayerStatus
{
    private readonly PlayerController controller;
    private readonly PlayerDataSO data;

    public int CurrentHp {  get; private set; }
    public float CurrentEnergy {  get; private set; }

    public bool IsDead => CurrentHp <= 0;
    public bool CanUseEX => CurrentEnergy >= 1f;
    public bool CanUseSuper => CurrentEnergy >= data.MaxEnergy;
    public bool IsInvincible { get; private set; }

    public event Action<int> OnHpChange;
    public event Action<float> OnEnergyChange;
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
        CurrentEnergy = 0;
        IsInvincible = false;
    }

    public void AddEnergy(float val)
    {
        CurrentEnergy = Mathf.Clamp(CurrentEnergy + val, 0, data.MaxEnergy);
        OnEnergyChange?.Invoke(CurrentEnergy);
    }
    public void UseEX(float val)
    {
        CurrentEnergy -= val;
        OnEnergyChange?.Invoke(CurrentEnergy);
    }

    public void UseSuper()
    {
        CurrentEnergy = 0f;
        OnEnergyChange?.Invoke(CurrentEnergy);
    }

    public bool CheckIsDead(float dmg)
    {
        CurrentHp = Mathf.Max(0, CurrentHp - (int)dmg);
        OnHpChange?.Invoke(CurrentHp);
        if (CurrentHp <= 0)
        {
            OnPlayerDie?.Invoke();
            return IsDead;
        }
        return false;
    }

    public void SetInvincible(bool isInvi)
    {
        IsInvincible = isInvi;
    }
}
