using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;
using System.Linq;
using System;
using Unity.VisualScripting;

public class PoisonShoes : ActiveSkill
{
    public PoisonShoes() : base(Enums.SkillName.PoisonShoes) { }

    protected override void SubscribeToPlayerStats()
    {
        //PlayerStats playerStats = UnitManager.Instance.GetPlayer().Stats;
        //playerStats.OnATKChanged += (value) => { stats.aTK = value; };
    }

    public override void InitSkill()
    {
        // init SkillStats
        Debug.Log("InitSkill!!");
        stats = new SkillStats()
        {
            defaultCooldown = 1.1f,
            cooldown = UnitManager.Instance.GetPlayer().Stats.CurrentCooldown,

            defaultATKRange = 1f,
            aTKRange = UnitManager.Instance.GetPlayer().Stats.CurrentATKRange,
            defaultDamage = 1f,
            aTK = UnitManager.Instance.GetPlayer().Stats.CurrentATK,
            pierceCount = 0,
            shotCount = 1,
            projectileCount = 1,
            projectileDelay = 0.1f,
            shotDelay = 0.5f,
            critical = 0.1f,
            cATK = 1.5f,
            amount = 1f,
            lifetime = 1.1f,
        };
        Debug.Log($"InitSkill!! {skillName} : stats.lifetime : {stats.lifetime}");

    }

    public override void LevelUp()
    {
        base.LevelUp();

        switch (level)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
        }
    }

}
