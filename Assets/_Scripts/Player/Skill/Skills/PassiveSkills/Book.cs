using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : Skill
{
    public Book() : base(Enums.SkillName.Book) { }

    public override void ModifySkill()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(Enums.StatType.ATKRange, 10f);
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
