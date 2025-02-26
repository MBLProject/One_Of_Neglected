using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkill : Skill
{
    protected Enums.StatType statType;

    public PassiveSkill(Enums.SkillName skillName) : base(skillName) { }

    public override void LevelUp()
    {
        base.LevelUp();

        if (level >= 6)
        {
            level = 5;
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
