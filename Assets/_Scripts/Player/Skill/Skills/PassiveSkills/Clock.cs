using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : Skill
{
    public Clock(float defaultCooldown) : base(Enums.SkillName.Clock, defaultCooldown) { }

    public override void InitSkill(float damage, int level, int pierceCount, int shotCount, int projectileCount, float projectileDelay, float shotDelay)
    {
        base.InitSkill(damage, level, pierceCount, shotCount, projectileCount, projectileDelay, shotDelay);

        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(Enums.StatType.MaxHp, 10);
    }
}
