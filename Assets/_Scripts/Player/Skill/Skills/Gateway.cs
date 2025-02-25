using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gateway : ActiveSkill
{
    public Gateway() : base(Enums.SkillName.Gateway) { }

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
            defaultCooldown = 10f,
            cooldown = UnitManager.Instance.GetPlayer().Stats.CurrentCooldown,
            defaultATKRange = 1f,
            aTKRange = UnitManager.Instance.GetPlayer().Stats.CurrentATKRange,
            defaultDamage = 10f,
            aTK = UnitManager.Instance.GetPlayer().Stats.CurrentATK,
            pierceCount = 0,
            shotCount = 1,
            projectileCount = UnitManager.Instance.GetPlayer().Stats.CurrentProjAmount,
            projectileDelay = 0.1f,
            shotDelay = 0.5f,
            critical = 0.1f,
            cATK = UnitManager.Instance.GetPlayer().Stats.CurrentCriDamage,
            amount = 1f,
            lifetime = 5f,
            projectileSpeed = 1f,
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
                stats.defaultATKRange += 0.1f;
                stats.lifetime += 1f;
                break;
            case 3:
                stats.defaultDamage += 10f;
                stats.projectileCount += 1;
                break;
            case 4:
                stats.defaultATKRange += 0.1f;
                stats.lifetime += 1f;
                break;
            case 5:
                stats.defaultDamage += 10f;
                stats.projectileCount += 1;
                break;
            case 6:
                stats.defaultDamage += 10f;
                stats.defaultATKRange += 0.1f;
                stats.lifetime += 2f;
                break;
        }
    }
}
