using Cysharp.Threading.Tasks;
using System;

public class ActiveSkill : Skill
{
    public SkillStats stats;

    public float FinalDamage => stats.defaultDamage + stats.aTK;       // ex) 3 + (3 per level) + Player's ATK Stat

    public float FinalCooldown => stats.defaultCooldown * (1 - stats.cooldown);       // ex) (1 per 3 seconds) * (1 - 0.3);

    public float FinelATKRange => stats.defaultATKRange * stats.aTKRange;

    public ActiveSkill(Enums.SkillName skillName) : base(skillName)
    {
        // init Stats
        InitSkill();

        SubscribeToPlayerStats();
    }



    public override void InitSkill()
    {
        // init SkillStats
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
                await UniTask.Delay(TimeSpan.FromSeconds(FinalCooldown));
            }
            else
            {
                await UniTask.Yield();
            }
        }
    }

    protected virtual void Fire()
    {
        ProjectileManager.Instance.SpawnProjectile(skillName, stats.defaultDamage, level, stats.shotCount, stats.projectileCount, stats.projectileDelay, stats.shotDelay, stats.pierceCount);
    }
}