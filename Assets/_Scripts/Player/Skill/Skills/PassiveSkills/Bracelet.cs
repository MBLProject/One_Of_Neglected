using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bracelet : Skill
{
    public Bracelet() : base(Enums.SkillName.Bracelet) { }

    public override void InitSkill()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(Enums.StatType.Duration, 10f);
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
