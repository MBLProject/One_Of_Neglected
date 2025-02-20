using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crown : Skill
{
    public Crown() : base(Enums.SkillName.Crown) { }

    public override void ModifySkill(float damage, int level, int pierceCount, int shotCount, int projectileCount, float projectileDelay, float shotDelay, float a)
    {

        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(Enums.StatType.Growth, 10f);
    }


    public override void InitSkill()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(Enums.StatType.Growth, 10f);
    }

    public override void LevelUp()
    {
        base.LevelUp();

        switch (level)
        {
            default:
                break;
        }
    }
}
