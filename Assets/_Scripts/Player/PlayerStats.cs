using System;
using UnityEngine;

public class PlayerStats : ScriptableObject
{
    #region Event
    // 스텟은 여기 이벤트로만 처리할 것!!! 진짜루..
    public event Action<float> OnHealthChanged;
    public event Action<int> OnLevelUp;
    public event Action<float> OnExpChanged;
    #endregion

    #region Field
    public int Level;
    public int MaxExp;
    public int Exp;
    public int MaxHp;
    public int Hp;   
    public int Recovery;
    public int Armor;   
    public int Mspd;    
    public int ATK;     
    public int Aspd;    
    public int Critical;
    public int CATK;    
    public int Amount;  
    public int Area;    
    public int Duration;
    public int Cooldown;
    public int Revival; 
    public int Magnet;  
    public int Growth;  
    public int Greed;   
    public int Curse;   
    public int Reroll;  
    public int Banish;
    #endregion

    private float regenTimer = 0f;
    public float currentHp
    {
        get => Hp;
        set
        {
            Hp = (int)Mathf.Clamp(value, 0, MaxHp);
            OnHealthChanged?.Invoke(Hp);
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
