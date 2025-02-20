using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : Skill
{
    public Ring() : base(Enums.SkillName.Ring) { }

    public override void InitSkill(float damage, int level, int pierceCount, int shotCount, int projectileCount, float projectileDelay, float shotDelay, float a)
    {
        base.InitSkill(damage, level, pierceCount, shotCount, projectileCount, projectileDelay, shotDelay, a);

        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(Enums.StatType.ProjAmount, 1f);
    }

    public override void InitSkill()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(Enums.StatType.ProjAmount, 1f);
    }
}
