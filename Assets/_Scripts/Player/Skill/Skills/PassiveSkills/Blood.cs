using UnityEngine;

public class Blood : PassiveSkill
{
    public Blood() : base(Enums.SkillName.Blood) { statType = Enums.StatType.MaxHp; ModifySkill(); }

    public override void ModifySkill()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(statType, 10f);
    }

    public override void LevelUp()
    {
        if (level >= 5) return;

        base.LevelUp();
        ModifySkill();
    }

    public override void UnRegister()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(statType, -10f * level);
    }
}