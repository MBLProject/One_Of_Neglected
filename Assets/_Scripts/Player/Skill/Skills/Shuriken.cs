using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : ActiveSkill
{
    public Shuriken() : base(Enums.SkillName.Shuriken) { }

    public int prjCount => stats.projectileCount;

    protected override void SubscribeToPlayerStats()
    {
        PlayerStats playerStats = UnitManager.Instance.GetPlayer().Stats;

        playerStats.OnATKChanged += (value) => stats.aTK = value;
        playerStats.OnATKRangeChanged += (value) => stats.aTKRange = value;
        playerStats.OnCriRateChanged += (value) => stats.critical = value;
        playerStats.OnCriDamageChanged += (value) => stats.cATK = value;
        playerStats.OnProjAmountChanged += (value) => stats.projectileCount += 1;
        playerStats.OnDurationChanged += (value) => stats.lifetime *= value;
    }

    public override void ModifySkill()
    {
        // init SkillStats
        stats = new SkillStats()
        {
            defaultCooldown = 1f,
            cooldown = UnitManager.Instance.GetPlayer().Stats.CurrentCooldown,
            defaultATKRange = 1f,
            aTKRange = UnitManager.Instance.GetPlayer().Stats.CurrentATKRange,
            defaultDamage = 5f,
            aTK = UnitManager.Instance.GetPlayer().Stats.CurrentATK,
            pierceCount = 1,
            shotCount = 1,
            projectileCount = UnitManager.Instance.GetPlayer().Stats.CurrentProjAmount,
            projectileDelay = 0.1f,
            shotDelay = 0.5f,
            critical = 0.1f,
            cATK = UnitManager.Instance.GetPlayer().Stats.CurrentCriDamage,
            amount = 1f,
            lifetime = 3f,
            projectileSpeed = 3f,
        };
    }

    public override void LevelUp()
    {
        base.LevelUp();

        if (level >= 7)
        {
            level = 6;
            return;
        }

        switch (level)
        {
            case 2:
                stats.projectileCount++;
                stats.defaultDamage += 5f;
                stats.pierceCount++;
                break;
            case 3:
                stats.projectileCount++;
                stats.defaultDamage += 5f;
                stats.pierceCount++;
                break;
            case 4:
                stats.projectileCount++;
                stats.defaultDamage += 5f;
                stats.pierceCount++;
                break;
            case 5:
                stats.projectileCount++;
                stats.defaultDamage += 5f;
                stats.pierceCount++;
                break;
            case 6:
                stats.projectileCount++;
                stats.defaultDamage += 5f;
                stats.pierceCount++;
                break;
        }
    }
    
}
