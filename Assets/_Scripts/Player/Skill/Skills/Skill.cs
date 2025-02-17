using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

[Serializable]
public class Skill
{
    public Enums.SkillName skillName;
    public float cooldown;

    protected float defaultCooldown;
    protected float damage = 1f;
    protected int level = 1;
    protected int pierceCount = 0;
    protected int shotCount = 1;
    protected int projectileCount = 1;

    protected float projectileDelay = 0.1f;
    protected float shotDelay = 0.5f;

    protected bool isSkillActive = false;

    protected Skill(Enums.SkillName skillName, float defaultCooldown, float pierceDelay = 0.1f, float shotDelay = 0.5f)
    {
        this.skillName = skillName;
        this.defaultCooldown = defaultCooldown;
        this.projectileDelay = pierceDelay;
        this.shotDelay = shotDelay;
    }

    public void InitSkill(float damage, int level, int pierceCount, int shotCount, int projectileCount, float projectileDelay, float shotDelay)
    {
        this.damage = damage;
        this.level = level;
        this.pierceCount = pierceCount;
        this.shotCount = shotCount;
        this.projectileCount = projectileCount;
        this.projectileDelay = projectileDelay;
        this.shotDelay = shotDelay;
    }

    public virtual async void StartMainTask()
    {
        await StartSkill();
    }

    protected virtual async UniTask StartSkill()
    {
        isSkillActive = true;

        while (isSkillActive)
        {
            if (!GameManager.Instance.isPaused)
            {
                Fire(); // ??ㅼ뻣 ?紐꾪뀱
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
        ProjectileManager.Instance.SpawnProjectile(skillName, damage, level, shotCount, projectileCount, projectileDelay, shotDelay);
    }


    public static async UniTask DelayFloat(float delayInSeconds)
    {
        int delayInMilliseconds = (int)(delayInSeconds * 1000);
        await UniTask.Delay(delayInMilliseconds);
    }
}
