using UnityEngine;

public class Cheese : Skill
{
    public Cheese() : base(Enums.SkillName.Cheese)
    {
    }

    public override void InitSkill(float damage, int level, int pierceCount, int shotCount, int projectileCount, float projectileDelay, float shotDelay, float atkrange)
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(Enums.StatType.Hp, player.Stats.CurrentMaxHp * 0.3f);
    }

    public override void InitSkill()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(Enums.StatType.Hp, player.Stats.CurrentMaxHp * 0.3f);
    }
}