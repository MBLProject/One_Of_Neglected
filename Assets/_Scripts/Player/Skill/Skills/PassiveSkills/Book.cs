using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : Skill
{
    public Book() : base(Enums.SkillName.Book) { }

    public override void InitSkill()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(Enums.StatType.ATKRange, 10f);
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
