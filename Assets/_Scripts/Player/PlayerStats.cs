using System;
using UnityEngine;
using static Enums;
public class PlayerStats
{
    private float regenTimer = 0f;
    private float regenPeriod = 1f;

    #region Field
    private int Level;
    private int MaxExp;
    private float Exp;
    private int MaxHp;
    private float Hp;   
    private float Recovery;
    private int Armor;   
    private float Mspd;    
    private float ATK;     
    private float Aspd;    
    private float Critical;
    private float CATK;    
    private int Amount;  
    private float Area;    
    private float Duration;
    private float Cooldown;
    private int Revival; 
    private float Magnet;  
    private float Growth;  
    private float Greed;   
    private float Curse;   
    private int Reroll;  
    private int Banish;
    #endregion

    #region Event
    // 특정 스텟이 변할때 이벤트를 호출시켜야 한다! 이럴때 아래 이벤트 쓰십쇼
    public event Action<float> OnHpChanged;
    public event Action<int> OnLevelUp;
    public event Action<float> OnExpChanged;
    public event Action<int> OnMaxExpChanged;
    public event Action<int> OnMaxHpChanged;
    public event Action<float> OnRecoveryChanged;
    public event Action<int> OnArmorChanged;
    public event Action<float> OnMspdChanged;
    public event Action<float> OnATKChanged;
    public event Action<float> OnAspdChanged;
    public event Action<float> OnCriticalChanged;
    public event Action<float> OnCATKChanged;
    public event Action<int> OnAmountChanged;
    public event Action<float> OnAreaChanged;
    public event Action<float> OnDurationChanged;
    public event Action<float> OnCooldownChanged;
    public event Action<int> OnRevivalChanged;
    public event Action<float> OnMagnetChanged;
    public event Action<float> OnGrowthChanged;
    public event Action<float> OnGreedChanged;
    public event Action<float> OnCurseChanged;
    public event Action<int> OnRerollChanged;
    public event Action<int> OnBanishChanged;
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
            if (Exp >= MaxExp)
            {
                Exp -= MaxExp;
                LevelUp();
            }
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
    public float CurrentRecovery
    {
        get => Recovery;
        set
        {
            Recovery = value;
            OnRecoveryChanged?.Invoke(Recovery);
        }
    }
    public int CurrentArmor
    {
        get => Armor;
        set
        {
            Armor = value;
            OnArmorChanged?.Invoke(Armor);
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
    public float CurrentCritical
    {
        get => Critical;
        set
        {
            Critical = value;
            OnCriticalChanged?.Invoke(Critical);
        }
    }
    public float CurrentCATK
    {
        get => CATK;
        set
        {
            CATK = value;
            OnCATKChanged?.Invoke(CATK);
        }
    }
    public int CurrentAmount
    {
        get => Amount;
        set
        {
            Amount = value;
            OnAmountChanged?.Invoke(Amount);
        }
    }
    public float CurrentArea
    {
        get => Area;
        set
        {
            Area = value;
            OnAreaChanged?.Invoke(Area);
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
    #endregion

    #region Func
    public void InitializeStats()
    {
        currentHp = MaxHp;
        currentExp = 0f;
    }


    /// <summary>
    /// 레벨업 메서드, 혹시 몰라서 일단 만듦
    /// </summary>
    public void LevelUp()
    {
        Level += 1;
        OnLevelUp?.Invoke(Level);
        CurrentMaxExp = CalculateNextLevelExp();
    }

    private int CalculateNextLevelExp()
    {
        //일단 레벨당 경험치 대충 처리함
        return (int)(100 * (1 + (Level - 1) * 0.2f));
    }

    public void UpadateHpRegen(float deltaTime)
    {
        regenTimer += deltaTime;
        if (regenTimer >= regenPeriod)
        {
            regenTimer = 0f;
            currentHp += (int)Recovery;
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
            case StatType.Recovery:
                CurrentRecovery += finalValue;
                break;
            case StatType.Armor:
                CurrentArmor += (int)finalValue;
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
            case StatType.Critical:
                CurrentCritical += finalValue;
                break;
            case StatType.CATK:
                CurrentCATK += finalValue;
                break;
            case StatType.Amount:
                CurrentAmount += (int)finalValue;
                break;
            case StatType.Area:
                CurrentArea += finalValue;
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
            case StatType.Recovery:
                CurrentRecovery = value;
                break;
            case StatType.Armor:
                CurrentArmor = (int)value;
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
            case StatType.Critical:
                CurrentCritical = value;
                break;
            case StatType.CATK:
                CurrentCATK = value;
                break;
            case StatType.Amount:
                CurrentAmount = (int)value;
                break;
            case StatType.Area:
                CurrentArea = value;
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
        }
    }
    #endregion
}
