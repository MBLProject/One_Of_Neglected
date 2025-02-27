using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : PassiveSkill
{
    public Magnet() : base(Enums.SkillName.Magnet) { statType = Enums.StatType.Magnet;}

    public override void ModifySkill()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(statType, 30f);
    }

    public override void UnRegister()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(statType, -30f * level);
    }
}
