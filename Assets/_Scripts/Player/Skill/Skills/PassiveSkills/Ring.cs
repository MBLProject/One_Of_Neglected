using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : PassiveSkill
{
    public Ring() : base(Enums.SkillName.Ring) { statType = Enums.StatType.ProjAmount; }

    public override void ModifySkill()
    {
        Debug.Log($"{skillName} ModifySkill : {level}");

        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(statType, 1f);
    }

    public override void LevelUp()
    {
        base.LevelUp();

        if (level >= 3)
        {
            level = 2;
            Debug.Log($"LevelUp!!!!3 : {level}");
            return;
        }

        switch (level)
        {
            case 1:
                ModifySkill();
                break;
            case 2:
                ModifySkill();
                break;
            default:
                break;
        }
    }

    public override void UnRegister()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(statType, -1f * level);
    }
}
