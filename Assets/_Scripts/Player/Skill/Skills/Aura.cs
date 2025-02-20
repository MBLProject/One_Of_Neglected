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
        //PlayerStats playerStats = UnitManager.Instance.GetPlayer().Stats;
        //playerStats.OnATKChanged += (value) => { stats.aTK = value; };
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