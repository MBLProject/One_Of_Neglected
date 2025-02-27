using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : PassiveSkill
{
    public Ring() : base(Enums.SkillName.Ring) { statType = Enums.StatType.ProjAmount; ModifySkill(); }

    public override void ModifySkill()
    {
        Debug.Log($"{skillName} ModifySkill : {level}");

        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(statType, 1f);
    }

    public override void LevelUp()
    {
        if (level >= 2) return;

        level++;
        ModifySkill();
    }

    public override void UnRegister()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(statType, -1f * level);
    }
}
