using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public class ActiveSkill : Skill
{

    public ActiveSkill(Enums.SkillName skillName, float defaultCooldown, float pierceDelay = 0.1f, float shotDelay = 0.5f) 
        : base(skillName, defaultCooldown, pierceDelay, shotDelay)
    {
    }

    public override async void StartMainTask()
    {
        await StartSkill();
    }

    public override void StopMainTask()
    {
        isSkillActive = false;
    }

    protected virtual async UniTask StartSkill()
    {
        isSkillActive = true;

        while (isSkillActive)
        {
            if (!GameManager.Instance.isPaused)
            {
                Fire();
                await UniTask.Delay(TimeSpan.FromSeconds(defaultCooldown));
            }
            else
            {
                await UniTask.Yield();
            }
        }
    }

    protected virtual void Fire()
    {
        ProjectileManager.Instance.SpawnProjectile(skillName, damage, level, shotCount, projectileCount, projectileDelay, shotDelay, pierceCount);
    }
} 