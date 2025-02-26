using UnityEngine;

public class Blood : PassiveSkill
{
    public Blood() : base(Enums.SkillName.Blood) { statType = Enums.StatType.MaxHp; }

    public override void ModifySkill()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(statType, 10f);
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

        player.Stats.ModifyStatValue(statType, -10f * level);
    }
}