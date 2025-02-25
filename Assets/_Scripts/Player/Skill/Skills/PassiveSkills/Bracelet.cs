using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bracelet : Skill
{
    public Bracelet() : base(Enums.SkillName.Bracelet) { }

    public override void ModifySkill()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(Enums.StatType.Duration, 10f);
    }

    public override void LevelUp()
    {
        base.LevelUp();

        if (level >= 6)
        {
            level = 5;
            Debug.Log($"LevelUp!!!!3 : {level}");
            return;
        }

        switch (level)
        {
            default:
                ModifySkill();
                break;
        }
    }
}
