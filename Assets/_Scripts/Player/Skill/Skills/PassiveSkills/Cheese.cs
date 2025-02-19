using UnityEngine;

public class Cheese : Skill
{
    public Cheese(float defaultCooldown) : base(Enums.SkillName.Cheese, defaultCooldown)
    {
    }

    public override void InitSkill(float damage, int level, int pierceCount, int shotCount, int projectileCount, float projectileDelay, float shotDelay)
    {
        base.InitSkill(damage, level, pierceCount, shotCount, projectileCount, projectileDelay, shotDelay);
        
        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(Enums.StatType.Hp, player.Stats.CurrentMaxHp * 0.3f);
    }
} 