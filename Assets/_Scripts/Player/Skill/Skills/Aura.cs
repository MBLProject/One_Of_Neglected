using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura : ActiveSkill
{
    private bool hasSpawned = false;

    public Aura() : base(Enums.SkillName.Aura) { }

    protected override async UniTask StartSkill()
    {
        isSkillActive = true;

        while (true)
        {
            if (!GameManager.Instance.isPaused && isSkillActive)
            {
                if (!hasSpawned)
                {
                    Fire();
                    hasSpawned = true;
                }
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
            else
            {
                hasSpawned = false;
                await UniTask.Yield();
            }
        }
    }

    protected override void SubscribeToPlayerStats()
    {
        PlayerStats playerStats = UnitManager.Instance.GetPlayer().Stats;
        playerStats.OnATKChanged += (value) => value = stats.aTK;
        playerStats.OnATKRangeChanged += (value) => value = stats.aTKRange;
        playerStats.OnCriRateChanged += (value) => value = stats.critical;
        playerStats.OnCriDamageChanged += (value) => value = stats.cATK;
    }

    public override void InitSkill()
    {
        // init SkillStats
        stats = new SkillStats()
        {
            defaultCooldown = 2f,
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
            cATK = 1f,
            amount = 1f,
            lifetime = 5f,
        };
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