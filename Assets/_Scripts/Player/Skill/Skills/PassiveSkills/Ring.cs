using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : Skill
{
    public Ring() : base(Enums.SkillName.Ring) { }

    public override void ModifySkill()
    {
        Debug.Log($"{skillName} ModifySkill : {level}");

        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(Enums.StatType.ProjAmount, 1f);
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
}
