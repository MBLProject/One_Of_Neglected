using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : Skill
{
    public Ring() : base(Enums.SkillName.Ring) { }

    public override void InitSkill()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(Enums.StatType.ProjAmount, 1f);
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
