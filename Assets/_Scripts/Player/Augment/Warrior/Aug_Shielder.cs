
public class Aug_Shielder : ConditionalAugment
{
    private float healthThreshold = 0.3f;
    private bool isTriggered = false;
    
    public Aug_Shielder(Player owner) : base(owner) 
    {
        aguName = Enums.AugmentName.Shielder;
    }

    public override bool CheckCondition()
    {
        float healthPercent = owner.Stats.currentHp / owner.Stats.CurrentMaxHp;
        return healthPercent <= healthThreshold && !isTriggered;
    }

    public override void OnConditionDetect()
    {
        float shieldAmount = 50f * Level;
        owner.ModifyStat(Enums.StatType.Defense, shieldAmount);
        isTriggered = true;
        // 쿨다운 후 리셋하는 로직 추가
    }
}