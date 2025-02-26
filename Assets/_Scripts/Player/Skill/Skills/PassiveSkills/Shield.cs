using UnityEngine;

public class Shield : Skill
{
    public Shield() : base(Enums.SkillName.Shield) { }

    public override void ModifySkill()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(Enums.StatType.Defense, 1f);
    }

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