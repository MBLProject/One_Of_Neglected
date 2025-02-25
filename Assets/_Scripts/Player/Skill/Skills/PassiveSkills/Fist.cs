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
