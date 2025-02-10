using System;
using UnityEngine;

public class PlayerStats : ScriptableObject
{
    #region Field
    public int Level;
    public int MaxExp;
    public float Exp;
    public int MaxHp;
    public float Hp;   
    public float Recovery;
    public int Armor;   
    public float Mspd;    
    public float ATK;     
    public float Aspd;    
    public float Critical;
    public float CATK;    
    public int Amount;  
    public float Area;    
    public float Duration;
    public float Cooldown;
    public int Revival; 
    public int Magnet;  
    public float Growth;  
    public float Greed;   
    public float Curse;   
    public int Reroll;  
    public int Banish;
    #endregion

    #region Event
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
    public event Action<int> OnMagnetChanged;
    public event Action<float> OnGrowthChanged;
    public event Action<float> OnGreedChanged;
    public event Action<float> OnCurseChanged;
    public event Action<int> OnRerollChanged;
    public event Action<int> OnBanishChanged;
    #endregion

    private float regenTimer = 0f;

    #region Properties
    public float currentHp
    {
        get => Hp;
        set
        {
            Hp = (int)Mathf.Clamp(value, 0, MaxHp);
            OnHpChanged?.Invoke(Hp);
        }
    }
    public float currentExp
    {
        get => Exp;
        set
        {
            Exp = (int)value;
            OnExpChanged?.Invoke(Exp);
            if (Exp >= MaxExp)
            {
                Exp -= MaxExp;
                LevelUp();
            }
        }
    }

    public int CurrentLevel 
    { 
        get => Level;
        set
        {
            Level = value;
            OnLevelUp?.Invoke(Level);
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

    public int CurrentMaxHp
    {
        get => MaxHp;
        set
        {
            MaxHp = value;
            OnMaxHpChanged?.Invoke(MaxHp);
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

    public int CurrentMagnet
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

    public void InitializeStats()
    {
        currentHp = MaxHp;
        currentExp = 0f;
    }

    public void LevelUp()
    {
        Level++;
        currentHp = MaxHp;

        OnLevelUp?.Invoke(Level);
    }

    //데미지 판정을 여기서??? 
    public float CalculateDamage(float incomingDamage)
    {
        return 1f;
        //return incomingDamage * (1f - damageReduction);
    }

    public void UpadateHealthRegen(float deltaTime)
    {
        regenTimer += deltaTime;
        if (regenTimer >= Recovery)
        {
            regenTimer = 0f;
            Recovery += Mathf.RoundToInt(Recovery);
        }
    }
}
