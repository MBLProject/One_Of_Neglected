using UnityEngine;

public class Shield : Skill
{
    public Shield(float defaultCooldown) : base(Enums.SkillName.Shield, defaultCooldown)
    {
    }

    public override void InitSkill(float damage, int level, int pierceCount, int shotCount, int projectileCount, float projectileDelay, float shotDelay)
    {
        base.InitSkill(damage, level, pierceCount, shotCount, projectileCount, projectileDelay, shotDelay);

        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(Enums.StatType.Defense, 4);
    }
} 