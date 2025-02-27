using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : PassiveSkill
{
    public Clock() : base(Enums.SkillName.Clock) { statType = Enums.StatType.Cooldown; ModifySkill(); }

    public override void ModifySkill()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(statType, 10f);
    }

    public override void LevelUp()
    {
        if (level >= 5) return;

        base.LevelUp();
        ModifySkill();
    }

    public override void UnRegister()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(statType, -10f * level);
    }
}
