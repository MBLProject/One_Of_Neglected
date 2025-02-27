using UnityEngine;

public class Water : PassiveSkill
{
    public Water() : base(Enums.SkillName.Water) { statType = Enums.StatType.HpRegen;}

    public override void ModifySkill()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(statType, 1f);
    }

    public override void UnRegister()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(statType, -1f * level);
    }
}