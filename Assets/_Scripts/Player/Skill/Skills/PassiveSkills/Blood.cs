using UnityEngine;

public class Blood : Skill
{

    public Blood() : base(Enums.SkillName.Blood) { }

    public override void ModifySkill()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(Enums.StatType.MaxHp, 10f);
    }

    public override void LevelUp()
    {
        base.LevelUp();

        switch (level)
        {
            default:
                ModifySkill();
                break;
        }
    }
}