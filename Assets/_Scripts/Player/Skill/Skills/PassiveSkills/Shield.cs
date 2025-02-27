using UnityEngine;

public class Shield : PassiveSkill
{
    public Shield() : base(Enums.SkillName.Shield) { statType = Enums.StatType.Defense;}

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