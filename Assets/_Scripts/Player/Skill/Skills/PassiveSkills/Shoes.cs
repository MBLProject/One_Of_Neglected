using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoes : Skill
{
    public Shoes() : base(Enums.SkillName.Shoes) { }

    public override void ModifySkill()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(Enums.StatType.Mspd, 4f);
    }

    public override void LevelUp()
    {
        base.LevelUp();

        switch (level)
        {
            default:
                break;
        }
    }
}
