using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meat : Skill
{
    public Meat() : base(Enums.SkillName.Meat) { }

    public override void ModifySkill()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(Enums.StatType.Greed, 10f);
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
