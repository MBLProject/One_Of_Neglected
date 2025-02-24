using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Javelin : ActiveSkill
{
    public Javelin() : base(Enums.SkillName.Javelin) { }

    protected override void SubscribeToPlayerStats()
    {
        PlayerStats playerStats = UnitManager.Instance.GetPlayer().Stats;

        playerStats.OnATKChanged += (value) => stats.aTK = value;
        playerStats.OnATKRangeChanged += (value) => stats.aTKRange = value;
        playerStats.OnCriRateChanged += (value) => stats.critical = value;
        playerStats.OnCriDamageChanged += (value) => stats.cATK = value;
        playerStats.OnProjAmountChanged += (value) => stats.projectileCount = value;
        playerStats.OnDurationChanged += (value) => stats.lifetime *= value;
    }

    public override void InitSkill()
    {
        // init SkillStats
        stats = new SkillStats()
        {
            defaultCooldown = 4f,
            cooldown = UnitManager.Instance.GetPlayer().Stats.CurrentCooldown,
            defaultATKRange = 1f,
            aTKRange = UnitManager.Instance.GetPlayer().Stats.CurrentATKRange,
            defaultDamage = 20f,
            aTK = UnitManager.Instance.GetPlayer().Stats.CurrentATK,
            pierceCount = 1,
            shotCount = 1,
            projectileCount = 1,
            projectileDelay = 0.1f,
            shotDelay = 0.5f,
            critical = 0.1f,
            cATK = 1f,
            amount = 1f,
            lifetime = 3f,
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
                stats.defaultDamage += 10f;
                stats.pierceCount += 1;
                break;
            case 3:
                stats.projectileCount += 1;
                stats.defaultATKRange *= 1.1f;
                break;
            case 4:
                stats.defaultDamage += 20f;
                stats.pierceCount += 1;
                break;
            case 5:
                stats.projectileCount += 1;
                stats.defaultATKRange *= 1.1f;
                break;
            case 6:
                stats.defaultDamage += 30f;
                stats.pierceCount += 2;
                break;
        }
    }
}
