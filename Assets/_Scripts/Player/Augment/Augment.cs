using System;
using Cysharp.Threading.Tasks;
using static Enums;

[Serializable]
public abstract class Augment
{
    protected int level = 0;
    protected bool isActive = false;
    protected Player owner;
    protected AugmentName aguName;

    public int Level => level;
    public bool IsActive => isActive;
    public AugmentName AugmentName => aguName;
    
    public Augment(Player owner)
    {
        this.owner = owner;
        this.aguName = AugmentName.None;
    }
    
    public virtual void Activate()
    {
        isActive = true;
        StartAugment();
    }
    
    public virtual void Deactivate()
    {
        isActive = false;
    }
    
    public virtual void LevelUp()
    {
        level++;
        OnLevelUp();
    }
    
    protected virtual void StartAugment() { }
    protected virtual void OnLevelUp() { }
}

public abstract class TimeBasedAugment : Augment 
{
    private float baseInterval;
    protected float interval => baseInterval * owner.Stats.CurrentCooldown;
    
    public TimeBasedAugment(Player owner, float interval) : base(owner)
    {
        this.baseInterval = interval;
    }
    
    protected void ModifyBaseInterval(float amount)
    {
        baseInterval += amount;
        if (baseInterval < 0.1f) baseInterval = 0.1f; 
    }
    
    protected override async void StartAugment()
    {
        while (isActive)
        {
            if (!GameManager.Instance.isPaused)
            {
                OnTrigger();
                await UniTask.Delay(TimeSpan.FromSeconds(interval));
            }
            else
            {
                await UniTask.Yield();
            }
        }
    }
    
    protected abstract void OnTrigger();
}

// 조건부 증강(발동 조건 필요함)
public abstract class ConditionalAugment : Augment
{
    public ConditionalAugment(Player owner) : base(owner) { }
    
    public abstract bool CheckCondition();
    public abstract void OnConditionDetect();
} 