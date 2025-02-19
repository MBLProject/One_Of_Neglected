public class Aug_BigSword : TimeBasedAugment
{
    public Aug_BigSword(Player owner) : base(owner, 5f)
    {
        aguName = Enums.AugmentName.BigSword;
    }

    protected override void OnTrigger()
    {
        float damageBonus = 0.1f * Level;
        owner.ModifyStat(Enums.StatType.ATK, owner.Stats.CurrentATK * damageBonus);
        // 5초 후에 효과 제거하는 로직 추가
    }
}
