using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meat : Skill
{
    public Meat() : base(Enums.SkillName.Meat) { }

    public override void InitSkill(float damage, int level, int pierceCount, int shotCount, int projectileCount, float projectileDelay, float shotDelay, float a)
    {
        base.InitSkill(damage, level, pierceCount, shotCount, projectileCount, projectileDelay, shotDelay, a);

        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(Enums.StatType.Greed, 10f);
    }

    public override void InitSkill()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(Enums.StatType.Greed, 10f);
    }
}
