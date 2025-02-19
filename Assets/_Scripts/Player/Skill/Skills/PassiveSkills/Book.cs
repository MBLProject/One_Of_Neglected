using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : Skill
{
    public Book(float defaultCooldown) : base(Enums.SkillName.Book, defaultCooldown) { }

    public override void InitSkill(float damage, int level, int pierceCount, int shotCount, int projectileCount, float projectileDelay, float shotDelay)
    {
        base.InitSkill(damage, level, pierceCount, shotCount, projectileCount, projectileDelay, shotDelay);

        var player = UnitManager.Instance.GetPlayer();

        player.Stats.ModifyStatValue(Enums.StatType.ATKRange, 10);
    }
}
