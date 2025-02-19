using UnityEngine;

public class Water : Skill
{
    public Water(float defaultCooldown) : base(Enums.SkillName.Water, defaultCooldown)
    {
    }

    public override void InitSkill(float damage, int level, int pierceCount, int shotCount, int projectileCount, float projectileDelay, float shotDelay)
    {
        base.InitSkill(damage, level, pierceCount, shotCount, projectileCount, projectileDelay, shotDelay);

        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(Enums.StatType.HpRegen, 1);
    }
} 