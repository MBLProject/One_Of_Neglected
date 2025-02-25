using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : Skill
{
    public Gold() : base(Enums.SkillName.Gold) { }

    public override void ModifySkill()
    {
        var player = UnitManager.Instance.GetPlayer();

        //player.Stats.ModifyStatValue(Enums.StatType., 10f);
    }

    public override void LevelUp()
    {
        ModifySkill();
    }

}
