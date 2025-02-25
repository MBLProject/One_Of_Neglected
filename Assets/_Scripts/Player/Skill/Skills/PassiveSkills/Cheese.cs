using UnityEngine;

public class Cheese : Skill
{
    public Cheese() : base(Enums.SkillName.Cheese)
    {
    }

    public override void InitSkill()
    {
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(Enums.StatType.Hp, player.Stats.CurrentMaxHp * 0.3f);
    }

    public override void LevelUp()
    {
        InitSkill();
    }
}