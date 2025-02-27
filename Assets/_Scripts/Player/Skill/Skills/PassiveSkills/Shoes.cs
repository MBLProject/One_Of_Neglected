using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoes : PassiveSkill
{
    public Shoes() : base(Enums.SkillName.Shoes) { statType = Enums.StatType.Mspd; ModifySkill(); }

    public override void ModifySkill()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(statType, 4f);
    }

    public override void UnRegister()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(statType, -4f * level);
    }
}
