using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crown : Skill
{
    public Crown() : base(Enums.SkillName.Crown) { }

    public override void ModifySkill()
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
                ModifySkill();
                break;
        }
    }
}
