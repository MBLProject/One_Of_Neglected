using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : Skill
{
    public Fist() : base(Enums.SkillName.Fist) { }

    public override void ModifySkill()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(Enums.StatType.ATK, 20f);
    }

    public override void LevelUp()
    {
        base.LevelUp();

        switch (level)
        {
            default:
                ModifySkill();
                break;
        }
    }
}
