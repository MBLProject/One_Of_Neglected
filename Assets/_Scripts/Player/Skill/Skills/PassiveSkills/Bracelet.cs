using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bracelet : PassiveSkill
{
    public Bracelet() : base(Enums.SkillName.Bracelet) { statType = Enums.StatType.Duration; ModifySkill(); }

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
