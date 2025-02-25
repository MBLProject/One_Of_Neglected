using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : Skill
{
    public Magnet() : base(Enums.SkillName.Magnet) { }

    public override void InitSkill()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(Enums.StatType.Magnet, 10f);
    }

    public override void LevelUp()
    {
        base.LevelUp();

        switch (level)
        {
            default:
                InitSkill();
                break;
        }
    }
}
