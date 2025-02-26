using UnityEngine;

public class Shield : PassiveSkill
{
    public Shield() : base(Enums.SkillName.Shield) { statType = Enums.StatType.Defense; }

    public override void ModifySkill()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(statType, 1f);
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

    public override void UnRegister()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(statType, -1f * level);
    }
}