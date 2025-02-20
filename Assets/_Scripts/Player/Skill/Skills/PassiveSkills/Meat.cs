using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meat : Skill
{
    public Meat() : base(Enums.SkillName.Meat) { }

    public override void InitSkill()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(Enums.StatType.Greed, 10f);
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
