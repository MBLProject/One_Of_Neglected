using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : Skill
{
    public Clock() : base(Enums.SkillName.Clock) { }

    public override void InitSkill()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(Enums.StatType.Cooldown, 10f);
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
