using UnityEngine;

public class Blood : Skill
{

    public Blood() : base(Enums.SkillName.Blood) { }

    public override void ModifySkill(float damage, int level, int pierceCount, int shotCount, int projectileCount, float projectileDelay, float shotDelay, float a)
    {

        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(Enums.StatType.MaxHp, 10f);
    }

    public override void InitSkill()
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
                var player = UnitManager.Instance.GetPlayer();

                player.Stats.ModifyStatValue(Enums.StatType.MaxHp, 10f);
                break;
        }
    }
}