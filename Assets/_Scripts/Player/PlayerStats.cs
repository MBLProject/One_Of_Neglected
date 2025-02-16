using System;
using UnityEngine;
using static Enums;
public class PlayerStats
{
    #region Field               
    private int Level;      
    private int MaxExp;
    private float Exp;
    private int MaxHp;
    private float Hp;   
    private float HpRegen;
    private int Defense;   
    private float Mspd;    
    private float ATK;     
    private float Aspd;    
    private float CriRate;
    private float CriDamage;    
    private int ProjAmount;  
    private float ATKRange;    
    private float Duration;
    private float Cooldown;
    private int Revival; 
    private float Magnet;  
    private float Growth;  
    private float Greed;   
    private float Curse;   
    private int Reroll;  
    private int Banish;
    private bool GodKill;
    private bool Barrier;
    private float BarrierCooldown;
    private bool Invincibility;
    private int DashCount;
    private bool Adversary;
    private bool ProjDestroy;
    private bool projParry;

    #endregion

    #region Event
    // 특정 스텟이 변할때 이벤트를 호출시켜야 한다! 이럴때 아래 이벤트 쓰십쇼
    public event Action<float> OnHpChanged;
    public event Action<int> OnLevelUp;
    public event Action<float> OnExpChanged;
    public event Action<int> OnMaxExpChanged;
    public event Action<int> OnMaxHpChanged;
    public event Action<float> OnHpRegenChanged;
    public event Action<int> OnDefenseChanged;
    public event Action<float> OnMspdChanged;
    public event Action<float> OnATKChanged;
    public event Action<float> OnAspdChanged;
    public event Action<float> OnCriRateChanged;
    public event Action<float> OnCriDamageChanged;
    public event Action<int> OnProjAmountChanged;
    public event Action<float> OnATKRangeChanged;
    public event Action<float> OnDurationChanged;
    public event Action<float> OnCooldownChanged;
    public event Action<int> OnRevivalChanged;
    public event Action<float> OnMagnetChanged;
    public event Action<float> OnGrowthChanged;
    public event Action<float> OnGreedChanged;
    public event Action<float> OnCurseChanged;
    public event Action<int> OnRerollChanged;
    public event Action<int> OnBanishChanged;
    public event Action<bool> OnGodKillChanged;
    public event Action<bool> OnBarrierChanged;
    public event Action<float> OnBarrierCooldownChanged;
    public event Action<bool> OnInvincibilityChanged;
    public event Action<int> OnDashCountChanged;
    public event Action<bool> OnAdversaryChanged;
    public event Action<bool> OnProjDestroyChanged;
    public event Action<bool> OnProjParryChanged;
    #endregion

    #region Properties
    public int CurrentLevel 
    { 
        get => Level;
        set
        {
            if (Level != value)  // 값이 변경될 때만 이벤트 발생
            {
                Level = value;
                OnLevelUp?.Invoke(Level);
            }
        }
    }
    public int CurrentMaxExp
    {
        get => MaxExp;
        set
        {
            MaxExp = value;
            OnMaxExpChanged?.Invoke(MaxExp);
        }
    }
    public float currentExp
    {
        get => Exp;
        set
        {
            Exp = value;
            OnExpChanged?.Invoke(Exp);
        }
    }
    public int CurrentMaxHp
    {
        get => MaxHp;
        set
        {
            MaxHp = value;
            OnMaxHpChanged?.Invoke(MaxHp);
        }
    }
    public float currentHp
    {
        get => Hp;
        set
        {
            Hp = (int)Mathf.Clamp(value, 0, MaxHp);
            OnHpChanged?.Invoke(Hp);
        }
    }
    public float CurrentHpRegen
    {
        get => HpRegen;
        set
        {
            HpRegen = value;
            OnHpRegenChanged?.Invoke(HpRegen);
        }
    }
    public int CurrentDefense
    {
        get => Defense;
        set
        {
            Defense = value;
            OnDefenseChanged?.Invoke(Defense);
        }
    }
    public float CurrentMspd
    {
        get => Mspd;
        set
        {
            Mspd = value;
            OnMspdChanged?.Invoke(Mspd);
        }
    }
    public float CurrentATK
    {
        get => ATK;
        set
        {
            ATK = value;
            OnATKChanged?.Invoke(ATK);
        }
    }
    public float CurrentAspd
    {
        get => Aspd;
        set
        {
            Aspd = value;
            OnAspdChanged?.Invoke(Aspd);
        }
    }
    public float CurrentCriRate
    {
        get => CriRate;
        set
        {
            CriRate = value;
            OnCriRateChanged?.Invoke(CriRate);
        }
    }
    public float CurrentCriDamage
    {
        get => CriDamage;
        set
        {
            CriDamage = value;
            OnCriDamageChanged?.Invoke(CriDamage);
        }
    }
    public int CurrentProjAmount
    {
        get => ProjAmount;
        set
        {
            ProjAmount = value;
            OnProjAmountChanged?.Invoke(ProjAmount);
        }
    }
    public float CurrentATKRange
    {
        get => ATKRange;
        set
        {
            ATKRange = value;
            OnATKRangeChanged?.Invoke(ATKRange);
        }
    }
    public float CurrentDuration
    {
        get => Duration;
        set
        {
            Duration = value;
            OnDurationChanged?.Invoke(Duration);
        }
    }
    public float CurrentCooldown
    {
        get => Cooldown;
        set
        {
            Cooldown = value;
            OnCooldownChanged?.Invoke(Cooldown);
        }
    }
    public int CurrentRevival
    {
        get => Revival;
        set
        {
            Revival = value;
            OnRevivalChanged?.Invoke(Revival);
        }
    }
    public float CurrentMagnet
    {
        get => Magnet;
        set
        {
            Magnet = value;
            OnMagnetChanged?.Invoke(Magnet);
        }
    }
    public float CurrentGrowth
    {
        get => Growth;
        set
        {
            Growth = value;
            OnGrowthChanged?.Invoke(Growth);
        }
    }
    public float CurrentGreed
    {
        get => Greed;
        set
        {
            Greed = value;
            OnGreedChanged?.Invoke(Greed);
        }
    }
    public float CurrentCurse
    {
        get => Curse;
        set
        {
            Curse = value;
            OnCurseChanged?.Invoke(Curse);
        }
    }
    public int CurrentReroll
    {
        get => Reroll;
        set
        {
            Reroll = value;
            OnRerollChanged?.Invoke(Reroll);
        }
    }
    public int CurrentBanish
    {
        get => Banish;
        set
        {
            Banish = value;
            OnBanishChanged?.Invoke(Banish);
        }
    }
    public bool CurrentGodKill
    {
        get => GodKill;
        set
        {
            GodKill = value;
            OnGodKillChanged?.Invoke(GodKill);
        }
    }
    public bool CurrentBarrier
    {
        get => Barrier;
        set
        {
            Barrier = value;
            OnBarrierChanged?.Invoke(Barrier);
        }
    }
    public float CurrentBarrierCooldown
    {
        get => BarrierCooldown;
        set
        {
            BarrierCooldown = value;
            OnBarrierCooldownChanged?.Invoke(BarrierCooldown);
        }
    }
    public bool CurrentInvincibility
    {
        get => Invincibility;
        set
        {
            Invincibility = value;
            OnInvincibilityChanged?.Invoke(Invincibility);
        }
    }
    public int CurrentDashCount
    {
        get => DashCount;
        set
        {
            DashCount = value;
            OnDashCountChanged?.Invoke(DashCount);
        }
    }
    public bool CurrentAdversary
    {
        get => Adversary;
        set
        {
            Adversary = value;
            OnAdversaryChanged?.Invoke(Adversary);
        }
    }
    public bool CurrentProjDestroy
    {
        get => ProjDestroy;
        set
        {
            ProjDestroy = value;
            OnProjDestroyChanged?.Invoke(ProjDestroy);
        }
    }
    public bool CurrentProjParry
    {
        get => projParry;
        set
        {
            projParry = value;
            OnProjParryChanged?.Invoke(projParry);
        }
    }
    #endregion

    #region Stat
    public void ModifyStatValue(StatType statType, float value)
    {
        float finalValue = value;

        switch (statType)
        {
            case StatType.Level:
                CurrentLevel += (int)finalValue;
                break;
            case StatType.Exp:
                currentExp += (int)finalValue;
                break;
            case StatType.MaxHp:
                CurrentMaxHp += (int)finalValue;
                break;
            case StatType.Hp:
                currentHp += finalValue;
                break;
            case StatType.HpRegen:
                CurrentHpRegen += finalValue;
                break;
            case StatType.Defense:
                CurrentDefense += (int)finalValue;
                break;
            case StatType.Mspd:
                CurrentMspd += finalValue;
                break;
            case StatType.Aspd:
                CurrentAspd += finalValue;
                break;
            case StatType.ATK:
                CurrentATK += finalValue;
                break;
            case StatType.CriRate:
                CurrentCriRate += finalValue;
                break;
            case StatType.CriDamage:
                CurrentCriDamage += finalValue;
                break;
            case StatType.ProjAmount:
                CurrentProjAmount += (int)finalValue;
                break;
            case StatType.ATKRange:
                CurrentATKRange += finalValue;
                break;
            case StatType.Duration:
                CurrentDuration += finalValue;
                break;
            case StatType.Cooldown:
                CurrentCooldown += finalValue;
                break;
            case StatType.Revival:
                CurrentRevival += (int)finalValue;
                break;
            case StatType.Magnet:
                CurrentMagnet += finalValue;
                break;
            case StatType.Growth:
                CurrentGrowth += finalValue;
                break;
            case StatType.Greed:
                CurrentGreed += finalValue;
                break;
            case StatType.Curse:
                CurrentCurse += finalValue;
                break;
            case StatType.Reroll:
                CurrentGreed += (int)finalValue;
                break;
            case StatType.Banish:
                CurrentBanish += (int)finalValue;
                break;
            case StatType.GodKill:
                CurrentGodKill = Convert.ToBoolean(finalValue);
                break;
            case StatType.Barrier:
                CurrentBarrier = Convert.ToBoolean(finalValue);
                break;
            case StatType.BarrierCooldown:
                CurrentBarrierCooldown += finalValue;
                break;
            case StatType.Invincibility:
                CurrentInvincibility = Convert.ToBoolean(finalValue);
                break;
            case StatType.DashCount:
                CurrentDashCount += (int)finalValue;
                break;
            case StatType.Adversary:
                CurrentAdversary = Convert.ToBoolean(finalValue);
                break;
            case StatType.ProjDestroy:
                CurrentProjDestroy = Convert.ToBoolean(finalValue);
                break;
            case StatType.ProjParry:
                CurrentProjParry = Convert.ToBoolean(finalValue);
                break;
        }
    }

    public void SetStatValue(StatType statType, float value)
    {
        switch (statType)
        {
            case StatType.Level:
                CurrentLevel = (int)value;
                break;
            case StatType.Exp:
                currentExp = (int)value;
                break;
            case StatType.MaxHp:
                CurrentMaxHp = (int)value;
                break;
            case StatType.Hp:
                currentHp = value;
                break;
            case StatType.HpRegen:
                CurrentHpRegen = value;
                break;
            case StatType.Defense:
                CurrentDefense = (int)value;
                break;
            case StatType.Mspd:
                CurrentMspd = value;
                break;
            case StatType.Aspd:
                CurrentAspd = value;
                break;
            case StatType.ATK:
                CurrentATK = value;
                break;
            case StatType.CriRate:
                CurrentCriRate = value;
                break;
            case StatType.CriDamage:
                CurrentCriDamage = value;
                break;
            case StatType.ProjAmount:
                CurrentProjAmount = (int)value;
                break;
            case StatType.ATKRange:
                CurrentATKRange = value;
                break;
            case StatType.Duration:
                CurrentDuration = value;
                break;
            case StatType.Cooldown:
                CurrentCooldown = value;
                break;
            case StatType.Revival:
                CurrentRevival = (int)value;
                break;
            case StatType.Magnet:
                CurrentMagnet = value;
                break;
            case StatType.Growth:
                CurrentGrowth = value;
                break;
            case StatType.Greed:
                CurrentGreed = value;
                break;
            case StatType.Curse:
                CurrentCurse = value;
                break;
            case StatType.Reroll:
                CurrentGreed = (int)value;
                break;
            case StatType.Banish:
                CurrentBanish = (int)value;
                break;
            case StatType.GodKill:
                CurrentGodKill = Convert.ToBoolean(value);
                break;
            case StatType.Barrier:
                CurrentBarrier = Convert.ToBoolean(value);
                break;
            case StatType.BarrierCooldown:
                CurrentBarrierCooldown = value;
                break;
            case StatType.Invincibility:
                CurrentInvincibility = Convert.ToBoolean(value);
                break;
            case StatType.DashCount:
                CurrentDashCount = (int)value;
                break;
            case StatType.Adversary:
                CurrentAdversary = Convert.ToBoolean(value);
                break;
            case StatType.ProjDestroy:
                CurrentProjDestroy = Convert.ToBoolean(value);
                break;
            case StatType.ProjParry:
                CurrentProjParry = Convert.ToBoolean(value);
                break;
        }
    }
    #endregion
}
