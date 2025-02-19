using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bracelet : Skill
{
    public Bracelet(float defaultCooldown) : base(Enums.SkillName.Bracelet, defaultCooldown) { }

    public override void InitSkill(float damage, int level, int pierceCount, int shotCount, int projectileCount, float projectileDelay, float shotDelay)
    {
        base.InitSkill(damage, level, pierceCount, shotCount, projectileCount, projectileDelay, shotDelay);

        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(Enums.StatType.Duration, player.Stats.CurrentDuration * 0.1f);
    }
}
