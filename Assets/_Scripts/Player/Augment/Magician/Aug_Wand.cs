using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class Aug_Wand : TimeBasedAugment
{
    private GameObject wandtEffectPrefab;

    public Aug_Wand(Player owner, float interval) : base(owner, interval)
    {
        aguName = Enums.AugmentName.Wand;
        wandtEffectPrefab = Resources.Load<GameObject>("Using/Effect/WandEffect");
    }

    protected override void OnTrigger()
    {
        if (wandtEffectPrefab != null)
        {
            GameObject startEffect = GameObject.Instantiate(wandtEffectPrefab, owner.transform.position, Quaternion.identity);
            GameObject.Destroy(startEffect, 1f); 
        }

        var skillDispenser = owner.GetComponent<SkillDispenser>();
        if (skillDispenser == null || skillDispenser.skills.Count == 0) 
            return;

        foreach (var skill in skillDispenser.skills.Values)
        {
            if (skill is ActiveSkill activeSkill)
            {
                //activeSkill.ReduceCurrentCooldown(1);
            }
        }
    }

    public override void Deactivate()
    {
        var skillDispenser = owner.GetComponent<SkillDispenser>();
        if (skillDispenser != null)
        {
            foreach (var skill in skillDispenser.skills.Values)
            {
                if (skill is ActiveSkill activeSkill)
                {
                    //activeSkill.ResetCooldownOverride();
                }
            }
        }
        base.Deactivate();
    }
}

//using Cysharp.Threading.Tasks;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ActiveSkill : Skill
//{
//    public SkillStats stats;

//    private float currentCooldownOverride = -1f;
//    private float remainingCooldown = 0f;

//    public float FinalDamage => stats.defaultDamage + stats.aTK;       // ex) 3 + (3 per level) + Player's ATK Stat

//    public float AdvancedCooldown => stats.defaultCooldown * stats.cooldown;
//    public float FinalCooldown => currentCooldownOverride >= 0
//        ? currentCooldownOverride
//        : MathF.Max(((stats.projectileCount * stats.projectileDelay) + stats.shotDelay) * stats.amount, AdvancedCooldown);

//    public float FinelATKRange => stats.defaultATKRange * stats.aTKRange;

//    public ActiveSkill(Enums.SkillName skillName) : base(skillName)
//    {
//        // init Stats
//        InitSkill();

//        SubscribeToPlayerStats();
//    }

//    public override async void StartMainTask()
//    {
//        await StartSkill();
//    }

//    public override void StopMainTask()
//    {
//        isSkillActive = false;
//    }

//    protected virtual async UniTask StartSkill()
//    {
//        isSkillActive = true;

//        while (isSkillActive)
//        {
//            if (!GameManager.Instance.isPaused)
//            {
//                Fire();
//                remainingCooldown = FinalCooldown;

//                while (remainingCooldown > 0 && isSkillActive)
//                {
//                    if (!GameManager.Instance.isPaused)
//                    {
//                        await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
//                        remainingCooldown -= 0.1f;
//                    }
//                    else
//                    {
//                        await UniTask.Yield();
//                    }
//                }
//            }
//            else
//            {
//                await UniTask.Yield();
//            }
//        }
//    }

//    protected virtual void Fire()
//    {
//        //ProjectileManager.Instance.SpawnProjectile(skillName, stats.defaultDamage, level, stats.shotCount, stats.projectileCount, stats.projectileDelay, stats.shotDelay, stats.pierceCount);
//        ProjectileManager.Instance.SpawnProjectile(skillName,
//            new ProjectileStats()
//            {
//                finalCooldown = FinalCooldown,
//                finalATKRange = FinelATKRange,
//                finalDamage = FinalDamage,
//                pierceCount = stats.pierceCount,
//                shotCount = stats.shotCount,
//                projectileCount = stats.projectileCount,
//                projectileDelay = stats.projectileDelay,
//                shotDelay = stats.shotDelay,
//                critical = stats.critical,
//                cATK = stats.cATK,
//                amount = stats.amount,
//                lifetime = stats.lifetime,
//            }
//            );
//    }

//    public void OverrideCooldown(float newCooldown)
//    {
//        currentCooldownOverride = newCooldown;
//    }

//    public void ResetCooldownOverride()
//    {
//        currentCooldownOverride = -1f;
//    }

//    public void ReduceCurrentCooldown(float targetCooldown)
//    {
//        if (remainingCooldown > targetCooldown)
//        {
//            remainingCooldown = targetCooldown;
//        }
//    }
//}