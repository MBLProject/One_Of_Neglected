using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : PassiveSkill
{
    public Fist() : base(Enums.SkillName.Fist) { statType = Enums.StatType.ATK; ModifySkill(); }

    public override void ModifySkill()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(statType, 20f);
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

        player.Stats.ModifyStatValue(statType, -20f * level);
    }
}
