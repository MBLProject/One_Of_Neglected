using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoes : PassiveSkill
{
    public Shoes() : base(Enums.SkillName.Shoes) { statType = Enums.StatType.Mspd; }

    public override void ModifySkill()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(statType, 4f);
    }

    public override void LevelUp()
    {
        base.LevelUp();

        if (level >= 6)
        {
            level = 5;
            Debug.Log($"LevelUp!!!!3 : {level}");
            return;
        }

        switch (level)
        {
            default:
                ModifySkill();
                break;
        }
    }

    public override void UnRegister()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(statType, -4f * level);
    }
}
